using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class ParameterUploadResponse
    {
        public string Parameter { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
    }
}
