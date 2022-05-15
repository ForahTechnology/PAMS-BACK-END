using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class ClientSampleResponse
    {
        public List<FMENVResponse> FMEnvs { get; set; }
        public List<DPRResponse> DPRs { get; set; }
        public List<NESREAResponse>  NESREAs { get; set; }
    }
}
