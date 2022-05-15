using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class RecentInvoiceResponse
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public decimal AmountDue { get; set; }
        public string Status { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
