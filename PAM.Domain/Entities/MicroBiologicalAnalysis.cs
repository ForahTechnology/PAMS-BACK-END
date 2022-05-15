using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class MicroBiologicalAnalysis : BaseEntity
    {
        public Guid SamplingID { get; set; }
        [JsonIgnore]
        public Sampling  Sampling { get; set; }
        public string Microbial_Group { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
    }
}
