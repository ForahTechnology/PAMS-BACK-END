using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PAMS.API.Configuration;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility = PAMS.Application.Helpers.Utility;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all managerial task in this application.
    /// </summary>
    [ApiVersion("1.0")]
    public class ManagementController : BaseController
    {
        private readonly UserManager<PamsUser> userManager;
        private readonly SignInManager<PamsUser> signInManager;
        private readonly IOptionsMonitor<JwtConfig> optionsMonitor;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IStoreManager<UserActivation> activationManager;
        private readonly IMailer mailer;
        /// <summary>
        /// Constructor for dependency injections.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="optionsMonitor"></param>
        /// <param name="env"></param>
        /// <param name="roleManager"></param>
        /// <param name="activationManager"></param>
        /// <param name="mailer"></param>
        /// <param name="config"></param>
        public ManagementController(
            IMapper mapper,
            UserManager<PamsUser> userManager,
            SignInManager<PamsUser> signInManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager,
            IStoreManager<UserActivation> activationManager,
            IMailer mailer,
            IConfiguration config
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.optionsMonitor = optionsMonitor;
            this.roleManager = roleManager;
            this.activationManager = activationManager;
            this.mailer = mailer;
        }

        /// <summary>
        /// This endpoint returns all the users of this application.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUsers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            List<User> appUsers = new List<User>();
            foreach (var user in users)
            {
                if (!await userManager.IsInRoleAsync(user, "SuperAdmin"))
                {
                    appUsers.Add(await GetUserWithUserRole(user, roles));
                }
            }
            return Ok(new ResponseViewModel {Message="Query Ok!", Status=true, ReturnObject = appUsers });
        }
        /// <summary>
        /// This Method returns list of all users and theirs roles.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        private async Task<User> GetUserWithUserRole(PamsUser user, List<IdentityRole> roles)
        {
            foreach (var role in roles)
            {
                var userRole = (await userManager.IsInRoleAsync(user, role.Name)) ? role.Name : "";
                if (!string.IsNullOrWhiteSpace(userRole))
                {

                    return new User
                    {
                        UserId = Guid.Parse(user.Id),
                        Name = $"{user.FirstName } {user.LastName}",
                        Role = userRole,
                        Status = user.LockoutEnabled ? "Disabled" : "Active",
                        RegistrationDate = user.RegisteredDate.ToShortDateString()
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// This action method unlocks out any user whose id is sent in.
        /// </summary>
        /// <param name="userId">string userId</param>
        /// <returns></returns>
        [HttpPatch("disableUser/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> Disable([FromRoute]string userId)
        {
            //Finding the user by ID
            var result = await userManager.FindByIdAsync(userId);
            if (result == null)
            {
                return NotFound(new ResponseViewModel { Message = "User not found!", Status = false });
            }
            result.LockoutEnabled = true;
            await userManager.UpdateAsync(result);
            return Ok(new ResponseViewModel { Message = "Account has been deactivated!", Status = true });

        }


        /// <summary>
        /// This action method locks out any user whose id is sent in.
        /// </summary>
        /// <param name="userId">string userId</param>
        /// <returns></returns>
        [HttpPatch("enableUser/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> Enable(string userId)
        {
            //Finding user
            var result = await userManager.FindByIdAsync(userId);
            if (result == null)
            {
                return NotFound(new ResponseViewModel { Message = "User not found!", Status = false });
            }
            result.LockoutEnabled = false;
            await userManager.UpdateAsync(result);
            return Ok(new ResponseViewModel { Message = "Account has been Reactivated!", Status = true });
        }

        /// <summary>
        /// This endpoint upgrades or downgrades a user by changing the user role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPut("changeUserRole/{userId}/{roleId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> AddToRole([FromRoute] string userId, [FromRoute] string roleId)
        {
            if (!string.IsNullOrWhiteSpace(userId)|| !string.IsNullOrWhiteSpace(roleId))
            {
                var user = await userManager.FindByIdAsync(userId);
                var userRole = await GetUserWithUserRole(user, roleManager.Roles.ToList());
                var newRole = await roleManager.FindByIdAsync(roleId);
                if (user!=null && userRole!=null && newRole!=null)
                {
                    await userManager.RemoveFromRoleAsync(user, userRole.Role);
                    await userManager.AddToRoleAsync(user, newRole.Name);
                    return Ok(new ResponseViewModel { Message = $"User role now updated to {newRole.Name} successfully!", Status = true });
                }
                return BadRequest(new ResponseViewModel { Message = "User or role could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "User and role IDs are required!", Status = false });
        }

        /// <summary>
        /// This endpoint returns all the available user types.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserTypes")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        [ProducesResponseType(200,Type = typeof(List<RoleViewModel>))]
        public IActionResult GetRoles()
        {
            var roleList = new List<RoleViewModel>();
            var roles = roleManager.Roles;
            foreach (var role in roles)
            {
                if (role.Name!= "SuperAdmin")
                {
                    roleList.Add(new RoleViewModel { RoleId = role.Id, RoleName = role.Name }); 
                }
            }
            return Ok(new ResponseViewModel { Message = "Quey ok!", Status = true, ReturnObject = roleList });
        }
    }
}
