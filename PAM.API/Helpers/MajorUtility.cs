using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PAMS.Application.Helpers;
using Serilog;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace PAMS.API.Helpers
{
    /// <summary>
    /// Major utility class for taking care fast and easy chores
    /// </summary>
    public class MajorUtility : IMajorUtility
    {
        private readonly IConverter converter;
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="env"></param>
        public MajorUtility(
            IConverter converter,
            IWebHostEnvironment env
            )
        {
            this.converter = converter;
            this.env = env;
        }
        /// <summary>
        /// Generate invoice pdf and return path
        /// </summary>
        /// <param name="htmlcontent"></param>
        /// <param name="documentName"></param>
        /// <param name="fileDestination"></param>
        /// <returns></returns>
        public string GenerateInvoicePDF(string htmlcontent, string documentName, string fileDestination)
        {
            Logger($"Entered GenerateInvoicePDF!");
            try
            {

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Grayscale,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.LetterSmall,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Invoice",
                    Out = $"{fileDestination}\\{documentName}.pdf"
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlcontent,
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                Logger($"PDF object generated for creation!");
                converter.Convert(pdf);
                Logger($"PDF Generated! - GenerateInvoicePDF()");
                return globalSettings.Out;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger($"An error occoured - {ex.Message} : GenerateInvoicePDF()");
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Generate pdf and return byte array.
        /// </summary>
        /// <param name="htmlcontent"></param>
        /// <param name="documentTitle"></param>
        /// <returns></returns>
        public byte[] GeneratePDF(string htmlcontent,string documentTitle)
        {
            Logger($"Entered GeneratePDF!");
            try
            {
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Bottom = 5, Left = 0, Right = 0, Top = 0 },
                    DocumentTitle = documentTitle,
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlcontent,
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "", Line = false },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = false, Center = "" },
                    ProduceForms = true
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                Logger($"PDF object generated for creation! : GeneratePDF()");
                var pdfile = converter.Convert(pdf);
                Logger($"PDF Generated! - GeneratePDF()");
                return pdfile;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger($"An error occoured - {ex.Message} : GeneratePDF()");
                return new byte[0];
            }
        }

        /// <summary>
        /// This deletes a file whose is path is passed
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool DeleteFile(string filePath)
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(filePath))
                {
                    // If file found, delete it    
                    File.Delete(filePath);
                    Logger($"Deleted! - {filePath}");
                    Log.Debug($"Deleted! - {filePath}");
                    return true;
                }
                return false;
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
                Logger($"An error occured - {ioExp.Message}");
                Log.Debug($"An error occured - {ioExp.Message}");
                return false;
            }
        }
        /// <summary>
        /// This uploads any file to any specified location.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileDestination"></param>
        /// <returns></returns>
        public string UploadFile(IFormFile file, string fileDestination)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(fileDestination, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Logger($"{file.FileName} uploaded!");
            }
            return uniqueFileName;
        }

        /// <summary>
        /// This uploads any file to any specified location specified from base64 string.
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="fileDestination"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string UploadFile(string base64, string fileDestination,string fileName)
        {
            string uniqueFileName = null;
            if (!string.IsNullOrWhiteSpace(base64))
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" +fileName;
                string filePath = Path.Combine(fileDestination, uniqueFileName);

                byte[] fileByteArray = Convert.FromBase64String(base64);
                File.WriteAllBytes(filePath , fileByteArray);
                Logger($"{fileName} uploaded!");
            }
            return uniqueFileName;
        }
        /// <summary>
        /// This writes messages to file in 
        /// </summary>
        /// <param name="message"></param>
        public void Logger(string message)
        {
            using (StreamWriter file = new StreamWriter(Path.Combine(env.WebRootPath, "Files","Docs","doc.txt"), append:true))
            {
                file.NewLine = "\n";
                file.WriteLine($"{DateTime.Now} : {message}",ColorMode.Color);
            }
            Console.Title = "Info";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} : {message}");
            Console.ResetColor();
        }
    }
}