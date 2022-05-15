using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class AirQualitySample : BaseEntity
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<AirQualitySampleParameter> Parameters { get; set; }
    }
}
