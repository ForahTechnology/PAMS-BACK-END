using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Request.PhysicoTemplates;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IAnalysisService
    {
        Task<AirQualityParameter> AddAirQualityParameter(AddAirQualityParameterRequest request);
        Task<Guid> CreateAirQualitySample(AirQualitySampleRequest request);
        Task<Guid> CreateAirQualityTemplate(AirQualityTemplateRequest request);
        Task<List<MicroBiologicalAnalysisTemplate>> CreateDprMicrobialTemplateAsync(CreateMicrobial temp);
        Task<List<MicroBiologicalAnalysisTemplate>> CreateFmenvMicrobialTemplateAsync(CreateMicrobial temp);
        Task<List<MicroBiologicalAnalysisTemplate>> CreateNesreaMicrobialTemplateAsync(CreateMicrobial temp);
        Task<FMEnv> CreateTemplateAsync(FMEnv temp);
        Task<DPR> CreateTemplateAsync(DPR temp);
        Task<NESREA> CreateTemplateAsync(NESREA temp);
        Task<Guid> DeleteAirQualityParameter(Guid Id);
        Task<Guid> DeleteAirQualitySample(Guid sampleId);
        Task<Guid> DeleteAirQualityTemplate(Guid Id);
        Task<AirQualityParameter> GetAirQualityParameter(Guid Id);
        Task<AirQualitySample> GetAirQualitySampleByBy(Guid SampleId);
        AirQualityTemplate GetAirQualityTemplateById(Guid Id);
        Task<List<AirQualityParameter>> GetAllAirQualityParameter(Guid TemplateId);
        Task<IEnumerable<AirQualitySample>> GetAllAirQualitySamples();
        Task<List<AirQualityTemplate>> GetAllAirQualityTemplate();
        Task<AnalysisTemplateResponse<List<DPRResponse>>> GetAllDPRTemplates();
        Task<AnalysisTemplateResponse<List<FMENVResponse>>> GetAllFMEnvTemplates();
        Task<AnalysisTemplateResponse<List<NESREAResponse>>> GetAllNESREATemplates();
        Task<AnalysisTemplateResponse<DPRResponse>> GetDprTemplateById(Guid Id);
        Task<AnalysisTemplateResponse<FMENVResponse>> GetFMEnvTemplateById(Guid Id);
        Task<AnalysisTemplateResponse<NESREAResponse>> GetNesreaTemplateById(Guid Id);
        Task<AirQualityParameter> UpdateAirQualityParameter(AirQualityParameter parameter);
        Task<AirQualityTemplate> UpdateAirQualityTemplate(AirQualityTemplate template);
    }
}
