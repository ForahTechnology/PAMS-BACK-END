using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class SamplePointLocation : FieldBaseEntity
    {
        public Guid ClientID { get; set; }
        public Client Client { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<NESREAField> NESREAFields { get; set; }
        public ICollection<FMENVField> FMENVFields { get; set; }
        public ICollection<DPRField> DPRFields { get; set; }
    }
}
