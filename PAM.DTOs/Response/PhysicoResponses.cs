using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class DPRResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateOn { get; set; }
        public ICollection<DPRParameterTemplate> PhysicoParameters { get; set; }
        public List<MicroBiologicalAnalysisTemplate> MicrobialParameters { get; set; }
    }
    
    public class NESREAResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateOn { get; set; }
        public ICollection<NESREAParameterTemplate> PhysicoParameters { get; set; }
        public List<MicroBiologicalAnalysisTemplate> MicrobialParameters { get; set; }
    }


    public class FMENVResponse
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string Sector { get; set; }
        public DateTime CreateOn { get; set; }
        public List<MicroBiologicalAnalysisTemplate> MicrobialParameters { get; set; }
        public ICollection<FMEnvParameterTemplate> PhysicoParameters { get; set; }
    }
}
