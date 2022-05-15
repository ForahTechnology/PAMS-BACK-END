using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class RemoveClientSample
    {
        public Guid ClientId { get; set; }
        public Guid SampleTemplateId { get; set; }
    }
}
