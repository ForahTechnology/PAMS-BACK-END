using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class AirQualityTemplate : BaseEntity
    {
        public string Name { get; set; }
        public List<AirQualityParameter> Parameters { get; set; }
    }
}