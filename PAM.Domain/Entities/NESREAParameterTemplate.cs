using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class NESREAParameterTemplate : BaseEntity
    {
        public Guid NESREAID { get; set; }
        public NESREA   NESREA { get; set; }
        public string Parameter { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
        public string Test_Performed_And_Unit => $"{Parameter}, {Unit}";
        public string Result { get; set; }
        public string UC { get; set; }
        public string Test_Method { get; set; }
    }
}
