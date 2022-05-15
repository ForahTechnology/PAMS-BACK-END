using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class ResponseViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }
    }
}
