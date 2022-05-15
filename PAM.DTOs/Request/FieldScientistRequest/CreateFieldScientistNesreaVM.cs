using Microsoft.AspNetCore.Http;
using PAMS.Domain.Common;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAMS.DTOs.Request.FieldScientistRequest
{
    public class NesreamResultVM
    {
        [Required]
        public long LocationId { get; set; }

        [Required]
        public long NesreaFieldId { get; set; }
    }
    public class CreateNesreamVM
    {
        [Required]
        public long LocationId { get; set; }
    }

    public class NesreaTestName
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long NesreaFieldId { get; set; }
    }

    public class NesreaTestResult
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long NesreaFieldId { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }

    public class AddNesreaTestResults
    {
        public long samplePtId { get; set; }
        public long NesreaFieldId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<UpdateNesreaResultVm> NesreaTemplates { get; set; }
        [Required][ValidateLogo] public IFormFile Picture { get; set; }
    }

    public class NesreaTemplatesVm
    {
        public List<NesreaTemplateVm> NesreaTemplates { get; set; }
    }

    public class NesreaTemplateVm
    {
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
    }

    public class NesreaTemplateDto
    {
        public long NesreaFieldId { get; set; }
        public long Id { get; set; }
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }

        public static implicit operator NesreaTemplateDto(NESREAFieldResult model)
        {
            return model == null
                ? null
                : new NesreaTemplateDto
                {
                    NesreaFieldId = model.NESREAFieldId,
                    Id = model.Id,
                    TestUnit = model.TestUnit,
                    TestLimit = model.TestLimit,
                    TestResult = model.TestResult,
                    TestName = model.TestName,
                };

        }
    }

    public class UpdateNesreaTemplateVm : NesreaTemplateVm
    {
        public Guid ID { get; set; }
    }

    public class UpdateNesreaResultVm : NesreaTemplateVm
    {
        public long Id { get; set; }
    }
}
