using Microsoft.AspNetCore.Identity;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IClientService _clientService;
        private readonly IFieldScientistAnalysisDPRService _fieldScientistAnalysisDPRService;
        private readonly IFieldScientistAnalysisFMEnvService _fieldScientistAnalysisFMEnvService;
        private readonly IFieldScientistAnalysisNesreaService _fieldScientistAnalysisNesreaService;
        private readonly ISampleService _sampleService;
        private readonly UserManager<PamsUser> _userManager;

        public DashBoardService(
            IClientService clientService,
            IFieldScientistAnalysisNesreaService fieldScientistAnalysisNesreaService,
            IFieldScientistAnalysisFMEnvService fieldScientistAnalysisFMEnvService,
            IFieldScientistAnalysisDPRService fieldScientistAnalysisDPRService, 
            ISampleService sampleService,
            UserManager<PamsUser> userManager)
        {
            _clientService = clientService;
            _fieldScientistAnalysisDPRService = fieldScientistAnalysisDPRService;
            _fieldScientistAnalysisFMEnvService = fieldScientistAnalysisFMEnvService;
            _fieldScientistAnalysisNesreaService = fieldScientistAnalysisNesreaService;
            _sampleService = sampleService;
            _userManager = userManager;
        }

        public async Task<DashboardVm> GetDashboard()
        {
            var dashBoardVm = new DashboardVm();

            var userCount = _userManager.GetUsersInRoleAsync("Staff").Result.Count();

            var labSample = await _sampleService.SampleTestCountPastSevenDays();

            var fieldAnalysisNesrea = await _fieldScientistAnalysisNesreaService.NesreaTestCountPastSevenDays();
            var fieldAnalysisDpr = await _fieldScientistAnalysisDPRService.DPRTestCountPastSevenDays();
            var fieldAnalysisFmenv = await _fieldScientistAnalysisFMEnvService.FMEnvTestCountPastSevenDays();

            dashBoardVm.NumberOfClient = _clientService.Count();
            dashBoardVm.NumberOfLocation = _fieldScientistAnalysisNesreaService.LocationsCount();
            dashBoardVm.NumberOfAnalyst = userCount;

            dashBoardVm.LabAnalysis.GToday = labSample.GToday;
            dashBoardVm.LabAnalysis.FYesterDay = labSample.FYesterDay;
            dashBoardVm.LabAnalysis.ETwoDaysBack = labSample.ETwoDaysBack;
            dashBoardVm.LabAnalysis.DThreeDaysBack = labSample.DThreeDaysBack;
            dashBoardVm.LabAnalysis.CFourDaysBack = labSample.CFourDaysBack;
            dashBoardVm.LabAnalysis.BFiveDaysBack = labSample.BFiveDaysBack;
            dashBoardVm.LabAnalysis.ASixDaysBack = labSample.ASixDaysBack;

            dashBoardVm.FieldAnalysis.GToday = fieldAnalysisNesrea.GToday + fieldAnalysisDpr.GToday + fieldAnalysisFmenv.GToday;
            dashBoardVm.FieldAnalysis.FYesterDay = fieldAnalysisNesrea.FYesterDay + fieldAnalysisDpr.FYesterDay + fieldAnalysisFmenv.FYesterDay;
            dashBoardVm.FieldAnalysis.ETwoDaysBack = fieldAnalysisNesrea.ETwoDaysBack + fieldAnalysisDpr.ETwoDaysBack + fieldAnalysisFmenv.ETwoDaysBack;
            dashBoardVm.FieldAnalysis.DThreeDaysBack = fieldAnalysisNesrea.DThreeDaysBack + fieldAnalysisDpr.DThreeDaysBack + fieldAnalysisFmenv.DThreeDaysBack;
            dashBoardVm.FieldAnalysis.CFourDaysBack = fieldAnalysisNesrea.CFourDaysBack + fieldAnalysisDpr.CFourDaysBack + fieldAnalysisFmenv.CFourDaysBack;
            dashBoardVm.FieldAnalysis.BFiveDaysBack = fieldAnalysisNesrea.BFiveDaysBack + fieldAnalysisDpr.BFiveDaysBack + fieldAnalysisFmenv.BFiveDaysBack;
            dashBoardVm.FieldAnalysis.ASixDaysBack = fieldAnalysisNesrea.ASixDaysBack + fieldAnalysisDpr.ASixDaysBack + fieldAnalysisFmenv.ASixDaysBack;

            return dashBoardVm;
        }
    }
}
