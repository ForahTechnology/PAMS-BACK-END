using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Application.Helpers
{
    public interface IMajorUtility
    {

        /// <summary>
        /// Generate invoice pdf and return path
        /// </summary>
        /// <param name="htmlcontent"></param>
        /// <param name="documentName"></param>
        /// <param name="fileDestination"></param>
        /// <returns></returns>
        string GenerateInvoicePDF(string htmlcontent, string documentName, string fileDestination);

        /// <summary>
        /// This deletes a file whose is path is passed
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool DeleteFile(string filePath);
        /// <summary>
        /// This uploads any file to any specified location.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileDestination"></param>
        /// <returns></returns>
        string UploadFile(IFormFile file, string fileDestination);

        /// <summary>
        /// This uploads any file to any specified location specified from base64 string.
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="fileDestination"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string UploadFile(string base64, string fileDestination, string fileName);
        /// <summary>
        /// Returns byte array of the newly generated pdf.
        /// </summary>
        /// <param name="htmlcontent"></param>
        /// <param name="documentTitle"></param>
        /// <returns></returns>
        byte[] GeneratePDF(string htmlcontent, string documentTitle);
        /// <summary>
        /// This writes messages to file in 
        /// </summary>
        /// <param name="message"></param>
        void Logger(string message);
    }
}
