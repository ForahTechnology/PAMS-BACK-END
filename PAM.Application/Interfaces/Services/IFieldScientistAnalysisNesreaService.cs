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
    public interface IFieldScientistAnalysisNesreaService
    {
        Task<long> AddLocationByClientId(CreateSampleLocationRequest model);
        Task<List<SampleLocationVM>> GetAllSampleLocationsByClientId(Guid clientId);
        Task<SampleLocationVM> UpdateSampleLocation(UpdateSampleLocationRequest updateSampleLocationRequest);
        Task<long> DeleteSampleLocation(long sampleLocationId);
        int LocationsCount();

        Task<List<NESREATemplate>> CreateNesreaSampleTemplate(NesreaTemplatesVm model);
        Task<List<NESREATemplate>> GetAllNesreaTemplates();
        Task<string> UpdateNesreaTemplate(UpdateNesreaTemplateVm model);
        Task<string> DeleteNesreaTemplate(Guid templateId);

        Task<FieldScientistNesreaTestsVM> GetNesreaTemplates(CreateNesreamVM model);
        Task<SampleTestVM> GetNesreaSpecificTestTemplatesByName(NesreaTestName model);
        Task<string> AddOrUpdateNesreaSpecificTestTestResultByName(NesreaTestResult model);
        Task<string> AddNesreaTestResult(AddNesreaTestResults model);
        Task<AnalysisDaysCount> NesreaTestCountPastSevenDays();

        PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTest(int pageSize, int pageNumber, string keyword);
        PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTestByClientName(int pageSize, int pageNumber, string keyword);
        PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTestByAnalystId(int pageSize, int pageNumber, string keyword);
        Task<AllFieldScientistNesreaTestsVM> GetNesreaTestById(NesreamResultVM model);
        Task<byte[]> DownloadNesreaTestToExcel(NesreamResultVM model);
        Task<ImageVM> DownloadNesreaTestPhoto(NesreamResultVM model);
    }
}
