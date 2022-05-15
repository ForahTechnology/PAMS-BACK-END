using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PAMS.Application.Helpers
{
    /// <summary>
    /// This static class Handles simple chores like generating mail contents for email confirmation, password reset and lots more.
    /// </summary>
    public static class Utility
    {

        /// <summary>
        /// This generates content for email confirmation upon registration.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public static string[] GenerateConfimationEmailContent(string link, string fileLocation)
        {
            var subject = "Account Confirmation";

            string htmlBody;
            using (var streamReader = File.OpenText(fileLocation))
            {
                htmlBody = streamReader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("#link#", link);


            var content = new[] { subject, htmlBody };
            return content;
        }

        /// <summary>
        ///     Generates subject and  Forgot password message content for mobile user
        /// </summary>
        /// <param name="link"></param>
        /// <param name="otp"></param>
        /// <param name="username"></param>
        /// <param name="fileLocation"></param>
        /// <returns> A list containing the subject and messagebody</returns>
        public static string[] GenerateForgortPasswordEmailContent(string otp, string username, string link, string fileLocation)
        {
            var subject = "Forgot Password";

            string htmlBody;
            using (var streamReader = File.OpenText(fileLocation))
            {
                htmlBody = streamReader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("#otp#", otp);
            htmlBody = htmlBody.Replace("#link#", link);
            htmlBody = htmlBody.Replace("#name#", username);


            var content = new[] { subject, htmlBody };
            return content;
        }

        /// <summary>
        /// This method generates a four digit OTP for users.
        /// </summary>
        /// <returns></returns>
        public static string GenerateOTP()
        {
            var random = new Random();
            var OTP = random.Next(2222, 9999).ToString();
            return OTP;
        }

        /// <summary>
        /// This method generates activation code and link for user registration.
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="link"></param>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public static string[] GenerateActivationEmailContent(string otp, string link, string fileLocation)
        {
            var subject = "Activation Code";

            string htmlBody;
            using (var streamReader = File.OpenText(fileLocation))
            {
                htmlBody = streamReader.ReadToEnd();
            }
            var code = otp.ToCharArray();
            htmlBody = htmlBody.Replace("#1#", code[0].ToString());
            htmlBody = htmlBody.Replace("#2#", code[1].ToString());
            htmlBody = htmlBody.Replace("#3#", code[2].ToString());
            htmlBody = htmlBody.Replace("#4#", code[3].ToString());
            htmlBody = htmlBody.Replace("#link#", link);
            var content = new[] { subject, htmlBody };
            return content;
        }
        /// <summary>
        /// This method generates invoice email content.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="fileLocation"></param>
        /// <param name="itemLocation"></param>
        /// <returns></returns>
        public static string[] GenerateInvoiceEmailContent(InvoiceResponse invoice, string vatPercent, string fileLocation, string itemLocation)
        {
            var subject = "SethLab - Invoice";

            string htmlBody;
            string invoiceItemBuilded = "";
            using (var streamReader = File.OpenText(fileLocation))
            {
                htmlBody = streamReader.ReadToEnd();
            }
            
            foreach (var item in invoice.Items)
            {
                string invoiceItems;
                using (var streamReader = File.OpenText(itemLocation))
                {
                    invoiceItems = streamReader.ReadToEnd();
                }
                invoiceItems = invoiceItems.Replace("#item", item.Name);
                invoiceItems = invoiceItems.Replace("#amount", item.Amount.ToString());
                invoiceItemBuilded = invoiceItemBuilded + invoiceItems;
            }
            var vat = (decimal.Parse(invoice.Amount) * decimal.Parse(vatPercent)) / 100;
            var total = decimal.Parse(invoice.Amount) + vat;
            htmlBody = htmlBody.Replace("#clientName", invoice.ClientName);
            htmlBody = htmlBody.Replace("#clientAddress", invoice.ClientAddress);
            htmlBody = htmlBody.Replace("#date", invoice.DateGenerated);
            htmlBody = htmlBody.Replace("#invoiceItems", invoiceItemBuilded);
            htmlBody = htmlBody.Replace("#duedate", invoice.DueDate);
            htmlBody = htmlBody.Replace("#invoiceNumber", invoice.InvoiceNumber);
            htmlBody = htmlBody.Replace("#tax", vat.ToString());
            htmlBody = htmlBody.Replace("#subtotal", invoice.Amount);
            htmlBody = htmlBody.Replace("#total", total.ToString());

            var content = new[] { subject, htmlBody };
            return content;
        }

        public static string[] GenerateReportContent(ReportResponse report, string lab_Director, string fileLocation,string netContent)
        {

            var subject = "SethLab - Report";

            string reportBody;
            string microItemBuilded = "";
            string physicoItemBuilded = "";

            using (var streamReader = File.OpenText(Path.Combine(fileLocation, "Report.html")))
            {
                reportBody = streamReader.ReadToEnd();
            }

            foreach (var item in report.MicroBiologicalAnalyses)
            {
                string microItems;
                using (var streamReader = File.OpenText(Path.Combine(fileLocation, "MicroItems.txt")))
                {
                    microItems = streamReader.ReadToEnd();
                }
                microItems = microItems.Replace("#microLimit#", item.Limit);
                microItems = microItems.Replace("#microGroup#", item.Microbial_Group);
                microItems = microItems.Replace("#microResult#", item.Result);
                microItems = microItems.Replace("#microTest_Method#", item.Test_Method);
                microItems = microItems.Replace("#microUnit#", item.Unit);

                microItemBuilded = microItemBuilded + microItems;
            }
            
            foreach (var item in report.PhysicoChemicalAnalyses)
            {
                string physicoItems;
                using (var streamReader = File.OpenText(Path.Combine(fileLocation, "PhysicoItems.txt")))
                {
                    physicoItems = streamReader.ReadToEnd();
                }

                physicoItems = physicoItems.Replace("#physicoLimit#", item.Limit);
                physicoItems = physicoItems.Replace("#physicoResult#", item.Result);
                physicoItems = physicoItems.Replace("#physicoTest_Method#", item.Test_Method);
                physicoItems = physicoItems.Replace("#physicoTest_Performed_And_Unit#", item.Test_Performed_And_Unit);
                physicoItems = physicoItems.Replace("#physicoUC#", item.UC);

                physicoItemBuilded = physicoItemBuilded + physicoItems;
            }

            reportBody = reportBody.Replace("#refNo#", report.Lab_Sample_Ref_Number);
            reportBody = reportBody.Replace("#certNo#", report.Certificate_Number);
            reportBody = reportBody.Replace("#clientName#", report.ClientName);
            reportBody = reportBody.Replace("#clientAddress#", report.ClientAddress);
            reportBody = reportBody.Replace("#humidity#", report.Lab_Env_Con.Humidity);
            reportBody = reportBody.Replace("#degree#", report.Lab_Env_Con.Temperature);
            reportBody = reportBody.Replace("#sameLabel#", report.Sample_Label);
            reportBody = reportBody.Replace("#sameType#", report.Sample_Label);
            reportBody = reportBody.Replace("#dateRec#", report.Date_Recieved_In_Lab.ToShortDateString());
            reportBody = reportBody.Replace("#batchNo#", report.Batch_Number);
            reportBody = reportBody.Replace("#dateAna#", report.Date_Analysed_In_Lab.ToShortDateString());
            reportBody = reportBody.Replace("#comment#", report.Comment);
            reportBody = reportBody.Replace("#labAnalyst#", report.Lab_Analyst);
            reportBody = reportBody.Replace(" #labDirector#", lab_Director);
            reportBody = reportBody.Replace("#netContent#", string.IsNullOrWhiteSpace(netContent)? "N/A":netContent);
            reportBody = reportBody.Replace("#physicoItems#", physicoItemBuilded);
            reportBody = reportBody.Replace("#microBialItems#", microItemBuilded);

            var content = new[] { subject, reportBody };
            return content;
        }
    }
}
