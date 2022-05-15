using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PAMS.Application.Helpers;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class SampleService : ISampleService
    {
        private readonly IStoreManager<Client> clientStoreManager;
        private readonly IStoreManager<PhysicoChemicalAnalysis> physicoManager;
        private readonly IStoreManager<MicroBiologicalAnalysis> microbialManager;
        private readonly IMapper mapper;
        private readonly IMajorUtility majorUtility;
        private readonly IStoreManager<FMEnv> fmenvManager;
        private readonly IStoreManager<FMEnvParameterTemplate> fmenvTemp;
        private readonly IStoreManager<NESREA> nesreaStore;
        private readonly IStoreManager<NESREAParameterTemplate> nesreaTemp;
        private readonly IStoreManager<DPR> dprStore;
        private readonly IStoreManager<DPRParameterTemplate> dprTemp;
        private readonly IStoreManager<MicroBiologicalAnalysisTemplate> microbialStore;

        public IStoreManager<Sampling> SamplingStoreManager { get; }
        public IStoreManager<ClientSample> ClientSamplestoreManager { get; }

        public SampleService(
            IStoreManager<Sampling> SamplingStoreManager,
            IStoreManager<ClientSample> ClientSamplestoreManager,
            IStoreManager<Client> clientStoreManager,
            IStoreManager<PhysicoChemicalAnalysis> physicoManager,
            IStoreManager<MicroBiologicalAnalysis> microbialManager,
            IMapper mapper,
            IMajorUtility majorUtility,
            IStoreManager<FMEnv> fmenvManager,
            IStoreManager<FMEnvParameterTemplate> fmenvTemp,
            IStoreManager<NESREA> nesreaStore,
            IStoreManager<NESREAParameterTemplate> nesreaTemp,
            IStoreManager<DPR> dprStore,
            IStoreManager<DPRParameterTemplate> dprTemp,
            IStoreManager<MicroBiologicalAnalysisTemplate> microbialStore
            )
        {
            this.SamplingStoreManager = SamplingStoreManager;
            this.ClientSamplestoreManager = ClientSamplestoreManager;
            this.clientStoreManager = clientStoreManager;
            this.physicoManager = physicoManager;
            this.microbialManager = microbialManager;
            this.mapper = mapper;
            this.majorUtility = majorUtility;
            this.fmenvManager = fmenvManager;
            this.fmenvTemp = fmenvTemp;
            this.nesreaStore = nesreaStore;
            this.nesreaTemp = nesreaTemp;
            this.dprStore = dprStore;
            this.dprTemp = dprTemp;
            this.microbialStore = microbialStore;
        }


        public async Task<ClientSampleResponse> GetClientSampleTemplates(Guid ClientId)
        {
            var samples = await ClientSamplestoreManager.DataStore.GetAllQuery()
                .Where(c => c.ClientID == ClientId)
                .ToListAsync();
            var fmenvList = new List<FMENVResponse>();
            var dprList = new List<DPRResponse>();
            var nesreaList = new List<NESREAResponse>();
            if (samples.Any())
            {
                foreach (var sample in samples)
                {
                    switch (sample.Type)
                    {
                        case PhysicoChemicalAnalysisType.DPR:
                            var dpr = dprStore.DataStore.GetAllQuery().Include("DPRParameterTemplates").FirstOrDefault(f => f.ID == sample.SampleTemplateID);
                            if (dpr !=null)
                            {
                                dprList.Add(new DPRResponse
                                {
                                    CreateOn = dpr.CreateOn,
                                    Id = dpr.ID,
                                    MicrobialParameters = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == dpr.ID).ToListAsync(),
                                    PhysicoParameters = dpr.DPRParameterTemplates,
                                    Name = dpr.Name
                                });
                            }
                            break;


                        case PhysicoChemicalAnalysisType.FMEnv:

                            var fmenv = fmenvManager.DataStore.GetAllQuery().Include("Parameters").FirstOrDefault(f=>f.ID == sample.SampleTemplateID);
                            if (fmenv != null)
                            {

                                fmenvList.Add(new FMENVResponse
                                {
                                    CreateOn = fmenv.CreateOn,
                                    Id = fmenv.ID,
                                    MicrobialParameters = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == fmenv.ID).ToListAsync(),
                                    PhysicoParameters = fmenv.Parameters,
                                    TypeName = fmenv.TypeName
                                }); 
                            }
                            break;

                        case PhysicoChemicalAnalysisType.NESREA:
                            var nesrea = nesreaStore.DataStore.GetAllQuery().Include("NESREAParameterTemplates").FirstOrDefault(f => f.ID == sample.SampleTemplateID); 
                            if (nesrea != null)
                            {

                                var microbs = await microbialStore.DataStore.GetAllQuery().Where(m => m.PhysicoId == nesrea.ID).ToListAsync();
                                nesreaList.Add(new NESREAResponse
                                {
                                    CreateOn = nesrea.CreateOn,
                                    Id = nesrea.ID,
                                    MicrobialParameters = microbs,
                                    PhysicoParameters = nesrea.NESREAParameterTemplates,
                                    Name = nesrea.Name
                                }); 
                            }
                            break;
                        default: break;
                    }
                }
            }
            return new ClientSampleResponse { NESREAs = nesreaList, DPRs = dprList, FMEnvs = fmenvList };
        }

        #region Sampling
        public async Task<Sampling> CreateSampling(SamplingDTO sampling)
        {
            var client = await clientStoreManager.DataStore.GetById(sampling.ClientId);
            if (client == null) return null;
            var newSampling = new Sampling
            {
                ID = Guid.NewGuid(),
                SamplingDate = DateTime.Parse(sampling.SamplingDate),
                SamplingTime = DateTime.Parse(sampling.SamplingTime),
                StaffId = sampling.StaffId,
                StaffName = sampling.StaffName,
                Status = Status.Pending,
                ClientID = sampling.ClientId,
                GPSLocation = $"{sampling.GPSLat},{sampling.GPSLong}",
                Picture = sampling.Picture,
                IsReportCreated = false
            };
            await SamplingStoreManager.DataStore.Add(newSampling);

            var physicoChemicalAnalyses = new List<PhysicoChemicalAnalysis>();
            foreach (var physicSample in sampling.PhysicoChemicals)
            {
                physicoChemicalAnalyses.Add(new PhysicoChemicalAnalysis
                {
                    Limit = physicSample.Limit,
                    Result = physicSample.Result,
                    Test_Performed_And_Unit = physicSample.Test_Performed_And_Unit,
                    Test_Method = physicSample.Test_Method,
                    Type = physicSample.Type,
                    UC = physicSample.UC,
                    ID = Guid.NewGuid(),
                    SamplingID = newSampling.ID
                });
            }
            await physicoManager.DataStore.AddRange(physicoChemicalAnalyses);

            var microbialAnalyses = new List<MicroBiologicalAnalysis>();
            foreach (var microbialSample in sampling.MicroBiologicals)
            {
                microbialAnalyses.Add(new MicroBiologicalAnalysis
                {
                    Limit = microbialSample.Limit,
                    Result = microbialSample.Result,
                    Test_Method = microbialSample.Test_Method,
                    Microbial_Group = microbialSample.Microbial_Group,
                    Unit = microbialSample.Unit,
                    ID = Guid.NewGuid(),
                    SamplingID = newSampling.ID
                });
            }
            await microbialManager.DataStore.AddRange(microbialAnalyses);

            await SamplingStoreManager.Save();
            return newSampling;
        }

        public SamplingResponse GetSampling(Guid samplingId)
        {
            var sampling = SamplingStoreManager.DataStore.GetAllQuery().Include("MicroBiologicalAnalyses").Include("PhysicoChemicalAnalyses").FirstOrDefault(s => s.ID == samplingId);
            if (sampling != null)
            {
                var response = new SamplingResponse
                {
                    ClientName = (sampling.ClientID != Guid.Empty && sampling.ClientID != null) ? clientStoreManager.DataStore.GetById((Guid)sampling.ClientID).Result.Name : "Unknown",
                    GPSLat = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(0).ToString()) : 0,
                    GPSLong = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(1).ToString()) : 0,
                    ID = sampling.ID,
                    Picture = sampling.Picture,
                    SamplingDate = sampling.SamplingDate.ToString("dd/MM/yyyy"),
                    SamplingTime = sampling.SamplingTime.ToString("hh:mm:ss:tt"),
                    StaffName = sampling.StaffName,
                    Status = sampling.Status.ToString(),
                    IsReportCreated = sampling.IsReportCreated,
                    MicroBiologicals = mapper.Map<List<MicroBiologicalAnalysisResponse>>(sampling.MicroBiologicalAnalyses),
                    PhysicoChemicals = mapper.Map<List<PhysicoChemicalAnalysisResponse>>(sampling.PhysicoChemicalAnalyses)
                };
                return response;
            }
            return default;
        }

        public async Task<IEnumerable<SamplingResponse>> GetAllSamplings(Guid clientId)
        {
            List<SamplingResponse> responses = new List<SamplingResponse>();
            var samplings = await SamplingStoreManager.DataStore.GetAllQuery()
                .Include("MicroBiologicalAnalyses")
                .Include("PhysicoChemicalAnalyses")
                .Where(s => s.ClientID == clientId)
                .ToListAsync();
            foreach (var sampling in samplings)
            {
                responses.Add(new SamplingResponse
                {
                    ClientName = (sampling.ClientID != Guid.Empty && sampling.ClientID != null) ? clientStoreManager.DataStore.GetById((Guid)sampling.ClientID).Result.Name : "Unknown",
                    GPSLat = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(0).ToString()) : 0,
                    GPSLong = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(1).ToString()) : 0,
                    ID = sampling.ID,
                    Picture = sampling.Picture,
                    SamplingDate = sampling.SamplingDate.ToString("dd/MM/yyyy"),
                    SamplingTime = sampling.SamplingTime.ToString("hh:mm:ss:tt"),
                    StaffName = sampling.StaffName,
                    Status = sampling.Status.ToString(),
                    IsReportCreated = sampling.IsReportCreated,
                    MicroBiologicals = mapper.Map<List<MicroBiologicalAnalysisResponse>>(sampling.MicroBiologicalAnalyses),
                    PhysicoChemicals = mapper.Map<List<PhysicoChemicalAnalysisResponse>>(sampling.PhysicoChemicalAnalyses)
                });
            }
            return responses;
        }
        public async Task<IEnumerable<SamplingResponse>> GetAllSamplings()
        {
            List<SamplingResponse> responses = new List<SamplingResponse>();
            var samplings = await SamplingStoreManager.DataStore.GetAllQuery()
                .Include("MicroBiologicalAnalyses")
                .Include("PhysicoChemicalAnalyses")
                .ToListAsync();
            foreach (var sampling in samplings)
            {
                responses.Add(new SamplingResponse
                {
                    ClientName = (sampling.ClientID != Guid.Empty && sampling.ClientID != null) ? clientStoreManager.DataStore.GetById((Guid)sampling.ClientID).Result.Name : "Unknown",
                    GPSLat = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(0).ToString()) : 0,
                    GPSLong = !string.IsNullOrWhiteSpace(sampling.GPSLocation) ? decimal.Parse(sampling.GPSLocation.Split(",").GetValue(1).ToString()) : 0,
                    ID = sampling.ID,
                    Picture = sampling.Picture,
                    SamplingDate = sampling.SamplingDate.ToString("dd/MM/yyyy"),
                    SamplingTime = sampling.SamplingTime.ToString("hh:mm:ss:tt"),
                    StaffName = sampling.StaffName,
                    Status = sampling.Status.ToString(),
                    IsReportCreated = sampling.IsReportCreated,
                    MicroBiologicals = mapper.Map<List<MicroBiologicalAnalysisResponse>>(sampling.MicroBiologicalAnalyses),
                    PhysicoChemicals = mapper.Map<List<PhysicoChemicalAnalysisResponse>>(sampling.PhysicoChemicalAnalyses)
                });
            }
            return responses;
        }

        public async Task<bool> DeleteClientSamplings(Guid clientId)
        {
            var samplings = await GetAllSamplings(clientId);
            if (samplings.Any())
            {
                foreach (var sampling in samplings)
                {

                    foreach (var physico in sampling.PhysicoChemicals)
                    {
                        await physicoManager.DataStore.Delete(physico.Id);
                    } 
                    foreach (var microb in sampling.MicroBiologicals)
                    {
                        await microbialManager.DataStore.Delete(microb.Id);
                    }
                    await SamplingStoreManager.DataStore.Delete(sampling.ID);
                }
                await SamplingStoreManager.Save();
            }
            return true;
        }

        public async Task<AnalysisDaysCount> SampleTestCountPastSevenDays()
        {
            var analysisDaysCount = new AnalysisDaysCount();

            var result = await SamplingStoreManager.DataStore.GetAllQuery()
                .Include("MicroBiologicalAnalyses")
                .Include("PhysicoChemicalAnalyses")
                .ToListAsync();

            int todayCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.Day && x.SamplingDate.Month == DateTime.Today.Month && x.SamplingDate.Year == DateTime.Today.Year).Count();
            int yesterdayCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-1).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-1).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-1).Year).Count();
            int twoDaysBackCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-2).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-2).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-2).Year).Count();
            int threeDaysBackCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-3).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-3).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-3).Year).Count();
            int fourDaysBackCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-4).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-4).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-4).Year).Count();
            int fiveDaysBackCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-5).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-5).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-5).Year).Count();
            int sixDaysCount = result.Where(x => x.SamplingDate.Day == DateTime.Today.AddDays(-6).Day && x.SamplingDate.Month == DateTime.Today.AddDays(-6).Month && x.SamplingDate.Year == DateTime.Today.AddDays(-6).Year).Count();

            analysisDaysCount.GToday = todayCount;
            analysisDaysCount.FYesterDay = yesterdayCount;
            analysisDaysCount.ETwoDaysBack = twoDaysBackCount;
            analysisDaysCount.DThreeDaysBack = threeDaysBackCount;
            analysisDaysCount.CFourDaysBack = fourDaysBackCount;
            analysisDaysCount.BFiveDaysBack = fiveDaysBackCount;
            analysisDaysCount.ASixDaysBack = sixDaysCount;

            return analysisDaysCount;
        }
        #endregion
    }
}
