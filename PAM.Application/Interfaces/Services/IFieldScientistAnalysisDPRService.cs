using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using PAMS.DTOs.Response;
using PAMS.DTOs.Response.FieldScientistResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IFieldScientistAnalysisDPRService
    {
        Task<string> DeleteDPRTemplate(Guid templateId);
        Task<string> UpdateDPRTemplate(UpdateTemplateVm model);
        Task<List<DPRTemplate>> GetAllDPRTemplates();
        Task<List<DPRTemplate>> CreateDPRSampleTemplate(DPRTemplatesVm model);

        Task<FieldScientistDPRTestsVM> GetDPRTemplates(DPRsCreateRequestVM model);
        Task<SampleDprTestVM> GetDPRSpecificTestTemplatesByName(DPRTestName model);
        Task<string> AddOrUpdateDPRSpecificTestTestResultByName(DPRTestResult model);
        Task<string> AddDPRTestResult(AddDPRTestResults model);
        Task<AnalysisDaysCount> DPRTestCountPastSevenDays();

        PagedResponse<AllFieldScientistDPRTestsVM> GetAllDPRTest(int pageSize, int pageNumber, string keyword);
        PagedResponse<AllFieldScientistDPRTestsVM> GetAllDPRTestByAnalystId(int pageSize, int pageNumber, string keyword);
        Task<byte[]> DownloadDPRTestToExcel(DprsResultVM model);
        Task<ImageVM> DownloadDPRTestPhoto(DprsResultVM model);
        Task<AllFieldScientistDPRTestsVM> GetDPRTestById(DprsResultVM model);
    }
}
