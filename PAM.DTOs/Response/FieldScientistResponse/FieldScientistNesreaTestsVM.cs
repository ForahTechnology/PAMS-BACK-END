using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAMS.DTOs.Response.FieldScientistResponse
{
    public class FieldScientistNesreaTestsVM
    {
        public long Id { get; set; }
        public long SamplePointLocationId { get; set; }
        public string Name { get; set; }
        public List<NesreaTemplateDto> NesreaSamples { get; set; }

        public static implicit operator FieldScientistNesreaTestsVM(NESREAField model)
        {
            return model == null
                ? null
                : new FieldScientistNesreaTestsVM
                {
                    Id = model.Id,
                    SamplePointLocationId = model.SamplePointLocationId,
                    Name = model.Name,
                    NesreaSamples = model.NESREASamples.Select(x => (NesreaTemplateDto)x).ToList()  
                };
        }
    }

    public class SampleTestVM
    {
        public long Id { get; set; }

        public long NESREAFieldId { get; set; }

        public string TestName { get; set; }

        public string TestUnit { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }

}
