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
    public interface IFieldScientistAnalysisFMEnvService
    {
        Task<List<FMENVTemplate>> CreateFMENVSampleTemplate(FMENVTemplatesVm model);
        Task<List<FMENVTemplate>> GetAllFmenvTemplates();
        Task<string> UpdateFMenvTemplate(UpdateFMENVTemplateVm model);
        Task<string> DeleteFMENVTemplate(Guid templateId);

        Task<FieldScientistFMEnvTestsVM> GetFMEnvTemplates(FMEnvsCreateRequestVM model);
        Task<SampleFMEnvTestVM> GetFMEnvSpecificTestTemplatesByName(FMEnvTestName model);
        Task<string> AddOrUpdateFMEnvSpecificTestTestResultByName(FMEnvTestResult model);
        Task<string> AddFMEnvTestResult(AddFMEnvTestResults model);
        Task<AnalysisDaysCount> FMEnvTestCountPastSevenDays();

        PagedResponse<AllFieldScientistFMEnveTestsVM> GetAllFMEnvTest(int pageSize, int pageNumber, string keyword);
        PagedResponse<AllFieldScientistFMEnveTestsVM> GetAllFMEnvTestByAnalystId(int pageSize, int pageNumber, string keyword);
        Task<byte[]> DownloadFMEnvTestToExcel(FmEnvResultVM model);
        Task<ImageVM> DownloadFMEnvTestPhoto(FmEnvResultVM model);
        Task<AllFieldScientistFMEnveTestsVM> GetFMEnvTestById(FmEnvResultVM model);
    }
}
