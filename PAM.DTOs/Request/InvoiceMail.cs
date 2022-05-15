using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class InvoiceMail
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceItems { get; set; }
        public string Invoice { get; set; }
    }
}
