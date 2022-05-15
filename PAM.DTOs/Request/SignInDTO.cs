using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class SignInDTO
    {
        //[Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
