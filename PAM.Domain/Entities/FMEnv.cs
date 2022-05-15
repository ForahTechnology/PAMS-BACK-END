using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class FMEnv : BaseEntity
    {
        public string TypeName => "Physichochemical Analysis - FMEnv";
        public string SectorName { get; set; }
        public DateTime CreateOn { get; set; }
        public ICollection<FMEnvParameterTemplate>  Parameters { get; set; }
    }
}
