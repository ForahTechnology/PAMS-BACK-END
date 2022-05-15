using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class ClientMobile
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<SamplingResponse> Samplings { get; set; }
    }
}
