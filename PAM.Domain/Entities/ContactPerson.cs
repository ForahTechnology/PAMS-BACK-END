using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class ContactPerson : BaseEntity
    {
        public Guid ClientId { get; set; }
        [JsonIgnore]
        public Client  Client { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
