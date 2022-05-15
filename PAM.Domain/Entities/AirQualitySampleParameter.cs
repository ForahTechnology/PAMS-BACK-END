using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class AirQualitySampleParameter : BaseEntity
    {
        [JsonIgnore]
        public AirQualitySample AirQualitySample { get; set; }
        public Guid AirQualitySampleId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
    }
}
