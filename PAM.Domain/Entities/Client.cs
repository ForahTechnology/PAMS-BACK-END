using Newtonsoft.Json;
using PAMS.Domain.Common;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Sampling> Samplings { get; set; }
        public ICollection<Invoice>  Invoices { get; set; }
        public ICollection<SamplePointLocation> SamplePointLocations { get; set; }

        //[JsonIgnore]
        //public ContactPerson  ContactPerson { get; set; }
    }
}
