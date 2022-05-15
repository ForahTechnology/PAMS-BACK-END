using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class MicroBiologicalAnalysisTemplate : BaseEntity
    {
        public Guid PhysicoId { get; set; }
        public string Microbial_Group { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
    }
}
