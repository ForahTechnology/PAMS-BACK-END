using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response.FieldScientistResponse
{
    public class SampleLocationVM
    {
        public long SampleLocationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static implicit operator SampleLocationVM(SamplePointLocation model)
        {
            return model == null
                ? null
                : new SampleLocationVM
                {
                    Description = model.Description,
                    Name = model.Name,
                    SampleLocationId = model.Id
                };
        }
    }
}
