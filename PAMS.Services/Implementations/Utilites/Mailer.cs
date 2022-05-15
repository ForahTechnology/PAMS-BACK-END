using PAMS.Application.Helpers;
using PAMS.Application.Interfaces.Utilities;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations.Utilities
{
    public class Mailer : IMailer
    {
        public static MailMessage mail = new MailMessage();
        private readonly IMajorUtility utility;
        public Mailer(
            IMajorUtility utility
            )
        {
            this.utility = utility;
        }
        public async Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent)
        {
            if (IsValidEmail(emailAddress))
            {
                mail.From = new MailAddress("sethlabltd@gmail.com");
                mail.Subject = subject;
                mail.To.Add(emailAddress);

                mail.IsBodyHtml = true;
                mail.Body = htmlContent;
                

                SmtpClient client = new SmtpClient();
              
                client.Host = "smtp.sendgrid.net";
                client.Port = 465;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("apikey", "#Fix");

                try
                {
                    await client.SendMailAsync(mail);
                    return new ResponseViewModel { Status = true, Message="Email sent!" };
                }
                catch (Exception ex)
                {
                    return new ResponseViewModel { Status = false, Message = ex.Message };
                }
            }
            return new ResponseViewModel { Status = false, Message="invalid email address!" };
        }

        public async Task<ResponseViewModel> SendMail(string emailAddress, string subject, string htmlContent, string attatchementPath)
        {

            if (IsValidEmail(emailAddress))
            {
                mail.From = new MailAddress("sethlabltd@gmail.com");
                mail.Subject = subject;
                mail.To.Add(emailAddress);
                var attatchment = new Attachment(attatchementPath);
                attatchment.Name = "Invoice.pdf";
                mail.IsBodyHtml = true;
                mail.Body = htmlContent;
                mail.Attachments.Add(attatchment);
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("sethlabltd@gmail.com", "@@SethL@b2021@");

                try
                {
                    client.Send(mail);
                    attatchment.Dispose();
                    utility.Logger("Email sent!");
                    return new ResponseViewModel { Status = true, Message = "Email sent!" };
                }
                catch (Exception ex)
                {
                    attatchment.Dispose();
                    utility.Logger($"An error occoured - {ex.Message}");
                    return new ResponseViewModel { Status = false, Message = ex.Message };
                }
            }
            return new ResponseViewModel { Status = false, Message = "invalid email address!" };
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
    }
}
