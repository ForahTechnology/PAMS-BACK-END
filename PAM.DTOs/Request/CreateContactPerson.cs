using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CreateContactPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ClientId { get; set; }
    }
}
