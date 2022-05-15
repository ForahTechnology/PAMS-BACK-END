using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAMS.DTOs.Response.FieldScientistResponse
{
    public class FieldScientistFMEnvTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string Name { get; set; }
        public List<FMENVTemplateDto> FMENVSamples { get; set; }

        public static implicit operator FieldScientistFMEnvTestsVM(FMENVField model)
        {
            return model == null
                ? null
                : new FieldScientistFMEnvTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    Name = model.Name,
                    FMENVSamples = model.FMENVSamples.Select(x => (FMENVTemplateDto)x).ToList()
                };

        }
    }

    public class SampleFMEnvTestVM
    {
        public long Id { get; set; }

        public long FMEnvFieldId { get; set; }

        public string TestName { get; set; }

        public string TestUnit { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }
}
