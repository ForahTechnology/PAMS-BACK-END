using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class AirQualityTemplateRequest
    {
        public string Name { get; set; }
        public List<AirQualityParameterRequest> Parameters { get; set; }
    }

    public class AirQualityParameterRequest
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
    }
    public class AddAirQualityParameterRequest
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
    }
}
