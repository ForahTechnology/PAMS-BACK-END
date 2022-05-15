using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class UpdateInvoice
    {
        public Guid ID { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public bool Paid { get; set; }
        public DateTime DueDate { get; set; }
    }
}
