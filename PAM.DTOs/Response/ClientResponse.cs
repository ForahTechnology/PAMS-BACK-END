using PAMS.Domain.Entities;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class ClientResponse
    {

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<SamplePointLocation> SamplePointLocations { get; set; }
        public ICollection<Sampling> Samplings { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public List<string> Templates { get; set; }
    }
}
