using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PAMS.API.Hubs;
using PAMS.Application.Helpers;
using PAMS.Application.Interfaces.Services;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all sampling tasks in this application.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SampleController : BaseController
    {
        private readonly ISampleService sampleService;
        private readonly IClientService clientService;
        private readonly IWebHostEnvironment env;
        private readonly INotification notification;
        private readonly IContextAccessor contextAccessor;
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly UserManager<PamsUser> userManager;

        /// <summary>
        /// Constructor for injecting dependencies
        /// </summary>
        /// <param name="sampleService"></param>
        /// <param name="clientService"></param>
        /// <param name="env"></param>
        /// <param name="notification"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="notificationHubContext"></param>
        /// <param name="userManager"></param>
        public SampleController(
            ISampleService sampleService,
            IClientService clientService,
            IWebHostEnvironment env,
            INotification notification,
            IContextAccessor contextAccessor,
            IHubContext<NotificationHub> notificationHubContext,
            UserManager<PamsUser> userManager
            )
        {
            this.sampleService = sampleService;
            this.clientService = clientService;
            this.env = env;
            this.notification = notification;
            this.contextAccessor = contextAccessor;
            this.notificationHubContext = notificationHubContext;
            this.userManager = userManager;
        }

        /*************** Sampling ***********************/
        #region Sampling
        /// <summary>
        /// This endpoint creates sampling taken by field analyst for a client.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("sampling")]
        [ProducesResponseType(200, Type = typeof(Sampling))]
        public async Task<IActionResult> CreateClientSampling([FromBody]SamplingDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await sampleService.CreateSampling(model);
                if (response!=null)
                {

                    //var notificationData = new NotificationDTO
                    //{
                    //    Data = new
                    //    {
                    //        page = "Sampling",
                    //        pushtitle = model.SampleType.ToString() ?? "New Sampling",
                    //        pushdate = model.SamplingDate == null ? DateTime.Now.ToString() : model.SamplingDate,
                    //        pushdetails = "Sampling",
                    //        pushothers = response.PicturePaths ?? "",
                    //        pushimagelink = response.PicturePaths ?? ""
                    //    },
                    //    Notification = new
                    //    {
                    //        body = $"A new sampling has been conducted!",
                    //        title = $"New Sampling From {model.StaffName}",
                    //        icon = "icon"
                    //    },
                    //    To = "/topics/Sampling" + model.ClientId
                    //};
                    //var res = await notification.Send(notificationData);

                    var users = await GetAllUsers();

                    foreach (var user in users)
                    {
                            await notificationHubContext.Clients.Group(user.UserId.ToString()).SendAsync("ReceiveNotification", new {Title="Sampling",Details=$"New sampling from {model.StaffName}" });
                    }
                    return Ok(new ResponseViewModel { Message = "Sample saved!", Status = true, ReturnObject = response });
                }
                return BadRequest(new ResponseViewModel { Message = "Invalid clientId supplied!" });
            }

            return BadRequest(new ResponseViewModel { Message = "All fields are required!", Status = false });
        }

        /// <summary>
        /// This enpoint returns a sampling object whose id passed.
        /// </summary>
        /// <param name="samplingId"></param>
        /// <returns></returns>
        [HttpGet("sampling/{samplingId}")]
        [ProducesResponseType(200, Type = typeof(Sampling))]
        public IActionResult GetSampling([FromRoute] Guid samplingId)
        {
            if (samplingId!=Guid.Empty)
            {
                var response = sampleService.GetSampling(samplingId);
                return Ok(new ResponseViewModel { Message = response != null || response != default ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid samplingId passed!", Status = false });
        }

        /// <summary>
        /// This endpoint returns all the samplings that belongs to a client whose id passed.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("Samplings/{clientId}")]
        [ProducesResponseType(200, Type = typeof(List<Sampling>))]
        public async Task<IActionResult> GetSamplings([FromRoute] Guid clientId)
        {
            if (clientId != Guid.Empty)
            {
                var response = await sampleService.GetAllSamplings(clientId);
                return Ok(new ResponseViewModel { Message = response.Any() ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid clientId passed!", Status = false });
        }
        
        /// <summary>
        /// This endpoint returns all the sample template that belongs to the client whose id passed.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("{clientId}/SampleTemplates")]
        [ProducesResponseType(200, Type = typeof(ClientSampleResponse))]
        public async Task<IActionResult> GetSampleTemplates([FromRoute] Guid clientId)
        {
            var response = await sampleService.GetClientSampleTemplates(clientId);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
                
        }

        /// <summary>
        /// This endpoint returns all the created samplings in this application, for admins.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllSamplings")]
        [ProducesResponseType(200, Type=typeof(List<Sampling>))]
        public async Task<IActionResult> GetAllSamplings()
        {
            if (User.IsInRole("Staff"))
            {
                return Ok(new ResponseViewModel { Message = "You are allowed to read this!", Status = false });
            }

            var response = await sampleService.GetAllSamplings();
            return Ok(new ResponseViewModel { Status = true, Message = response.Any() ? "Query Ok!" : "No record found!", ReturnObject = response });
            
        }
        #endregion

        /// <summary>
        /// This method returns all users except staff.
        /// </summary>
        /// <returns></returns>
        protected async Task<List<User>> GetAllUsers()
        {
            var users = userManager.Users.ToList();
            List<User> appUsers = new List<User>();
            foreach (var user in users)
            {
                if (!await userManager.IsInRoleAsync(user, Constants.Staff))
                {
                    appUsers.Add(new User
                    {
                        Name = user.FirstName + " " + user.LastName,
                        UserId = Guid.Parse(user.Id)
                    });
                }
            }
            return appUsers;
        }
    }
}
