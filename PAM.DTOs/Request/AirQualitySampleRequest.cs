using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class AirQualitySampleRequest
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<AirQualityParameterRequest> Parameters { get; set; }
    }
}
