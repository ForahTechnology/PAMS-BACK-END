using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities.FieldScientistEntities
{
    public class FieldBaseEntity
    {
        public long Id { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public DateTime TimeModified { get; set; }            
    }
}
