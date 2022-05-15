using Microsoft.AspNetCore.Http;
using PAMS.Domain.Common;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAMS.DTOs.Request.FieldScientistRequest
{
    public class DprsResultVM
    {
        [Required]
        public long LocationId { get; set; }

        [Required]
        public long DprFieldId { get; set; }
    }

    public class DPRsCreateRequestVM
    {
        [Required]
        public long LocationId { get; set; }
    }

    public class DPRTestName
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long DPRFieldId { get; set; }
    }

    public class DPRTestResult
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long DPRFieldId { get; set; }

        public string TestLimit { get; set; }

        public string TestResult { get; set; }
    }

    public class AddDPRTestResults
    {
        public long samplePtId { get; set; }
        public long DPRFieldId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<UpdateResultVm> DPRTemplates { get; set; }
        [Required][ValidateLogo] public IFormFile Picture { get; set; }
    }

    public class DPRTemplatesVm
    {
        public List<DPRTemplateVm> DPRTemplates { get; set; }
    }

    public class DPRTemplateVm
    {
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }
    }

    public class DPRTemplateDto
    {
        public long DPRFieldId { get; set; }
        public long Id { get; set; }
        public string TestName { get; set; }
        public string TestUnit { get; set; }
        public string TestLimit { get; set; }
        public string TestResult { get; set; }

        public static implicit operator DPRTemplateDto(DPRFieldResult model)
        {
            return model == null
                ? null
                : new DPRTemplateDto
                {
                    DPRFieldId = model.DPRFieldId,
                    Id = model.Id,
                    TestUnit = model.TestUnit,
                    TestLimit = model.TestLimit,
                    TestResult = model.TestResult,
                    TestName = model.TestName,
                };

        }
    }

    public class UpdateTemplateVm : DPRTemplateVm
    {
        public Guid ID { get; set; }
    }

    public class UpdateResultVm : DPRTemplateVm
    {
        public long Id { get; set; }
    }
}
