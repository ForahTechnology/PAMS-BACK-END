using Microsoft.AspNetCore.Http;
using PAMS.Domain.Common;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAMS.DTOs.Request.FieldScientistRequest
{
    public class FmEnvResultVM
    {
        [Required]
        public long LocationId { get; set; }

        [Required]
        public long FMEnvFieldId { get; set; }
    }

    public class FMEnvsCreateRequestVM
    {
        [Required]
        public long LocationId { get; set; }
    }

    public class FMEnvTestName
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long FMEnvFieldId { get; set; }
    }

    public class FMEnvTestResult
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long FMEnvFieldId { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }

    public class AddFMEnvTestResults
    {
        public long samplePtId { get; set; }
        public long FMEnvFieldId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<UpdateFMENVResultVm> FMENVTemplates { get; set; }
        [Required][ValidateLogo] public IFormFile Picture { get; set; }
    }

    public class FMENVTemplatesVm
    {
        public List<FMENVTemplateVm> FMENVTemplates { get; set; }
    }

    public class FMENVTemplateVm
    {
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
    }

    public class FMENVTemplateDto
    {
        public long FMENVFieldId { get; set; }
        public long Id { get; set; }
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }

        public static implicit operator FMENVTemplateDto(FMENVFieldResult model)
        {
            return model == null
                ? null
                : new FMENVTemplateDto
                {
                    FMENVFieldId = model.FMENVFieldId,
                    Id = model.Id,
                    TestUnit = model.TestUnit,
                    TestLimit = model.TestLimit,
                    TestResult = model.TestResult,
                    TestName = model.TestName,
                };

        }
    }

    public class UpdateFMENVTemplateVm : FMENVTemplateVm
    {
        public Guid ID { get; set; }
    }

    public class UpdateFMENVResultVm : FMENVTemplateVm
    {
        public long Id { get; set; }
    }
}
