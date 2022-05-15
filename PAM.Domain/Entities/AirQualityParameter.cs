using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class AirQualityParameter : BaseEntity
    {   
        [JsonIgnore]
        public AirQualityTemplate AirQualityTemplate { get; set; }
        public Guid AirQualityTemplateId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Limit { get; set; }
    }
}
