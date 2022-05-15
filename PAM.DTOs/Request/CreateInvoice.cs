using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CreateInvoice
    {
        public List<InvoiceItem> Items { get; set; }
        public bool Paid { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ClientID { get; set; }
        public Guid SamplingID { get; set; }
    }
    public class InvoiceItem
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
