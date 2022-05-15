using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string RegistrationDate { get; set; }
    }
}
