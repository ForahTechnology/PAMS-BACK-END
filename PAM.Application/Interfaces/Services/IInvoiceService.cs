using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoice(Invoice invoice);
        Task<IEnumerable<InvoiceResponse>> GetInvoice();
        Task<InvoiceResponse> GetInvoice(Guid invoiceId);
        Task<InvoiceResponse> UpdateInvoice(UpdateInvoice invoice);
        Task<bool> DeleteInvoice(Guid invoiceId);
        IEnumerable<InvoiceResponse> GetClientInvoice(Guid clientId);
        /// <summary>
        /// This mark an invoice status as paid.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        Task<bool> Pay(Guid invoiceId);
        /// <summary>
        /// This sends invoice to client.
        /// </summary>
        /// <param name="invoiceMail"></param>
        Task<bool> SendInvoice(InvoiceMail invoiceMail);
        /// <summary>
        /// This generates invoice email stream for attatchment.
        /// </summary>
        /// <param name="invoiceMail"></param>
        /// <returns></returns>
        Task<string> GetInvoiceFileString(InvoiceMail invoiceMail);
        /// <summary>
        /// This sends invoice with attatchment to client.
        /// </summary>
        /// <param name="invoiceMail"></param>
        /// <param name="attatchementPath"></param>
        /// <returns></returns>
        Task<bool> SendInvoice(InvoiceMail invoiceMail, string attatchementPath);
        Task<IEnumerable<RecentInvoiceResponse>> GetClientRecentInvoices();
        Task<decimal> GetTotalInvoiceAmount();
    }
}
