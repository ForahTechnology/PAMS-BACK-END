using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PAMS.Application.Interfaces.Services;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService invoiceService;
        private readonly IWebHostEnvironment env;
        private readonly IConverter converter;
        private readonly IMailer mailer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceService"></param>
        /// <param name="env"></param>
        /// <param name="converter"></param>
        /// <param name="mailer"></param>
        public InvoiceController(
            IInvoiceService invoiceService,
            IWebHostEnvironment env,
            IConverter converter,
            IMailer mailer
            )
        {
            this.invoiceService = invoiceService;
            this.env = env;
            this.converter = converter;
            this.mailer = mailer;
        }
        /// <summary>
        /// Generate invoice for client sampling.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("invoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoice model)
        {
            if (model.SamplingID != Guid.Empty && model.ClientID != Guid.Empty && model.Items.Any())
            {
                var random = new Random();
                var date = DateTime.Now;
                var invoice = new Invoice
                {
                    Items = model.Items.Any() ? JsonConvert.SerializeObject(model.Items):"",
                    ClientID = model.ClientID,
                    SamplingID = model.SamplingID,
                    DateGenerated = date,
                    DueDate = model.DueDate,
                    InoviceNumber = $"{random.Next(11, 99)}{date.Hour}{date.Day}{date.Minute}{date.Second}",
                    Paid = model.Paid
                };

                var response = await invoiceService.CreateInvoice(invoice);
                if (response != null)
                {
                    return Ok(new ResponseViewModel { Message = "Invoice generated!", Status = true, ReturnObject = response });
                }
                return Ok(new ResponseViewModel { Message = "An error occured while processing your request!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!", Status = false });
        }

        /// <summary>
        /// Get invoice by Id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("invoice/{invoiceId}")]
        public async   Task<IActionResult> GetInvoice([FromRoute] Guid invoiceId)
        {

            if (invoiceId != Guid.Empty)
            {
                var invoice = await invoiceService.GetInvoice(invoiceId);
                if (invoice!=null)
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = invoice });
                }
                return NotFound(new ResponseViewModel { Message = "Invoice could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid invoiceId supplied!" });
        }

        /// <summary>
        /// Get all avaialbe invoices
        /// </summary>
        /// <returns></returns>
        [HttpGet("invoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = await invoiceService.GetInvoice() });            
        }

        /// <summary>
        /// Get all invoice for a client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("invoices/{clientId}")]
        public async Task<IActionResult> GetClientInvoices(Guid clientId)
        {
            if (clientId != Guid.Empty)
            {
                var invoices =  invoiceService.GetClientInvoice(clientId);
                if (invoices.Any())
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = invoices });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid invoiceId supplied!" });
        }

        /// <summary>
        /// Update an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [HttpPut("invoice")]
        public async Task<IActionResult> UpdateInvoice(UpdateInvoice invoice)
        {
            if (invoice != null)
            {
                var updatedInvoice = await invoiceService.UpdateInvoice(invoice);
                if (updatedInvoice != null)
                {
                    return Ok(new ResponseViewModel { Message = "Invoice updated!", Status = true, ReturnObject = updatedInvoice });
                }
                return NotFound(new ResponseViewModel { Message = "Invoice could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid invoiceId supplied!" });
        }

        /// <summary>
        /// Delete an invoice record
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpDelete("invoice/{invoiceId}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] Guid invoiceId)
        {
            if (invoiceId != Guid.Empty)
            {
                var response = await invoiceService.DeleteInvoice(invoiceId);
                if (response)
                {
                    return Ok(new ResponseViewModel { Message = "Invoice deleted!", Status = true });
                }
                return NotFound(new ResponseViewModel { Message = "Invoice could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid invoiceId supplied!" });
        }

        /// <summary>
        /// Send invoice to the client email.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        //[HttpGet("sendinvoice/{invoiceId}")]
        //public async Task<IActionResult> SendInvoice(Guid invoiceId)
        //{
        //    if (invoiceId !=Guid.Empty)
        //    {

        //        var webRoot = env.WebRootPath;
        //        var templateLocation = Path.Combine(webRoot, "EmailTemplates", "Invoice.html");
        //        var itemLocation = Path.Combine(webRoot, "EmailTemplates", "InvoiceItems.txt");
        //        var mail = new InvoiceMail
        //        {
        //            InvoiceItems = itemLocation,
        //            Invoice = templateLocation,
        //            InvoiceId = invoiceId
        //        };
        //        var response = await invoiceService.SendInvoice(mail);
        //        if (response)
        //        {
        //            return Ok(new ResponseViewModel { Message = "Invoice sent to client", Status = true });
        //        }
        //        return BadRequest(new ResponseViewModel { Status = false, Message = "Client email is invalid!" });
        //    }
        //    return BadRequest(new ResponseViewModel { Status = false, Message = "Invalid invoice Id!" });
        //}

        /// <summary>
        /// Mark an invoice as paid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPatch("pay/{invoiceId}")]
        public async Task<IActionResult> Pay([FromRoute]Guid invoiceId)
        {
            if (invoiceId != Guid.Empty)
            {                
                var response = await invoiceService.Pay(invoiceId);
                if (response)
                {
                    return Ok(new ResponseViewModel { Message = "Success!", Status = true });
                }
                return NotFound(new ResponseViewModel { Message = "Invoice could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid invoiceId supplied!" });
        }

        /// <summary>
        /// Send invoice to the client email.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("sendinvoice/{invoiceId}")]
        public async Task<IActionResult> SendInvoice(Guid invoiceId)
        {
            if (invoiceId != Guid.Empty)
            {
                var webRoot = env.WebRootPath;
                var templateLocation = Path.Combine(webRoot, "EmailTemplates", "InvoicePrintable.html");
                var itemLocation = Path.Combine(webRoot, "EmailTemplates", "InvoiceItems.txt");
                var fileDestination = Path.Combine(webRoot, "Files", "Docs");
                var mail = new InvoiceMail
                {
                    InvoiceItems = itemLocation,
                    Invoice = templateLocation,
                    InvoiceId = invoiceId
                };
                //Getting invoice template stream
                var response = await invoiceService.GetInvoiceFileString(mail);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var clientName = invoiceService.GetInvoice(invoiceId).Result.ClientName;
                    var fileName = $"Invoice_{clientName}_{DateTime.Now.ToString()}";
                    fileName = fileName.Replace(@"\", "_");
                    fileName = fileName.Replace(" ", "_");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    var utility = new Helpers.MajorUtility(converter, env);
                    //Generating PDF version of invoice
                    var link = utility.GenerateInvoicePDF(response, fileName, fileDestination);
                    Log.Debug($"PDF Generated!, Link received!");
                    //sending generated invoice as attatchment to client.
                    mail.Invoice = Path.Combine(webRoot, "EmailTemplates", "Invoice.html");
                    await invoiceService.SendInvoice(mail, link);
                    Log.Debug($"Invoice  sent!");
                    utility.Logger($"Invoice sent!");
                    utility.DeleteFile(link);
                    utility.Logger($"Generated invoice file deleted!");
                    Log.Debug($"Generated invoice file deleted!");
                    return Ok(new ResponseViewModel { Message = "Invoice sent!", Status = true});
                }
                return NotFound(new ResponseViewModel { Status = false, Message = "Invoice could not be found!" });
            }
            return BadRequest(new ResponseViewModel { Status = false, Message = "Invalid invoice Id!" });
        }

        /// <summary>
        /// Get pdf file of invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("downloadinvoice/{invoiceId}")]
        public async Task<IActionResult> DownloadInvoice(Guid invoiceId)
        {

            if (invoiceId != Guid.Empty)
            {
                var webRoot = env.WebRootPath;
                var templateLocation = Path.Combine(webRoot, "EmailTemplates", "InvoicePrintable.html");
                var itemLocation = Path.Combine(webRoot, "EmailTemplates", "InvoiceItems.txt");
                var fileDestination = Path.Combine(webRoot, "Files", "Docs");
                var mail = new InvoiceMail
                {
                    InvoiceItems = itemLocation,
                    Invoice = templateLocation,
                    InvoiceId = invoiceId
                };
                //Getting invoice template stream
                var response = await invoiceService.GetInvoiceFileString(mail);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var clientName = invoiceService.GetInvoice(invoiceId).Result.ClientName;
                    var fileName = $"Invoice_{clientName}_{DateTime.Now.ToString()}";
                    fileName = fileName.Replace(@"\", "_");
                    fileName = fileName.Replace(" ", "_");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    var utility = new Helpers.MajorUtility(converter, env);
                    try
                    {
                        //Generating PDF version of invoice
                        var file = utility.GeneratePDF(response, "Invoice");
                        utility.Logger($"{fileName} downloaded!");
                        return File(file, "application/octet-stream", $"{fileName}.pdf");
                    }
                    catch (Exception ex)
                    {
                        utility.Logger($"An error occoured - {ex.Message}");
                        return Content(ex.Message);
                    }
                }
                return NotFound(new ResponseViewModel { Status = false, Message = "Invoice could not be found!" });
            }
            return BadRequest(new ResponseViewModel { Status = false, Message = "Invalid invoice Id!" });
            
        }
    }
}
