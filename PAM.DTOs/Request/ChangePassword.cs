using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
