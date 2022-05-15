using PAMS.Domain.Entities;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class SamplingResponse
    {
        public Guid ID { get; set; }
        public string StaffName { get; set; }
        public string SamplingTime { get; set; }
        public string SamplingDate { get; set; }
        public string ClientName { get; set; }
        public string Status { get; set; }
        public decimal GPSLong { get; set; }
        public decimal GPSLat { get; set; }
        public bool IsReportCreated { get; set; }
        public string Picture { get; set; }
        public List<MicroBiologicalAnalysisResponse> MicroBiologicals { get; set; }
        public List<PhysicoChemicalAnalysisResponse> PhysicoChemicals { get; set; }
    }
    public class MicroBiologicalAnalysisResponse
    {

        public Guid SamplingID { get; set; }
        public Guid Id { get; set; }
        public string Microbial_Group { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
    }
    public class PhysicoChemicalAnalysisResponse
    {
        public Guid SamplingID { get; set; }
        public Guid Id { get; set; }
        public string Test_Performed_And_Unit { get; set; }
        public string Result { get; set; }
        public string UC { get; set; }
        public string Limit { get; set; }
        public string Test_Method { get; set; }
        public PhysicoChemicalAnalysisType Type { get; set; }
    }
}
