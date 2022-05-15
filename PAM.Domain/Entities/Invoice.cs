using Newtonsoft.Json;
using PAMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string Items { get; set; }
        public bool Paid { get; set; }
        public string InoviceNumber { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateGenerated { get; set; }
        public Guid ClientID { get; set; }
        public Guid SamplingID { get; set; }
        [JsonIgnore]
        public Client Client { get; set; }
    }
}
