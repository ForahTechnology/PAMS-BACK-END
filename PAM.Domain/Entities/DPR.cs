using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class DPR : BaseEntity
    {
        public string Name => "Physichochemical Analysis - DPR";
        public DateTime CreateOn { get; set; }
        public ICollection<DPRParameterTemplate>  DPRParameterTemplates { get; set; }
    }
}
