using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class NESREA : BaseEntity
    {
        public string Name => "Physichochemical Analysis - NESREA";
        public DateTime CreateOn { get; set; }
        public ICollection<NESREAParameterTemplate> NESREAParameterTemplates { get; set; }
    }
}
