using Microsoft.Extensions.Configuration;
using PAMS.Application.Helpers;
using PAMS.DTOs.Response;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations.Utilites
{
    /// <summary>
    /// This handles email sending tasks in this application.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// This method checkts if the passed email is valid or not, using regular expression.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsValidEmail(string email);

        /// <summary>
        /// This method sends instant email message to the passed single email address.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent);

        /// <summary>
        /// This method sends instant email message to the passed single email address with attatchment.
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <param name="subject"></param>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent, string attatchementPath);
    }

    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        private readonly IMajorUtility _utility;

        public SendGridMailService(
            IConfiguration configuration,
            IMajorUtility utility)
        {
            _configuration = configuration;
            _utility = utility;
        }

        public bool IsValidEmail(string email)
        {
            if (email != null)
            {
                return Regex.IsMatch(email, @"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z")
                    && Regex.IsMatch(email, @"^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*");
            }
            else
            {
                return false;
            }
        }

        public async Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent)
        {
            if (IsValidEmail(emailAddress))
            {
                var sendgridKey = _configuration["SendgridKey"];
                var secret = _configuration["Secret"];
                var key = _configuration["IVKey"];
                var apiKey = Decrypt.DeCrypt(sendgridKey, secret, key );
                var client = new SendGridClient(apiKey);
                //this 'from' email must be the one you set up in send grid under setting -> mail setting not the sign up email
                var from = new EmailAddress("ekeneanolue@outlook.com", "PamsAPIKey");
                var to = new EmailAddress(emailAddress);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, htmlContent );

                try
                {
                    var respos = await client.SendEmailAsync(msg).ConfigureAwait(false);
                    return new ResponseViewModel { Status = true, Message = "Email sent!", ReturnObject = respos };
                }
                catch (Exception ex)
                {
                    return new ResponseViewModel { Status = false, Message = ex.Message };
                }
            }
            return new ResponseViewModel { Status = false, Message = "invalid email address!" };
        }

        public async Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent, string attatchementPath)
        {
            if (IsValidEmail(emailAddress))
            {
                var sendgridKey = _configuration["SendgridKey"];
                var secret = _configuration["Secret"];
                var key = _configuration["IVKey"];
                var apiKey = Decrypt.DeCrypt(sendgridKey, secret, key);
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("sethlabltd@gmail.com", "PAMS");
                var to = new EmailAddress(emailAddress);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, htmlContent);

                byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(attatchementPath));
                msg.Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Content = Convert.ToBase64String(byteData),
                        Filename = "Invoice.pdf",
                        Type = "application/pdf",
                        Disposition = "attachment"
                    }
                };


                try
                {
                    var respo = await client.SendEmailAsync(msg).ConfigureAwait(false);
                    _utility.Logger("Email sent!");
                    return new ResponseViewModel { Status = true, Message = "Email sent!" };
                }
                catch (Exception ex)
                {
                    _utility.Logger($"An error occoured - {ex.Message}");
                    return new ResponseViewModel { Status = false, Message = ex.Message };
                }
            }
            return new ResponseViewModel { Status = false, Message = "invalid email address!" };
        }
    }
}
