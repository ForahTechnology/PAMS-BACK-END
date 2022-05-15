using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Request.FieldScientistRequest
{
    public class UpdateSampleLocationRequest
    {
        public long SampleLocationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class CreateSampleLocationRequest
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
