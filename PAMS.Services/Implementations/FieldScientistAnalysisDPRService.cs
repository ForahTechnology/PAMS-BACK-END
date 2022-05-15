using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Application.Interfaces.Utilities;
using PAMS.Domain.Entities;
using PAMS.Domain.Entities.FieldScientistEntities;
using PAMS.DTOs.Request.FieldScientistRequest;
using PAMS.DTOs.Response;
using PAMS.DTOs.Response.FieldScientistResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class FieldScientistAnalysisDPRService : IFieldScientistAnalysisDPRService
    {
        private readonly UserManager<PamsUser> _userManager;
        private readonly IContextAccessor _contextAccessor;
        private readonly IUploadService _imageUploadService;

        private readonly IStoreManager<DPRField> _dPRFieldStoreManager;
        private readonly IStoreManager<FieldLocation> _fileLocationStoreManager;
        private readonly IStoreManager<SamplePointLocation> _samplePointLocationStoreManager;

        private readonly IStoreManager<DPRTemplate> _dPRTemplate;
        private readonly IStoreManager<DPRFieldResult> _dPRFieldResult;

        public FieldScientistAnalysisDPRService(
            UserManager<PamsUser> userManager,
            IContextAccessor contextAccessor,
            IUploadService imageUploadService,
            IStoreManager<DPRField> dPRFieldStoreManager,
            IStoreManager<FieldLocation> fileLocationStoreManager,
            IStoreManager<DPRTemplate> dPRTemplate,
            IStoreManager<DPRFieldResult> dPRFieldResult,
            IStoreManager<SamplePointLocation> samplePointLocationStoreManager
            )
        {
            _userManager = userManager;
            _imageUploadService = imageUploadService;
            _contextAccessor = contextAccessor;
            _dPRFieldStoreManager = dPRFieldStoreManager;
            _fileLocationStoreManager = fileLocationStoreManager;
            _dPRFieldResult = dPRFieldResult;
            _dPRTemplate = dPRTemplate;
            _samplePointLocationStoreManager = samplePointLocationStoreManager;
        }

        #region DPR Field Template

        public async Task<List<DPRTemplate>> CreateDPRSampleTemplate(DPRTemplatesVm model)
        {
            var dPRTemplates = new List<DPRTemplate>();

            foreach (var template in model.DPRTemplates)
            {
                dPRTemplates.Add(new DPRTemplate
                {
                    ID = Guid.NewGuid(),
                    TestLimit = template.TestLimit,
                    TestName = template.TestName,
                    TestResult = template.TestResult,
                    TestUnit = template.TestUnit
                });
            }

            await _dPRTemplate.DataStore.AddRange(dPRTemplates);
            await _dPRTemplate.Save();
            return dPRTemplates;
        }

        public async Task<List<DPRTemplate>> GetAllDPRTemplates()
        {
            return await _dPRTemplate.DataStore.GetAllQuery().ToListAsync();
        }

        public async Task<string> UpdateDPRTemplate(UpdateTemplateVm model)
        {
            var template = await _dPRTemplate.DataStore.GetById(model.ID);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            template.TestResult = (String.IsNullOrWhiteSpace(model.TestResult)) ? template.TestResult : model.TestResult;
            template.TestLimit = (String.IsNullOrWhiteSpace(model.TestLimit)) ? template.TestLimit : model.TestLimit;
            template.TestUnit = (String.IsNullOrWhiteSpace(model.TestUnit)) ? template.TestUnit : model.TestUnit;
            template.TestName = (String.IsNullOrWhiteSpace(model.TestName)) ? template.TestName : model.TestName;
            template.ModifiedDate = DateTime.UtcNow.AddHours(1);

            _dPRTemplate.DataStore.Update(template);
            await _dPRTemplate.Save();

            return "Success";
        }

        public async Task<string> DeleteDPRTemplate(Guid templateId)
        {
            var template = await _dPRTemplate.DataStore.GetById(templateId);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            await _dPRTemplate.DataStore.Delete(template.ID);
            await _dPRTemplate.Save();

            return "Success";
        }

        #endregion

        #region Field DPR

        public async Task<string> AddDPRTestResult(AddDPRTestResults model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var imageId = await _imageUploadService.UploadImageToDatabase(model.Picture);

            var dpr = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == model.DPRFieldId && x.SamplePointLocationId == model.samplePtId)
                .Include(x => x.FieldLocations)
                .Include(x => x.DPRSamples)
                .FirstOrDefaultAsync();
            if (dpr == null) throw new KeyNotFoundException("Dpr with Id not found!");

            dpr.ImageModelId = imageId;
            dpr.PamsUserId = userId.ToString();
            dpr.Submitted = true;
            dpr.TimeModified = DateTime.UtcNow.AddHours(1);
            dpr.FieldLocations.Latitude = model.Latitude;
            dpr.FieldLocations.Longitude = model.Longitude;

            if (model.DPRTemplates.Any())
            {
                foreach (var sampleResult in model.DPRTemplates)
                {
                    var result = dpr.DPRSamples.FirstOrDefault(x => x.Id == sampleResult.Id);

                    result.TestLimit = (sampleResult.TestLimit is null) ? result.TestLimit : sampleResult.TestLimit;
                    result.TestResult = sampleResult.TestResult;

                    _dPRFieldResult.DataStore.Update(result);
                    await _dPRFieldResult.Save();
                }
            }
            
            _dPRFieldStoreManager.DataStore.Update(dpr);
            await _dPRFieldStoreManager.Save();

            return "Success";
        }

        public async Task<string> AddOrUpdateDPRSpecificTestTestResultByName(DPRTestResult model)
        {
            var dpr = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == model.DPRFieldId)
                .Include(x => x.DPRSamples)
                .FirstOrDefaultAsync();

            if (dpr is null) return "Dpr with Id Not Found";

            var result = dpr.DPRSamples.FirstOrDefault(x => x.Id == model.Id);
            if (result is null) return "Dpr Sample Not Found";

            result.TestLimit = (model.TestLimit is null) ? result.TestLimit : model.TestLimit;
            result.TestResult = (model.TestResult is null) ? result.TestResult : model.TestResult;
            result.TimeModified = DateTime.Now;

            _dPRFieldResult.DataStore.Update(result);
            await _dPRFieldResult.Save();

            return "Success";
        }

        public async Task<SampleDprTestVM> GetDPRSpecificTestTemplatesByName(DPRTestName model)
        {
            var sampleVM = new SampleDprTestVM();
            var dpr = new DPRField();

            dpr = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Include(x => x.DPRSamples)
                .FirstOrDefaultAsync(x => x.Id == model.DPRFieldId);

            if (dpr == null) throw new KeyNotFoundException("Dpr with ID not found!");

            var result = dpr.DPRSamples.FirstOrDefault(x => x.Id == model.Id);

            if (result is null) return sampleVM;
           
            sampleVM.Id = result.Id;
            sampleVM.DPRFieldId = dpr.Id;
            sampleVM.TestName = result.TestName;
            sampleVM.TestUnit = result.TestUnit;
            sampleVM.TestLimit = result.TestLimit;
            sampleVM.TestResult = result.TestResult;

            return sampleVM;
        }

        public async Task<FieldScientistDPRTestsVM> GetDPRTemplates(DPRsCreateRequestVM model)
        {
            var fieldScientistsDprVm = new FieldScientistDPRTestsVM();

            //check if the location is existing
            var samplePointLocation = await _samplePointLocationStoreManager.DataStore.Find(x => x.Id == model.LocationId);
            if (samplePointLocation.Count() == 0) throw new KeyNotFoundException("Sample Point Location not found!");

            //As far as submitted is false, return the entity
            var dpr = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.SamplePointLocationId == model.LocationId && x.Submitted == false)
                .Include(x => x.DPRSamples)
                .FirstOrDefaultAsync();

            if (dpr is null)
            {
                //create a sample test
                var addNewDprSample = new DPRField { SamplePointLocationId = model.LocationId };
                await _dPRFieldStoreManager.DataStore.Add(addNewDprSample);
                await _dPRFieldStoreManager.Save();

                var fieldLocation = new FieldLocation { DPRFieldId = addNewDprSample.Id };
                await _fileLocationStoreManager.DataStore.Add(fieldLocation);
                await _fileLocationStoreManager.Save();

                var dprSamples = new List<DPRFieldResult>();

                var dprTemplates = GetAllDPRTemplates().Result;
                foreach (var template in dprTemplates)
                {
                    dprSamples.Add(new DPRFieldResult
                    {
                        DPRFieldId = addNewDprSample.Id,
                        TestLimit = template.TestLimit,
                        TestName = template.TestName,
                        TestResult = template.TestResult,
                        TestUnit = template.TestUnit
                    });
                }
                await _dPRFieldResult.DataStore.AddRange(dprSamples);
                await _dPRFieldResult.Save();

                addNewDprSample.FieldLocations = fieldLocation;
                addNewDprSample.DPRSamples = dprSamples;

                fieldScientistsDprVm = addNewDprSample;
                return fieldScientistsDprVm;
            }

            fieldScientistsDprVm = dpr;
            return fieldScientistsDprVm;
        }

        public async Task<byte[]> DownloadDPRTestToExcel(DprsResultVM model)
        {
            var arrOfByte = new byte[0];

            var resultTemplate = await GetDPRTestForPrinting(model.LocationId, model.DprFieldId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Dpr Test Result Template");

                for (int i = 1; i <= 18; i++)
                {
                    var headFormat = worksheet.Cell(1, i);
                    headFormat.Style.Font.SetBold();
                    headFormat.WorksheetRow().Height = 20;

                    var secondHeadFormat = worksheet.Cell(2, i);
                    secondHeadFormat.Style.Font.SetBold();
                    secondHeadFormat.WorksheetRow().Height = 30;
                }

                worksheet.Cell(1, 1).Value = "Customer Name";
                worksheet.Cell(1, 2).Value = "Location";
                worksheet.Cell(1, 3).Value = "Latitude";
                worksheet.Cell(1, 4).Value = "Longitude";

                worksheet.Cell(2, 1).Value = "DPR Limit";

                worksheet.Cell(3, 1).Value = $"{resultTemplate.ClientName}";
                worksheet.Cell(3, 2).Value = $"{resultTemplate.SamplePointName}";
                worksheet.Cell(3, 3).Value = $"{resultTemplate.Location.Latitude}";
                worksheet.Cell(3, 4).Value = $"{resultTemplate.Location.Longitude}";

                var count = 5;
                foreach (var dPrResult in resultTemplate.DprSamples)
                {
                    worksheet.Cell(1, count).Value = $"{dPrResult.TestName} {dPrResult.TestUnit}";
                    worksheet.Cell(2, count).Value = $"{dPrResult.TestLimit}";
                    worksheet.Cell(3, count).Value = $"{dPrResult.TestResult} ";
                    count++;
                }
                
                worksheet.Cell(1, count).Value = "Time";

                worksheet.Cell(3, count).Value = $"{resultTemplate.Time}";

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    arrOfByte = content;

                    return arrOfByte;
                }
            }
        }

        public PagedResponse<AllFieldScientistDPRTestsVM> GetAllDPRTest(int pageSize, int pageNumber, string keyword)
        {
            var listAllFieldScientistDPRTestsVM = new List<AllFieldScientistDPRTestsVM>();
            var listOfDPRField = new List<DPRField>();
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfDPRField = _dPRFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true)
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.DPRSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            else
            {
                listOfDPRField = _dPRFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.DPRSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }

            listAllFieldScientistDPRTestsVM = listOfDPRField.Select(x => (AllFieldScientistDPRTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistDPRTestsVM> { Data = listAllFieldScientistDPRTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = listOfDPRField.Count() };
        }

        public PagedResponse<AllFieldScientistDPRTestsVM> GetAllDPRTestByAnalystId(int pageSize, int pageNumber, string keyword)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var listAllFieldScientistDPRTestsVM = new List<AllFieldScientistDPRTestsVM>();
            var listOfDPRField = new List<DPRField>();
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfDPRField = _dPRFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString())
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.DPRSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            else
            {
                listOfDPRField = _dPRFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString() && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.DPRSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }

            listAllFieldScientistDPRTestsVM = listOfDPRField.Select(x => (AllFieldScientistDPRTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistDPRTestsVM> { Data = listAllFieldScientistDPRTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = listOfDPRField.Count() };
        }

        public async Task<ImageVM> DownloadDPRTestPhoto(DprsResultVM model)
        {
            var resultModel = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == model.DprFieldId && x.SamplePointLocationId == model.LocationId)
                .Include(x => x.ImageModel)
                .FirstOrDefaultAsync();

            var image = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

            return image;
        }

        public async Task<AllFieldScientistDPRTestsVM> GetDPRTestById(DprsResultVM model)
        {
            return await GetDPRTestForPrinting(model.LocationId, model.DprFieldId);
        }

        private async Task<AllFieldScientistDPRTestsVM> GetDPRTestForPrinting(long samplelocationId, long dprFieldId)
        {
            var imageDetail = new ImageVM();
            var allFieldScientistDPRTestsVM = new AllFieldScientistDPRTestsVM();

            var resultModel = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == dprFieldId && x.SamplePointLocationId == samplelocationId)
                .OrderByDescending(x => x.TimeModified)
                .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                .Include(x => x.PamsUser)
                .Include(x => x.FieldLocations)
                .Include(x => x.DPRSamples)
                .FirstOrDefaultAsync();

            allFieldScientistDPRTestsVM = resultModel;

            //get image as base64 string
            if (!(resultModel.ImageModelId is null))
            {
                imageDetail = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

                allFieldScientistDPRTestsVM.ImageDetails = imageDetail;
            }

            return allFieldScientistDPRTestsVM;
        }

        public async Task<AnalysisDaysCount> DPRTestCountPastSevenDays()
        {
            var analysisDaysCount = new AnalysisDaysCount();

            var result = await _dPRFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true).ToListAsync();

            int todayCount = result.Where(x => x.TimeModified.Day == DateTime.Today.Day && x.TimeModified.Month == DateTime.Today.Month && x.TimeModified.Year == DateTime.Today.Year).Count();
            int yesterdayCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-1).Day && x.TimeModified.Month == DateTime.Today.AddDays(-1).Month && x.TimeModified.Year == DateTime.Today.AddDays(-1).Year).Count();
            int twoDaysBackCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-2).Day && x.TimeModified.Month == DateTime.Today.AddDays(-2).Month && x.TimeModified.Year == DateTime.Today.AddDays(-2).Year).Count();
            int threeDaysBackCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-3).Day && x.TimeModified.Month == DateTime.Today.AddDays(-3).Month && x.TimeModified.Year == DateTime.Today.AddDays(-3).Year).Count();
            int fourDaysBackCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-4).Day && x.TimeModified.Month == DateTime.Today.AddDays(-4).Month && x.TimeModified.Year == DateTime.Today.AddDays(-4).Year).Count();
            int fiveDaysBackCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-5).Day && x.TimeModified.Month == DateTime.Today.AddDays(-5).Month && x.TimeModified.Year == DateTime.Today.AddDays(-5).Year).Count();
            int sixDaysCount = result.Where(x => x.TimeModified.Day == DateTime.Today.AddDays(-6).Day && x.TimeModified.Month == DateTime.Today.AddDays(-6).Month && x.TimeModified.Year == DateTime.Today.AddDays(-6).Year).Count();

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
