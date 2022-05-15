using System;
using System.Collections.Generic;
using PAMS.Application.Interfaces.Services;
using System.Threading.Tasks;
using PAMS.Domain.Entities;
using PAMS.Application.Interfaces.Persistence;
using System.Linq;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using Newtonsoft.Json;
using Utility = PAMS.Application.Helpers.Utility;
using PAMS.Application.Interfaces.Utilities;
using System.Threading;
using PAMS.Domain.Enums;
using PAMS.Services.Implementations.Utilites;

namespace PAMS.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IStoreManager<Invoice> invoiceStoreManager;
        private readonly IStoreManager<Client> clientStoreManager;
        private readonly ISettingsService settingsService;
        //private readonly IMailer mailer;
        private readonly IMailService _mailer;

        public InvoiceService(
            IStoreManager<Invoice> InvoiceStoreManager,
            IStoreManager<Client> clientStoreManager,
            ISettingsService settingsService,
            IMailService mailer
            //IMailer mailer
            )
        {
            this.invoiceStoreManager = InvoiceStoreManager;
            this.clientStoreManager = clientStoreManager;
            this.settingsService = settingsService;
            _mailer = mailer;
            //this.mailer = mailer;
        }
        public async Task<Invoice> CreateInvoice(Invoice invoice)
        {
            try
            {
                await invoiceStoreManager.DataStore.Add(invoice);
                await invoiceStoreManager.Save();
                return invoice;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteInvoice(Guid invoiceId)
        {
            var response = await invoiceStoreManager.DataStore.Delete(invoiceId);
            await invoiceStoreManager.Save();
            return response;
        }

        public async Task<IEnumerable<InvoiceResponse>> GetInvoice()
        {
            var response = new List<InvoiceResponse>();
            var invoices = await invoiceStoreManager.DataStore.GetAll();
            foreach (var invoice in invoices)
            {
                var items = JsonConvert.DeserializeObject<List<InvoiceItem>>(invoice.Items);
                var client = await clientStoreManager.DataStore.GetById(invoice.ClientID);
                response.Add(new InvoiceResponse
                {
                    ID = invoice.ID,
                    Amount = items.Any() ? items.Sum(i => i.Amount).ToString() : null,
                    ClientID = invoice.ClientID,
                    ClientName = client != null ? client.Name : "undefined",
                    ClientEmail = client.Email,
                    ClientAddress = client.Address,
                    DateGenerated = invoice.DateGenerated.ToShortDateString(),
                    DueDate = invoice.DueDate.ToShortDateString(),
                    Items = items,
                    Status = invoice.Paid ? "Paid" : "Pending",
                    InvoiceNumber = invoice.InoviceNumber,
                });
            }
            return response;
        }

        public IEnumerable<InvoiceResponse> GetClientInvoice(Guid clientId)
        {
            var response = new List<InvoiceResponse>();
            var invoices = invoiceStoreManager.DataStore.GetAllQuery().Where(invoice => invoice.ClientID == clientId).ToList();
            foreach (var invoice in invoices)
            {
                var items = JsonConvert.DeserializeObject<List<InvoiceItem>>(invoice.Items);
                var client = clientStoreManager.DataStore.GetById(invoice.ClientID).Result;
                response.Add(new InvoiceResponse
                {
                    ID = invoice.ID,
                    Amount = items.Any() ? items.Sum(i => i.Amount).ToString() : null,
                    ClientID = invoice.ClientID,
                    ClientName = client != null ? client.Name : "undefined",
                    ClientEmail = client.Email,
                    ClientAddress = client.Address,
                    DateGenerated = invoice.DateGenerated.ToShortDateString(),
                    DueDate = invoice.DueDate.ToShortDateString(),
                    Items = items,
                    Status = invoice.Paid ? "Paid" : "Pending",
                    InvoiceNumber = invoice.InoviceNumber,
                });
            }
            return response;
        }

        public async Task<InvoiceResponse> GetInvoice(Guid invoiceId)
        {
            var invoice = await invoiceStoreManager.DataStore.GetById(invoiceId);
            if (invoice != null)
            {
                var items = JsonConvert.DeserializeObject<List<InvoiceItem>>(invoice.Items);
                var client = await clientStoreManager.DataStore.GetById(invoice.ClientID);
                var response = new InvoiceResponse
                {
                    ID = invoice.ID,
                    Amount = items.Any() ? items.Sum(i => i.Amount).ToString() : null,
                    ClientID = invoice.ClientID,
                    ClientName = client != null ? client.Name : "undefined",
                    ClientEmail = client.Email,
                    ClientAddress = client.Address,
                    DateGenerated = invoice.DateGenerated.ToShortDateString(),
                    DueDate = invoice.DueDate.ToShortDateString(),
                    Items = items,
                    Status = invoice.Paid ? "Paid" : "Pending",
                    InvoiceNumber = invoice.InoviceNumber,
                };
                return response; 
            }
            return null;
        }

        public async Task<InvoiceResponse> UpdateInvoice(UpdateInvoice model)
        {
            var invoice = await invoiceStoreManager.DataStore.GetById(model.ID);
            if (invoice != null)
            {
                invoice.Items = model.Items.Any()? JsonConvert.SerializeObject(model.Items):"";
                invoice.DueDate = model.DueDate;
                invoice.Paid = model.Paid;
                invoiceStoreManager.DataStore.Update(invoice);
                await invoiceStoreManager.Save();
                return await GetInvoice(invoice.ID); 
            }
            return null;
        }

        public async Task<bool> Pay(Guid invoiceId)
        {
            var invoice = await invoiceStoreManager.DataStore.GetById(invoiceId);
            if (invoice == null) return false;
            if (!invoice.Paid)
            {
                invoice.Paid = true;
            }
            else
            {
                invoice.Paid = false;
            }
            invoiceStoreManager.DataStore.Update(invoice);
            await invoiceStoreManager.Save();
            return true;
        }

        public async Task<bool> SendInvoice(InvoiceMail invoiceMail)
        {
            var invoice = await GetInvoice(invoiceMail.InvoiceId);

            var content = Utility.GenerateInvoiceEmailContent(invoice, settingsService.GetSettings().Result.VAT,invoiceMail.Invoice,invoiceMail.InvoiceItems);

            //Sending email
            var response = await _mailer.SendMail(invoice.ClientEmail, content[0], content[1]);
            return response.Status;
        }

        public async Task<bool> SendInvoice(InvoiceMail invoiceMail,string attatchementPath)
        {
            var invoice = await GetInvoice(invoiceMail.InvoiceId);

            var content = Utility.GenerateInvoiceEmailContent(invoice, settingsService.GetSettings().Result.VAT, invoiceMail.Invoice, invoiceMail.InvoiceItems);

            //Sending email
            var response = await _mailer.SendMail(invoice.ClientEmail, content[0], content[1], attatchementPath);
            return response.Status;
        }

        public async Task<string> GetInvoiceFileString(InvoiceMail invoiceMail)
        {
            var invoice = await GetInvoice(invoiceMail.InvoiceId);
            if (invoice==null) return string.Empty; 
            
            var content = Utility.GenerateInvoiceEmailContent(invoice, settingsService.GetSettings().Result.VAT, invoiceMail.Invoice, invoiceMail.InvoiceItems);

            return content[1];
        }

        public async Task<IEnumerable<RecentInvoiceResponse>> GetClientRecentInvoices()
        {
            var invoinceResponseList = new List<RecentInvoiceResponse>();
            var clients = await clientStoreManager.DataStore.GetAll();
            foreach (var client in clients)
            {
                var invoice = invoiceStoreManager.DataStore.GetAllQuery().Where(i => i.ClientID == client.ID).OrderByDescending(i => i.DueDate).FirstOrDefault();
                if (invoice != null)
                {
                    var items = JsonConvert.DeserializeObject<IEnumerable<InvoiceItem>>(invoice.Items);
                    var amountDue = items.Sum(i => i.Amount);
                    var invoiceRes = new RecentInvoiceResponse
                    {
                        AmountDue = amountDue,
                        CompanyName = client.Name,
                        ContactPerson = client.ContactPerson,
                        InvoiceId = invoice.ID,
                        Status = invoice.Paid ? InvoiceStatus.PAID.ToString() : invoice.DueDate < DateTime.Now ? InvoiceStatus.OVERDUE.ToString() : InvoiceStatus.ONTRACK.ToString(),
                    };
                    invoinceResponseList.Add(invoiceRes);
                }
            }
            return invoinceResponseList;
        }

        public async Task<decimal> GetTotalInvoiceAmount()
        {
            decimal totalAmount = 0.0m;
            var invoices = await invoiceStoreManager.DataStore.GetAll();
            foreach (var invoice in invoices)
            {
                var items = JsonConvert.DeserializeObject<IEnumerable<InvoiceItem>>(invoice.Items);
                totalAmount += items.Sum(i => i.Amount);
            }
            return totalAmount;
        }
    }
}
