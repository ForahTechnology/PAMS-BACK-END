using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAMS.DTOs.Response.FieldScientistResponse
{
    public class FieldScientistDPRTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string Name { get; set; }
        public List<DPRTemplateDto> DprSamples { get; set; }
        
        public static implicit operator FieldScientistDPRTestsVM(DPRField model)
        {
            return model == null
                ? null
                : new FieldScientistDPRTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    Name = model.Name,
                    DprSamples = model.DPRSamples.Select(x => (DPRTemplateDto)x).ToList()
                };

        }
    }

    public class SampleDprTestVM
    {
        public long Id { get; set; }

        public long DPRFieldId { get; set; }

        public string TestName { get; set; }

        public string TestUnit { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }
}
