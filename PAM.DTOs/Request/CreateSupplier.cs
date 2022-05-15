using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CreateSupplier
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
