using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class InvoiceResponse
    {
        public Guid ID { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientEmail { get; set; }
        public string DueDate { get; set; }
        public string DateGenerated { get; set; }
        public string Status { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid ClientID { get; set; }
        public string Amount { get; set; }
        public string FilePath { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
