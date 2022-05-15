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
using PAMS.Application.Interfaces.Services;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using PAMS.Services.Implementations.Utilites;
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
    /// This controller handles all account processes of this application.
    /// Registration, Update and all account verification of user.
    /// </summary>
    [ApiVersion("1.0")]
    public class AccountController : BaseController
    {
        private readonly IMapper mapper;
        private readonly UserManager<PamsUser> _userManager;
        private readonly SignInManager<PamsUser> signInManager;
        private readonly IWebHostEnvironment env;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IStoreManager<UserActivation> activationManager;
        private readonly IMailService _mailerService;
        private readonly IConfiguration config;
        private readonly IClientService clientService;
        private readonly IInvoiceService invoiceService;
        private readonly IUploadService _imageUploadService;
        private readonly IContextAccessor _contextAccessor;
        private long? imageId;

        /// <summary>
        /// Constructor for dependency injections
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="optionsMonitor"></param>
        /// <param name="env"></param>
        /// <param name="roleManager"></param>
        /// <param name="activationManager"></param>
        /// <param name="mailer"></param>
        /// <param name="mailerService"></param>
        /// <param name="config"></param>
        /// <param name="clientService"></param>
        /// <param name="invoiceService"></param>
        /// <param name="imageUploadService"></param>
        /// <param name="contextAccessor"></param>
        public AccountController(
            IMapper mapper,
            UserManager<PamsUser> userManager,
            SignInManager<PamsUser> signInManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager,
            IStoreManager<UserActivation> activationManager,
            IMailService mailerService,
            IConfiguration config,
            IClientService clientService,
            IInvoiceService invoiceService,
            IUploadService imageUploadService,
            IContextAccessor contextAccessor
            )
        {
            this._userManager = userManager;
            this.signInManager = signInManager;
            this.env = env;
            this.roleManager = roleManager;
            this.activationManager = activationManager;
            this.mapper = mapper;
            _mailerService = mailerService;
            this.config = config;
            this.clientService = clientService;
            this.invoiceService = invoiceService;
            _imageUploadService = imageUploadService;
            _contextAccessor = contextAccessor;
        }
        /// <summary>
        /// This endpoint registers staff on this application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [HttpPost("SignUp")]                
        public async Task<IActionResult> SignUp([FromForm] SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    return BadRequest(new ResponseViewModel()
                    {
                        Status = false,
                        Message = "Email belongs to an existing staff"
                    });
                }
                var codeExists = activationManager.DataStore.GetAllQuery().FirstOrDefault(activation => activation.Email == model.Email);
                if (codeExists == null)
                {
                    return BadRequest(new ResponseViewModel()
                    {
                        Status = false,
                        Message = "Activation Code is incorrect"
                    });
                }
                if (codeExists.Code != model.ActivationCode || codeExists.Email != model.Email)
                {
                    return BadRequest(new ResponseViewModel()
                    {
                        Status = false,
                        Message = "Activation Code is incorrect"
                    });
                }

                if (!(model.Picture is null))
                     imageId = await _imageUploadService.UploadImageToDatabase(model.Picture);

                var newUser = new PamsUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    EmailConfirmed = true,
                    RegisteredDate = DateTime.Now,
                    OTP = model.ActivationCode,
                    PhoneNumber = model.PhoneNumber,
                    Active = true,
                    LockoutEnabled = true,
                    ImageModelId = imageId
                };
               
                var isCreated = await _userManager.CreateAsync(newUser, model.Password);
                if (isCreated.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Staff").Result)
                    {
                        var role = new IdentityRole()
                        {
                            Name = "Staff"
                        };
                        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                        _userManager.AddToRoleAsync(newUser, "Staff").Wait();
                    }
                    else
                    {
                        _userManager.AddToRoleAsync(newUser, "Staff").Wait();
                    }
                    await activationManager.DataStore.Delete(codeExists.ID);
                    await activationManager.Save();
                    return Ok(new ResponseViewModel {  Status = true, Message = "Registered successfully!"});
                }


            }
            return BadRequest(new ResponseViewModel()
            {
                Status = false,
                Message = "All fields are required!"
            });
        }

        /// <summary>
        /// This endpoint Updates staff Details on this application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [HttpPut("UpdateAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAccount([FromForm] EditDTO model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return BadRequest(new ResponseViewModel()
                {
                    Status = false,
                    Message = "User doesn't Exist"
                });
            }
            //no image in db b4 now, and the update has an image
            if (user.ImageModelId == null && model.Picture != null)
            {
                imageId = await _imageUploadService.UploadImageToDatabase(model.Picture);
                if (imageId != null)
                    user.ImageModelId = imageId;
            }
            //there's an existing image, also there's an image to update the old one
            else if (user.ImageModelId != null && model.Picture != null)
            {
                await _imageUploadService.UpdateImageInDatabase(model.Picture, user.ImageModelId);
            }
               
            user.FirstName = model.FirstName is null ? user.FirstName : model.FirstName;
            user.LastName = model.LastName is null ? user.LastName : model.LastName;
            user.PhoneNumber = model.PhoneNumber is null ? user.PhoneNumber : model.PhoneNumber;

            await _userManager.UpdateAsync(user);
            return Ok(new ResponseViewModel { Status = true, Message = "Updated successfully!" });
        }

        /// <summary>
        /// This method generates access token for the user that is passed into it.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string Token</returns>
        private async Task<string> GenerateJWT(PamsUser user)
        {

            //get the user's roles
            var roles = await _userManager.GetRolesAsync(user);
            //Generate Token
            var expirationDay = DateTime.UtcNow.AddDays(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtSettings:Secret"].ToString()));
            List<Claim> subjectClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            subjectClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            subjectClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            subjectClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(subjectClaims.AsEnumerable()),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Expires = expirationDay

            };
            //create the token 
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// This endpoint signs in a user whose correct credentials are sent in.
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody]SignInDTO model)
        {
            if (ModelState.IsValid)
            {
                // check if the user with the same email exist
                var user = await _userManager.FindByEmailAsync(model.Email.Trim());

                if (user == null)
                {
                    // We dont want to give to much information on why the request has failed for security reasons
                    return BadRequest(new ResponseViewModel()
                    {
                        Status = false,
                        Message ="User with this email doesn't exixts"
                               
                    });
                }

                // Now we need to check if the user has inputed the right password
                var isCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

                if (isCorrect)
                {
                    var imageDetail = new ImageVM();

                    //get image as base64 string
                    if (!(user.ImageModelId is null))
                        imageDetail = await _imageUploadService.GetFileFromDatabase(user.ImageModelId);

                    if (user.LockoutEnabled)
                    {
                        return Ok(new ResponseViewModel { Message = "Your account has been suspended! Contact Admin.", Status = false });
                    }
                    var jwtToken = await GenerateJWT(user);

                    return Ok(new ResponseViewModel()
                    {
                        Status = true,
                        Message = "Signed Successfully!",
                        ReturnObject = new SignInResponsDTO
                        {
                            Email = user.Email.Trim(),
                            Fullname = $"{user.FirstName} {user.LastName}",
                            UserId = Guid.Parse(user.Id),
                            Token = jwtToken,
                            Role = _userManager.GetRolesAsync(user).Result.ToList(),
                            TotalCustomers = clientService.Count(),
                            Invoices = invoiceService.GetClientRecentInvoices().Result,
                            TotalAmountInvoiced = invoiceService.GetTotalInvoiceAmount().Result,
                            ImageDetails = imageDetail
                        }
                    });
                }
                else
                {
                    // We dont want to give too much information on why the request has failed for security reasons
                    return BadRequest(new ResponseViewModel()
                    {
                        Status = false,
                        Message =  "Invalid authentication request"
                               
                    });
                }
            }

            return BadRequest(new ResponseViewModel()
            {
                Status = false,
                Message =  "Invalid signin request" 
            });
        }

        /// <summary>
        /// This endpoint signs out users.
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// This endpoint generates activation code for user registration by admin
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>        
        [Authorize(AuthenticationSchemes = "Bearer",Roles = "SuperAdmin,Admin")]        
        [HttpPost("GenerateActivationCode/{email}")]
        public async Task<IActionResult> GenerateActivationCode([FromRoute]string email)
        {
            if (User.IsInRole("Staff"))
            {
                return Ok(new ResponseViewModel { Message = "You are authorized to generate code!", Status = false });
            }
            if (!string.IsNullOrEmpty(email)&& _mailerService.IsValidEmail(email))
            {
                var userCodeExists = activationManager.DataStore.GetAllQuery().FirstOrDefault(activation => activation.Email == email);
                //Generating OTP for mobile app
                var OTP = Utility.GenerateOTP();

                // checking if a code has already been generated for the email.
                if (userCodeExists != null)
                {
                    userCodeExists.Code = OTP;
                    activationManager.DataStore.Update(userCodeExists);
                    await activationManager.Save();
                }
                else
                {
                    var userActivatonObj = new UserActivation()
                    {
                        Code = OTP,
                        Email = email.Trim()
                    };

                    await activationManager.DataStore.Add(userActivatonObj);
                    await activationManager.Save();
                }
                                
                //Generating password reset link for users signed from web.
                //var callbackURL = $"http://sethlab-001-site2.itempurl.com/?activationCode={OTP}&email={email.Trim()}";
                var callbackURL = $"http://sethlab-001-site2.itempurl.com/?activation";

                //Sending the generated link and OTP to the user email address
                var webRoot = env.WebRootPath;
                var templateLocation = webRoot + Path.DirectorySeparatorChar
                                               + Path.DirectorySeparatorChar
                                               + "EmailTemplates"
                                               + Path.DirectorySeparatorChar
                                               + "ActivationCode.html";
                var content = Utility.GenerateActivationEmailContent(OTP, callbackURL, templateLocation);

                //Sending email
                await _mailerService.SendMail(email, content[0], content[1]);

                return Ok(new ResponseViewModel()
                {
                    Status = true,
                    Message = "Email Successfully sent to the staff with Activaton Code",
                    
                });
            }
            
            // if Model not valid
            return BadRequest(new ResponseViewModel()
            {
                Status = false,
                Message = "Invalid email address supplied!"
            });
        }

        /// <summary>
        /// This endpoint resets the password for the account associated with this email.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword/{Email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string Email)
        {
            //Checking if email is valid.
            if (!string.IsNullOrWhiteSpace(Email))
            {
                //Finding the user in database.
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null)
                {
                    return NotFound(new ResponseViewModel { Status = false, Message = "There's no account associated with this email!" });
                }
                else
                {
                    try
                    {
                        //Generating encoded password reset token.
                        var token = WebEncoders.Base64UrlEncode(
                            Encoding.UTF8.GetBytes(
                                await _userManager.GeneratePasswordResetTokenAsync(user)
                                )
                            );
                        //Generating password reset link for users signed from web.
                        var callbackURL = $"http://sethlab-001-site2.itempurl.com/reset-password?resetToken={token}&email={Email}";

                        //Generating OTP for mobile app
                        var OTP = Utility.GenerateOTP();
                        var secrete = $"{OTP},{token}";

                        //Sending the generated link and OTP to the user email address
                        var webRoot = env.WebRootPath;
                        var templateLocation = webRoot + Path.DirectorySeparatorChar
                                                       + Path.DirectorySeparatorChar
                                                       + "EmailTemplates"
                                                       + Path.DirectorySeparatorChar
                                                       + "ForgotPassword.html";
                        var content = Utility.GenerateForgortPasswordEmailContent(OTP, user.FirstName, callbackURL, templateLocation);
                        user.OTP = secrete;
                        await _userManager.UpdateAsync(user);
                        //Sending email
                        await _mailerService.SendMail(Email, content[0], content[1]);
                        return Ok(new ResponseViewModel { Status = true, Message = "An OTP has been sent to your email!" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new ResponseViewModel { Message = ex.Message, Status = false });
                    }
                }
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid email entered!", Status = false });
        }

        /// <summary>
        /// This endpoint confirms an OTP
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("ConfirmOTP")]
        public async Task<IActionResult> ConfirmOTP([FromQuery] string otp, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) || !string.IsNullOrWhiteSpace(otp))
            {
                var user = await _userManager.FindByEmailAsync(email);
                var secrete = user.OTP.Split(",");
                if (otp == secrete[0])
                    return Ok(new ResponseViewModel { Status = true, Message = "OTP is valid!", ReturnObject = new { token = secrete[1] } });
                return BadRequest(new ResponseViewModel { Status = false, Message = "Invalid OTP supplied!" });
            }
            return BadRequest(new ResponseViewModel { Status = false, Message = "Invalid OTP or email supplied!" });
        }

        /// <summary>
        /// This enpoint reset password for user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordResetDTO model)
        {
            if (!string.IsNullOrWhiteSpace(model.Email) || !string.IsNullOrWhiteSpace(model.Password) || !string.IsNullOrWhiteSpace(model.ResetToken))
            {
                try
                {
                    // Find the user by email
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user == null)
                    {
                        return NotFound(new ResponseViewModel { Status = false, Message = "Email could not be found! Password reset unsuccessful!" });
                    }
                    //Converting password reset token.
                    var token = Encoding.UTF8.GetString(
                        WebEncoders.Base64UrlDecode(model.ResetToken)
                        );
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        user.OTP = "";
                        await _userManager.UpdateAsync(user);
                        return Ok(new ResponseViewModel { Status = true, Message = "Password reset successfully!" });
                    }
                    return BadRequest(new ResponseViewModel { Status = false, Message = $"{result.Errors.FirstOrDefault().Description}", ReturnObject = model });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ResponseViewModel { Status = false, Message = ex.InnerException.Message });
                }
            }
            return BadRequest(new ResponseViewModel { Status = false, Message = "All fields are required!", ReturnObject = model });
        }

        /// <summary>
        /// This endpoint changes user's account password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (string.IsNullOrWhiteSpace(model.NewPassword)|| string.IsNullOrWhiteSpace(model.OldPassword))
            {
                return BadRequest(new ResponseViewModel { Message = "All fields are required!", Status = false });
            }
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest(new ResponseViewModel { Message = result.Errors.FirstOrDefault().Description, Status = false });
                }
                await signInManager.RefreshSignInAsync(user);
                return Ok(new ResponseViewModel { Message = "Password changed successfully!", Status = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Status = false, Message = ex.Message });
            }

        }
    }    
}
