using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class FieldLocation : FieldBaseEntity
    {
        public long? NESREAFieldId { get; set; }
        public NESREAField NESREAField { get; set; }
        public long? FMENVFieldId { get; set; }
        public FMENVField FMENVField { get; set; }
        public long? DPRFieldId { get; set; }
        public DPRField DPRField { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
