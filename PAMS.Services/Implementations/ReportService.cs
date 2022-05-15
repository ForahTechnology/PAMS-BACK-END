using Microsoft.EntityFrameworkCore;
using Utility = PAMS.Application.Helpers.Utility;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PAMS.Domain.Enums;
using AutoMapper;

namespace PAMS.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IStoreManager<Report> reportStoreManager;
        private readonly IStoreManager<PhysicoChemicalAnalysis> physicoStoreManager;
        private readonly IStoreManager<MicroBiologicalAnalysisTemplate> microTemplateStoreManager;
        private readonly IStoreManager<MicroBiologicalAnalysis> microStoreManager;
        private readonly IClientService clientService;
        private readonly ISettingsService settingsService;
        private readonly IMapper mapper;
        private readonly IStoreManager<Sampling> samplingStoreManager;

        public ReportService(
            IStoreManager<Report> reportStoreManager,
            IStoreManager<PhysicoChemicalAnalysis> physicoStoreManager,
            IStoreManager<MicroBiologicalAnalysis> microStoreManager,
            IStoreManager<MicroBiologicalAnalysisTemplate> microTemplateStoreManager,
            IClientService clientService,
            ISettingsService settingsService,
            IMapper mapper,
            IStoreManager<Sampling> samplingStoreManager
            )
        {
            this.reportStoreManager = reportStoreManager;
            this.physicoStoreManager = physicoStoreManager;
            this.microTemplateStoreManager = microTemplateStoreManager;
            this.microStoreManager = microStoreManager;
            this.clientService = clientService;
            this.settingsService = settingsService;
            this.mapper = mapper;
            this.samplingStoreManager = samplingStoreManager;
        }
        public async Task<ReportResponse> CreateReport(CreateReport report)
        {
            var client = clientService.GetClient(report.ClientID);
            if (client == null) return null;

            var sample = samplingStoreManager.DataStore.GetById(report.SamplingID);
            if (sample == null) return null;

            var newReport = new Report
            {
                Batch_Number = report.Batch_Number,
                Certificate_Number = report.Certificate_Number,
                ClientID = report.ClientID,
                Comment = report.Comment,
                Date_Analysed_In_Lab = report.Date_Analysed_In_Lab,
                Date_Recieved_In_Lab = report.Date_Recieved_In_Lab,
                ID = Guid.NewGuid(),
                Lab_Analyst = report.Lab_Analyst,
                Lab_Env_Con = $"{report.Lab_Env_Con.Humidity},{report.Lab_Env_Con.Temperature}",
                Lab_Sample_Ref_Number = report.Lab_Sample_Ref_Number,
                Sample_Label = report.Sample_Label,
                Sample_Type = report.Sample_Type,
                SamplingID = sample.Result.ID
            };
            await reportStoreManager.DataStore.Add(newReport);
            await reportStoreManager.Save();

            //update that the report for the sampleId has been created
            sample.Result.IsReportCreated = true;
            samplingStoreManager.DataStore.Update(sample.Result);
            await samplingStoreManager.Save();

            var response = new ReportResponse
            {
                Batch_Number = newReport.Batch_Number,
                Certificate_Number = newReport.Certificate_Number,
                ClientAddress = client.Address,
                ClientName = client.Name,
                Comment = newReport.Comment,
                Date_Analysed_In_Lab = newReport.Date_Analysed_In_Lab,
                Date_Recieved_In_Lab = newReport.Date_Recieved_In_Lab,
                ID = newReport.ID,
                Lab_Analyst = newReport.Lab_Analyst,
                Lab_Env_Con = report.Lab_Env_Con,
                Lab_Sample_Ref_Number = newReport.Lab_Sample_Ref_Number,
                MicroBiologicalAnalyses = new List<MicroBiologicalAnalysis>(),
                PhysicoChemicalAnalyses = new List<PhysicoChemicalAnalysis>(),
                Sample_Label = newReport.Sample_Label,
                Sample_Type = newReport.Sample_Type,
                SamplingID = newReport.SamplingID
            };
            return response;
        }

        public async Task<bool> DeleteReport(Guid reportId)
        {
            var report = reportStoreManager.DataStore.GetAllQuery().Include("PhysicoChemicalAnalyses").Include("MicroBiologicalAnalyses").FirstOrDefault(r => r.ID == reportId);
            if (report != null)
            {
                foreach (var phy in report.PhysicoChemicalAnalyses)
                {
                    await physicoStoreManager.DataStore.Delete(phy.ID);
                }
                foreach (var mic in report.MicroBiologicalAnalyses)
                {
                    await microStoreManager.DataStore.Delete(mic.ID);
                }
                await reportStoreManager.DataStore.Delete(reportId);
                await reportStoreManager.Save();
                return true;
            }
            return false;
        }

        public async Task<ReportResponse> GetReport(Guid reportId)
        {
            var report = reportStoreManager.DataStore.GetAllQuery().Include("PhysicoChemicalAnalyses").Include("MicroBiologicalAnalyses").FirstOrDefault(r => r.ID == reportId);
            
            if (report != null)
            {
                var client = clientService.GetClient(report.ClientID);
                if (client == null) return null;
                var sampling = samplingStoreManager.DataStore.GetAllQuery().Include("MicroBiologicalAnalyses").Include("PhysicoChemicalAnalyses").FirstOrDefault(s => s.ID == report.SamplingID);
                if (sampling == null) return null;
                var lab_env = new Laboratory_Environment_Conditions
                {
                    Humidity = report.Lab_Env_Con.Split(",").GetValue(0).ToString(),
                    Temperature = report.Lab_Env_Con.Split(",").GetValue(1).ToString()
                };
                var response = new ReportResponse
                {
                    Batch_Number = report.Batch_Number,
                    Certificate_Number = report.Certificate_Number,
                    ClientAddress = client.Address,
                    ClientName = client.Name,
                    Comment = report.Comment,
                    Date_Analysed_In_Lab = report.Date_Analysed_In_Lab,
                    Date_Recieved_In_Lab = report.Date_Recieved_In_Lab,
                    ID = report.ID,
                    Lab_Analyst = report.Lab_Analyst,
                    Lab_Env_Con = lab_env,
                    Lab_Sample_Ref_Number = report.Lab_Sample_Ref_Number,
                    MicroBiologicalAnalyses = sampling.MicroBiologicalAnalyses.Select(x => new MicroBiologicalAnalysis 
                    { 
                        ID = x.ID,
                        Limit = x.Limit,
                        Microbial_Group = x.Microbial_Group,
                        Result = x.Result,
                        Sampling = x.Sampling,
                        SamplingID = x.SamplingID,
                        Test_Method = x.Test_Method,
                        Unit = x.Unit
                    }).ToList(),
                    PhysicoChemicalAnalyses = sampling.PhysicoChemicalAnalyses.Select(x => new PhysicoChemicalAnalysis
                    {
                        ID = x.ID,
                        Limit = x.Limit,
                        Test_Performed_And_Unit = x.Test_Performed_And_Unit,
                        Result = x.Result,
                        Sampling = x.Sampling,
                        SamplingID = x.SamplingID,
                        Test_Method = x.Test_Method,
                        Type = x.Type,
                        UC  = x.UC
                    }).ToList(),
                    Sample_Label = report.Sample_Label,
                    Sample_Type = report.Sample_Type,
                    SamplingID = report.SamplingID
                };
                return response; 
            }
            return null;
        }

        public async Task<List<ReportResponse>> GetReports()
        {
            var reportIds = await reportStoreManager.DataStore.GetAllQuery().Select(r=>r.ID).ToListAsync();
            if (reportIds.Any())
            {
                var response = new List<ReportResponse>();
                foreach (var id in reportIds)
                {
                    response.Add(await GetReport(id));
                }
                return response;
            }
            return null;
        }

        public async Task<List<ReportResponse>> GetReports(Guid clientId)
        {
            var clientReportIds = await reportStoreManager.DataStore.GetAllQuery().Where(r => r.ClientID == clientId).Select(id => id.ID).ToListAsync();
            var response = new List<ReportResponse>();
            if (clientReportIds.Any())
            {
                foreach (var id in clientReportIds)
                {
                    response.Add(await GetReport(id));
                }
                return response;
            }
            return response;
        }

        public async Task<ReportResponse> GetReportBySampleId(Guid sampleId)
        {
            var sampleReportId = await reportStoreManager.DataStore.GetAllQuery().Where(r => r.SamplingID == sampleId).Select(id => id.ID).ToListAsync();
            var response = new ReportResponse();
            if (sampleReportId.Any())
            {
                foreach (var id in sampleReportId)
                {
                    response = await GetReport(id);
                }
                return response;
            }
            return response;
        }

        public async Task<ReportResponse> UpdateReport(UpdateReport update)
        {
            var report = await reportStoreManager.DataStore.GetById(update.ID); 
            if (report!=null)
            {
                report.Date_Recieved_In_Lab = update.Date_Recieved_In_Lab;
                report.Date_Analysed_In_Lab = update.Date_Analysed_In_Lab;
                report.Batch_Number = update.Batch_Number;
                report.Certificate_Number = update.Certificate_Number;
                report.Comment = update.Comment;
                report.Lab_Analyst = update.Lab_Analyst;
                report.Lab_Env_Con = $"{update.Lab_Env_Con.Humidity},{update.Lab_Env_Con.Temperature}";
                report.Lab_Sample_Ref_Number = update.Lab_Sample_Ref_Number;
                report.Sample_Label = update.Sample_Label;
                report.Sample_Type = update.Sample_Type;
                reportStoreManager.DataStore.Update(report);
                await reportStoreManager.Save();
                return await GetReport(report.ID); 
            }
            return null;
        }

        public async Task<PhysicoChemicalAnalysis> GetPhysico(Guid id) => await physicoStoreManager.DataStore.GetById(id);

        public async Task<PhysicoChemicalAnalysis> UpdatePhysico(PhysicoChemicalAnalysis analysis)
        {
            var physico = await physicoStoreManager.DataStore.GetById(analysis.ID);
            if (physico!=null)
            {
                physicoStoreManager.DataStore.Update(analysis);
                await physicoStoreManager.Save();
                return analysis;
            }
            return physico;
        }

        public async Task<bool> DeletePhysico(Guid id) => await physicoStoreManager.DataStore.Delete(id);

        public async Task<MicroBiologicalAnalysis> GetMicro(Guid id) => await microStoreManager.DataStore.GetById(id);

        public async Task<MicroBiologicalAnalysis> UpdateMicroBio(MicroBiologicalAnalysis analysis)
        {
            var microbio = await microStoreManager.DataStore.GetById(analysis.ID);
            if (microbio!=null)
            {
                microStoreManager.DataStore.Update(analysis);
                await physicoStoreManager.Save();
                return analysis;
            }
            return microbio;
        }

        public async Task<bool> DeleteMicroBio(Guid id) => await microStoreManager.DataStore.Delete(id);

        public async Task<string> GetReportFileString(ReportResponse reportResponse, string path)
        {
            var content = new string[0];
            var settings = await settingsService.GetSettings();
            if(settings is null)
            {
                content = Utility.GenerateReportContent(reportResponse, "" , path, "");
            }
            else
            {
                content = Utility.GenerateReportContent(reportResponse, settings.Lab_Director == string.Empty ? "" : settings.Lab_Director, path, "");
            }
            
            return content[1];
        }
    }
}
