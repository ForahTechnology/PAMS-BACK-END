using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class Sampling : BaseEntity
    {
        public Guid? ClientID { get; set; }
        [JsonIgnore]
        public Client Client { get; set; }
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime SamplingTime { get; set; }
        public DateTime SamplingDate { get; set; }
        public Status Status { get; set; }
        public string GPSLocation { get; set; }
        public bool IsReportCreated { get; set; }
        public string Picture { get; set; }
        public ICollection<MicroBiologicalAnalysis> MicroBiologicalAnalyses { get; set; }
        public ICollection<PhysicoChemicalAnalysis> PhysicoChemicalAnalyses { get; set; }
    }

    public enum Status
    {
        Pending,
        Sent
    }
}
