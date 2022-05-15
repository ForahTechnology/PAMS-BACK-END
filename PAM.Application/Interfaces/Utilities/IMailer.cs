using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Utilities
{
    /// <summary>
    /// This handles email sending tasks in this application.
    /// </summary>
    public interface IMailer
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
}
