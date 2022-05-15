using Microsoft.EntityFrameworkCore;
using PAMS.Application.Handlers;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Request.PhysicoTemplates;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IStoreManager<FMEnv> fmenvManager;
        private readonly IStoreManager<FMEnvParameterTemplate> fmenvTemp;
        private readonly IStoreManager<NESREA> nesreaStore;
        private readonly IStoreManager<NESREAParameterTemplate> nesreaTemp;
        private readonly IStoreManager<DPR> dprStore;
        private readonly IStoreManager<DPRParameterTemplate> dprTemp;
        private readonly IStoreManager<MicroBiologicalAnalysisTemplate> microbialStore;
        private readonly IStoreManager<AirQualityTemplate> airQualityStore;
        private readonly IStoreManager<AirQualityParameter> airQualityParamStore;
        private readonly IStoreManager<AirQualitySample> airQualitySampleStore;
        private readonly IStoreManager<AirQualitySampleParameter> airQualitySampleParameterStore;

        public AnalysisService(
            IStoreManager<FMEnv> fmenvManager,
            IStoreManager<FMEnvParameterTemplate> fmenvTemp,
            IStoreManager<NESREA>  nesreaStore,
            IStoreManager<NESREAParameterTemplate> nesreaTemp,
            IStoreManager<DPR> dprStore,
            IStoreManager<DPRParameterTemplate>dprTemp,
            IStoreManager<MicroBiologicalAnalysisTemplate> microbialStore,
            IStoreManager<AirQualityTemplate>airQualityStore,
            IStoreManager<AirQualityParameter>airQualityParamStore,
            IStoreManager<AirQualitySample> airQualitySampleStore,
            IStoreManager<AirQualitySampleParameter> airQualitySampleParameterStore
            )
        {
            this.fmenvManager = fmenvManager;
            this.fmenvTemp = fmenvTemp;
            this.nesreaStore = nesreaStore;
            this.nesreaTemp = nesreaTemp;
            this.dprStore = dprStore;
            this.dprTemp = dprTemp;
            this.microbialStore = microbialStore;
            this.airQualityStore = airQualityStore;
            this.airQualityParamStore = airQualityParamStore;
            this.airQualitySampleStore = airQualitySampleStore;
            this.airQualitySampleParameterStore = airQualitySampleParameterStore;
        }
        public async Task<FMEnv> CreateTemplateAsync(FMEnv temp)
        {
            await fmenvManager.DataStore.Add(temp);
            foreach (var param in temp.Parameters)
            {
                param.FMEnvID = temp.ID;
            }
            await fmenvTemp.DataStore.AddRange(temp.Parameters.ToList());
            await fmenvManager.Save();
            return temp;
        }

        public async Task<DPR> CreateTemplateAsync(DPR temp)
        {
            await dprStore.DataStore.Add(temp);
            foreach (var param in temp.DPRParameterTemplates)
            {
                param.DPRID = temp.ID;
            }
            await dprTemp.DataStore.AddRange(temp.DPRParameterTemplates.ToList());
            await fmenvManager.Save();
            return temp;
        }

        public async Task<NESREA> CreateTemplateAsync(NESREA temp)
        {
            await nesreaStore.DataStore.Add(temp);
            foreach (var param in temp.NESREAParameterTemplates)
            {
                param.NESREAID = temp.ID;
            }
            await nesreaTemp.DataStore.AddRange(temp.NESREAParameterTemplates.ToList());
            await fmenvManager.Save();
            return temp;
        }
        
        public async Task<List<MicroBiologicalAnalysisTemplate>> CreateNesreaMicrobialTemplateAsync(CreateMicrobial temp)
        {
            NESREA physico = null;
            //Checking if the physico exists
            physico = nesreaStore.DataStore.GetAllQuery().FirstOrDefault(p=>p.ID == temp.PhysicoId);
            if (physico == null) throw new KeyNotFoundException($"Template with id '{temp.PhysicoId}' does not exist");
            var microbialList = new List<MicroBiologicalAnalysisTemplate>();
            foreach (var param in temp.Parameters)
            {
                microbialList.Add(new MicroBiologicalAnalysisTemplate
                {
                    Limit = param.Limit,
                    Unit = param.Unit,
                    Microbial_Group = param.Parameter,
                    PhysicoId = temp.PhysicoId
                });
            }
            await microbialStore.DataStore.AddRange(microbialList);
            await microbialStore.Save();
            return microbialList;
        }
        
        public async Task<List<MicroBiologicalAnalysisTemplate>> CreateFmenvMicrobialTemplateAsync(CreateMicrobial temp)
        {
            FMEnv physico = null;
            //Checking if the physico exists


            physico = fmenvManager.DataStore.GetAllQuery().FirstOrDefault(p=>p.ID == temp.PhysicoId);
            if (physico == null) throw new KeyNotFoundException($"Template with id '{temp.PhysicoId}' does not exist");
            var microbialList = new List<MicroBiologicalAnalysisTemplate>();
            foreach (var param in temp.Parameters)
            {
                microbialList.Add(new MicroBiologicalAnalysisTemplate
                {
                    Limit = param.Limit,
                    Unit = param.Unit,
                    Microbial_Group = param.Parameter,
                    PhysicoId = temp.PhysicoId
                });
            }
            await microbialStore.DataStore.AddRange(microbialList);
            await microbialStore.Save();
            return microbialList;
        }
        
        public async Task<List<MicroBiologicalAnalysisTemplate>> CreateDprMicrobialTemplateAsync(CreateMicrobial temp)
        {
            DPR physico = null;
            //Checking if the physico exists


            physico = dprStore.DataStore.GetAllQuery().FirstOrDefault(p=>p.ID == temp.PhysicoId);
            if (physico == null) throw new KeyNotFoundException($"Template with id '{temp.PhysicoId}' does not exit");
            var microbialList = new List<MicroBiologicalAnalysisTemplate>();
            foreach (var param in temp.Parameters)
            {
                microbialList.Add(new MicroBiologicalAnalysisTemplate
                {
                    Limit = param.Limit,
                    Unit = param.Unit,
                    Microbial_Group = param.Parameter,
                    PhysicoId = temp.PhysicoId
                });
            }
            await microbialStore.DataStore.AddRange(microbialList);
            await microbialStore.Save();
            return microbialList;
        }

        public async Task<AnalysisTemplateResponse<List<FMENVResponse>>> GetAllFMEnvTemplates()
        {
            var fmenvlist = await fmenvManager.DataStore.GetAllQuery().Include("Parameters").ToListAsync();
            var response = new List<FMENVResponse>();
            if (fmenvlist != null||fmenvlist.Any())
            {
                foreach (var temp in fmenvlist)
                {
                    var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == temp.ID).ToListAsync();
                    response.Add(new FMENVResponse
                    {
                        CreateOn = temp.CreateOn,
                        Id = temp.ID,
                        MicrobialParameters = microbs,
                        PhysicoParameters = temp.Parameters,
                        TypeName = temp.TypeName,
                        Sector = temp.SectorName
                    });
                }
            }
            return new AnalysisTemplateResponse<List<FMENVResponse>> { Analysis = response };
        }
        
        public async Task<AnalysisTemplateResponse<FMENVResponse>> GetFMEnvTemplateById(Guid Id)
        {
            var fmenv = fmenvManager.DataStore.GetAllQuery().Include("Parameters").FirstOrDefault(f=>f.ID == Id);
            var response = new FMENVResponse();
            if (fmenv != null)
            {
                    var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == fmenv.ID).ToListAsync();
                    response = new FMENVResponse
                    {
                        CreateOn = fmenv.CreateOn,
                        Id = fmenv.ID,
                        MicrobialParameters = microbs,
                        PhysicoParameters = fmenv.Parameters,
                        TypeName = fmenv.TypeName
                    };
            }
            return new AnalysisTemplateResponse<FMENVResponse> { Analysis = response };
        }

        public async Task<AnalysisTemplateResponse<List<NESREAResponse>>> GetAllNESREATemplates()
        {
            var fmenvlist = await nesreaStore.DataStore.GetAllQuery().Include("NESREAParameterTemplates").ToListAsync();
            var response = new List<NESREAResponse>();
            if (fmenvlist != null||fmenvlist.Any())
            {
                foreach (var temp in fmenvlist)
                {
                    var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == temp.ID).ToListAsync();
                    response.Add(new NESREAResponse
                    {
                        CreateOn = temp.CreateOn,
                        Id = temp.ID,
                        MicrobialParameters = microbs,
                        PhysicoParameters = temp.NESREAParameterTemplates,
                        Name = temp.Name
                    });
                }
            }
            return new AnalysisTemplateResponse<List<NESREAResponse>> { Analysis = response };
        }

        public async Task<AnalysisTemplateResponse<NESREAResponse>> GetNesreaTemplateById(Guid Id)
        {
            var nesrea = nesreaStore.DataStore.GetAllQuery().Include("NESREAParameterTemplates").FirstOrDefault(n=>n.ID == Id);
            var response = new NESREAResponse();
            if (nesrea != null)
            {
                var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == nesrea.ID).ToListAsync();
                response = new NESREAResponse
                {
                    CreateOn = nesrea.CreateOn,
                    Id = nesrea.ID,
                    MicrobialParameters = microbs,
                    PhysicoParameters = nesrea.NESREAParameterTemplates
                };
            }
            return new AnalysisTemplateResponse<NESREAResponse> { Analysis = response };
        }

        public async Task<AnalysisTemplateResponse<List<DPRResponse>>> GetAllDPRTemplates()
        {
            var fmenvlist = await dprStore.DataStore.GetAllQuery().Include("DPRParameterTemplates").ToListAsync();
            var response = new List<DPRResponse>();
            if (fmenvlist != null||fmenvlist.Any())
            {
                foreach (var temp in fmenvlist)
                {
                    var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == temp.ID).ToListAsync();
                    response.Add(new DPRResponse
                    {
                        CreateOn = temp.CreateOn,
                        Id = temp.ID,
                        MicrobialParameters = microbs,
                        PhysicoParameters = temp.DPRParameterTemplates,
                        Name = temp.Name
                    });
                }
            }
            return new AnalysisTemplateResponse<List<DPRResponse>> { Analysis = response };
        }

        public async Task<AnalysisTemplateResponse<DPRResponse>> GetDprTemplateById(Guid Id)
        {
            var dpr = dprStore.DataStore.GetAllQuery().Include("DPRParameterTemplates").FirstOrDefault(d=>d.ID == Id);
            var response = new DPRResponse();
            if (dpr != null)
            {
                var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == dpr.ID).ToListAsync();
                response = new DPRResponse
                {
                    CreateOn = dpr.CreateOn,
                    Id = dpr.ID,
                    MicrobialParameters = microbs,
                    PhysicoParameters = dpr.DPRParameterTemplates,
                };
            }
            return new AnalysisTemplateResponse<DPRResponse> { Analysis = response };
        }
        
        
        #region Air Quality Template

        public async Task<Guid> CreateAirQualityTemplate(AirQualityTemplateRequest request)
        {
            if (request == null) throw new ApiException("All feilds are required!");
            var temp = new AirQualityTemplate
            {
                Name = request.Name
            };
            await airQualityStore.DataStore.Add(temp);
            if (request.Parameters.Any())
            {
                var parameters = new List<AirQualityParameter>();
                foreach (var parameter in request.Parameters)
                {
                    parameters.Add(new AirQualityParameter
                    {
                        AirQualityTemplateId = temp.ID,
                        Limit = parameter.Limit,
                        Name = parameter.Name,
                        Unit = parameter.Unit
                    });
                }
                await airQualityParamStore.DataStore.AddRange(parameters);
            }
            await airQualityStore.Save();
            return temp.ID;
        }

        public async Task<AirQualityTemplate> UpdateAirQualityTemplate(AirQualityTemplate template)
        {
            var temp =await airQualityStore.DataStore.GetById(template.ID);
            if (temp == null) throw new ApiException("Template not found!");
            temp.Name = template.Name ?? temp.Name;
            airQualityStore.DataStore.Update(temp);
            await airQualityStore.Save();
            return temp;
        }

        public AirQualityTemplate GetAirQualityTemplateById(Guid Id)
        {
            var temp = airQualityStore.DataStore.GetAllQuery().Include("Parameters").FirstOrDefault(t=>t.ID == Id);
            if (temp == null) throw new KeyNotFoundException("Template not found!");
            return temp;
        }
        
        public async Task<Guid> DeleteAirQualityTemplate(Guid Id)
        {
            var temp = await airQualityStore.DataStore.GetById(Id);
            if (temp == null) throw new KeyNotFoundException("Template not found!");
            var parameters = airQualityParamStore.DataStore.GetAllQuery().Where(p => p.AirQualityTemplateId == temp.ID).ToList();
            if (parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    await airQualityParamStore.DataStore.Delete(parameter.ID);
                }
            }
            await airQualityStore.DataStore.Delete(temp.ID);
            await airQualityStore.Save();
            return temp.ID;
        }

        public async Task<List<AirQualityTemplate>> GetAllAirQualityTemplate()
        {
            var templates = await airQualityStore.DataStore.GetAllQuery().Include("Parameters").ToListAsync();
            return templates;
        }

        public async Task<AirQualityParameter> AddAirQualityParameter(AddAirQualityParameterRequest request)
        {
            var temp = await airQualityStore.DataStore.GetById(request.TemplateId);
            if (temp == null) throw new KeyNotFoundException($"Template with Id:'{request.TemplateId}' not found");
            var parameter = new AirQualityParameter
            {
                AirQualityTemplateId = request.TemplateId,
                Limit = request.Limit,
                Name = request.Name,
                Unit = request.Unit
            };

            await airQualityParamStore.DataStore.Add(parameter);
            await airQualityParamStore.Save();
            return parameter;
        }

        public async Task<AirQualityParameter> GetAirQualityParameter(Guid Id)
        {
            var parameter = await airQualityParamStore.DataStore.GetById(Id);
            if (parameter == null) throw new KeyNotFoundException("Parameter not found");
            return parameter;
        }
        
        public async Task<List<AirQualityParameter>> GetAllAirQualityParameter(Guid TemplateId)
        {
            var parameters = await airQualityParamStore.DataStore.GetAllQuery().Where(p=>p.AirQualityTemplateId==TemplateId).ToListAsync();
            return parameters;
        }
        
        public async Task<Guid> DeleteAirQualityParameter(Guid Id)
        {
            var parameter = await airQualityParamStore.DataStore.GetById(Id);
            if (parameter == null) throw new KeyNotFoundException("Parameter not found");
            await airQualityParamStore.DataStore.Delete(Id);
            return Id;
        }
        
        public async Task<AirQualityParameter> UpdateAirQualityParameter(AirQualityParameter parameter)
        {
            var oldParameter = await airQualityParamStore.DataStore.GetById(parameter.ID);
            if (oldParameter == null) throw new KeyNotFoundException("Parameter not found");
            oldParameter.Limit = parameter.Limit ?? oldParameter.Limit;
            oldParameter.Name = parameter.Name ?? oldParameter.Name;
            oldParameter.Unit = parameter.Unit ?? oldParameter.Unit;
            airQualityParamStore.DataStore.Update(oldParameter);
            return oldParameter;
        }


        #endregion


        #region Air Quality Sample

        //Start bringing services to controller


        public async Task<Guid> CreateAirQualitySample(AirQualitySampleRequest request)
        {
            if (request == null) throw new ApiException("All feilds are required!");
            var temp = new AirQualitySample
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                ID = Guid.NewGuid()
            };
            await airQualitySampleStore.DataStore.Add(temp);
            if (request.Parameters.Any())
            {
                var parameters = new List<AirQualitySampleParameter>();
                foreach (var parameter in request.Parameters)
                {
                    parameters.Add(new AirQualitySampleParameter
                    {
                        AirQualitySampleId = temp.ID,
                        Limit = parameter.Limit,
                        Name = parameter.Name,
                        Unit = parameter.Unit
                    });
                }
                await airQualitySampleParameterStore.DataStore.AddRange(parameters);
            }
            await airQualityStore.Save();
            return temp.ID;
        }

        public async Task<Guid> DeleteAirQualitySample(Guid sampleId)
        {
            if (sampleId == Guid.Empty) throw new ApiException("Invalid sample Id supplied");
            var sample = airQualitySampleStore.DataStore.GetAllQuery().Include("Parameters").FirstOrDefault(p => p.ID == sampleId);
            if (sample == null) throw new KeyNotFoundException($"Sample with ID:{sampleId} not found");

            if (sample.Parameters.Any())
            {
                foreach (var param in sample.Parameters)
                {
                    await airQualitySampleParameterStore.DataStore.Delete(param.ID);
                }
            }
            await airQualitySampleStore.DataStore.Delete(sample.ID);
            await airQualitySampleStore.Save();
            return sampleId;
        }

        public async Task<IEnumerable<AirQualitySample>> GetAllAirQualitySamples()
        {
            var response = await airQualitySampleStore.DataStore.GetAllQuery().Include("Parameters").ToListAsync();
            return response;
        }

        public async Task<AirQualitySample> GetAirQualitySampleByBy(Guid SampleId)
        {

            var sample = airQualitySampleStore.DataStore.GetAllQuery().Include("Parameters").FirstOrDefault(p => p.ID == SampleId);
            if (sample == null) throw new KeyNotFoundException("Sample not found");
            return sample;
        }



        #endregion
    }
}
