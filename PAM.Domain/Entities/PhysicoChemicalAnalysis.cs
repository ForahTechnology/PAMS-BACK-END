using Newtonsoft.Json;
using PAMS.Domain.Common;
using PAMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class PhysicoChemicalAnalysis : BaseEntity
    {

        public Guid SamplingID { get; set; }
        [JsonIgnore]
        public Sampling Sampling { get; set; }
        public string Test_Performed_And_Unit { get; set; }
        public string Result { get; set; }
        public string UC { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
        public PhysicoChemicalAnalysisType Type { get; set; }
    }
}
