using PAMS.Domain.Common;
using PAMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class ClientSample : BaseEntity
    { 
        public Guid ClientID { get; set; }
        public Guid SampleTemplateID { get; set; }
        public PhysicoChemicalAnalysisType   Type { get; set; }
    }
}
