using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAMS.DTOs.Request
{
    public class CreateClientSample
    {
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public ICollection<ClientSampleTemplate> SampleTemplates { get; set; }
    }
}
