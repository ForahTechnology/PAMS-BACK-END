using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CodeActivationDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
