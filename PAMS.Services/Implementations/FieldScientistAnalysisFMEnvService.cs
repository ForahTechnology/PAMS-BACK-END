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
    public class FieldScientistAnalysisFMEnvService : IFieldScientistAnalysisFMEnvService
    {
        private readonly UserManager<PamsUser> _userManager;
        private readonly IContextAccessor _contextAccessor;
        private readonly IUploadService _imageUploadService;

        private readonly IStoreManager<FMENVField> _fmenvFieldStoreManager;
        private readonly IStoreManager<FieldLocation> _fileLocationStoreManager;
        private readonly IStoreManager<SamplePointLocation> _samplePointLocationStoreManager;

        private readonly IStoreManager<FMENVTemplate> _fMENVTemplate;
        private readonly IStoreManager<FMENVFieldResult> _fMENVFieldResult;

        public FieldScientistAnalysisFMEnvService(
            IUploadService imageUploadService,
            UserManager<PamsUser> userManager,
            IContextAccessor contextAccessor,
            IStoreManager<FMENVField> fmenvFieldStoreManager,
            IStoreManager<FieldLocation> fileLocationStoreManager,
            IStoreManager<FMENVTemplate> fMENVTemplate,
            IStoreManager<FMENVFieldResult> fMENVFieldResult,
            IStoreManager<SamplePointLocation> samplePointLocationStoreManager
            )
        {
            _imageUploadService = imageUploadService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _fmenvFieldStoreManager = fmenvFieldStoreManager;
            _fileLocationStoreManager = fileLocationStoreManager;
            _fMENVFieldResult = fMENVFieldResult;
            _fMENVTemplate = fMENVTemplate;
            _samplePointLocationStoreManager = samplePointLocationStoreManager;
        }

        #region FMENV Field Template

        public async Task<List<FMENVTemplate>> CreateFMENVSampleTemplate(FMENVTemplatesVm model)
        { 
            var fmenvTemplates = new List<FMENVTemplate>();

            foreach (var template in model.FMENVTemplates)
            {
                fmenvTemplates.Add(new FMENVTemplate
                {
                    ID = Guid.NewGuid(),
                    TestLimit = template.TestLimit,
                    TestName = template.TestName,
                    TestResult = template.TestResult,
                    TestUnit = template.TestUnit
                });
            }

            await _fMENVTemplate.DataStore.AddRange(fmenvTemplates);
            await _fMENVTemplate.Save();
            return fmenvTemplates;
        }

        public async Task<List<FMENVTemplate>> GetAllFmenvTemplates()
        {
            return await _fMENVTemplate.DataStore.GetAllQuery().ToListAsync();
        }

        public async Task<string> UpdateFMenvTemplate(UpdateFMENVTemplateVm model)
        {
            var template = await _fMENVTemplate.DataStore.GetById(model.ID);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            template.TestResult = (String.IsNullOrWhiteSpace(model.TestResult)) ? template.TestResult : model.TestResult;
            template.TestLimit = (String.IsNullOrWhiteSpace(model.TestLimit)) ? template.TestLimit : model.TestLimit;
            template.TestUnit = (String.IsNullOrWhiteSpace(model.TestUnit)) ? template.TestUnit : model.TestUnit;
            template.TestName = (String.IsNullOrWhiteSpace(model.TestName)) ? template.TestName : model.TestName;
            template.ModifiedDate = DateTime.UtcNow.AddHours(1);

            _fMENVTemplate.DataStore.Update(template);
            await _fMENVTemplate.Save();

            return "Success";
        }

        public async Task<string> DeleteFMENVTemplate(Guid templateId)
        {
            var template = await _fMENVTemplate.DataStore.GetById(templateId);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            await _fMENVTemplate.DataStore.Delete(template.ID);
            await _fMENVTemplate.Save();

            return "success";
        }

        #endregion

        #region Field FMENV

        public async Task<string> AddFMEnvTestResult(AddFMEnvTestResults model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var imageId = await _imageUploadService.UploadImageToDatabase(model.Picture);

            var fmenv = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == model.FMEnvFieldId && x.SamplePointLocationId == model.samplePtId)
                .Include(x => x.FieldLocations)
                .Include(x => x.FMENVSamples)
                .FirstOrDefaultAsync();
            if (fmenv == null) throw new KeyNotFoundException("FMENV with Id not found!");

            fmenv.ImageModelId = imageId;
            fmenv.PamsUserId = userId.ToString();
            fmenv.Submitted = true;
            fmenv.TimeModified = DateTime.UtcNow.AddHours(1);
            fmenv.FieldLocations.Latitude = model.Latitude;
            fmenv.FieldLocations.Longitude = model.Longitude;

            if (model.FMENVTemplates.Any())
            {
                foreach (var sampleResult in model.FMENVTemplates)
                {
                    var result = fmenv.FMENVSamples.FirstOrDefault(x => x.Id == sampleResult.Id);

                    result.TestLimit = (sampleResult.TestLimit is null) ? result.TestLimit : sampleResult.TestLimit;
                    result.TestResult = sampleResult.TestResult;

                    _fMENVFieldResult.DataStore.Update(result);
                    await _fMENVFieldResult.Save();
                }
            }
            
            _fmenvFieldStoreManager.DataStore.Update(fmenv);
            await _fmenvFieldStoreManager.Save();

            return "Success";
        }

        public async Task<string> AddOrUpdateFMEnvSpecificTestTestResultByName(FMEnvTestResult model)
        {
            var fmenv = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
               .Where(x => x.Id == model.FMEnvFieldId)
               .Include(x => x.FMENVSamples)
               .FirstOrDefaultAsync();

            if (fmenv is null) return "FMENV with Id Not Found";

            var result = fmenv.FMENVSamples.FirstOrDefault(x => x.Id == model.Id);
            if (result is null) return "FMENV Sample Not Found";

            result.TestLimit = (model.TestLimit is null) ? result.TestLimit : model.TestLimit;
            result.TestResult = (model.TestResult is null) ? result.TestResult : model.TestResult;
            result.TimeModified = DateTime.Now;

            _fmenvFieldStoreManager.DataStore.Update(fmenv);
            await _fmenvFieldStoreManager.Save();

            return "Success";
        }

        public async Task<SampleFMEnvTestVM> GetFMEnvSpecificTestTemplatesByName(FMEnvTestName model)
        {
            var sampleVM = new SampleFMEnvTestVM();
            var fmenv = new FMENVField();

            fmenv = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Include(x => x.FMENVSamples)
                .FirstOrDefaultAsync(x => x.Id == model.FMEnvFieldId);

            if (fmenv == null) throw new KeyNotFoundException("FMENV with ID not found!");

            var result = fmenv.FMENVSamples.FirstOrDefault(x => x.Id == model.Id);

            if (result is null) return sampleVM;

            sampleVM.Id = result.Id;
            sampleVM.FMEnvFieldId = fmenv.Id;
            sampleVM.TestName = result.TestName;
            sampleVM.TestUnit = result.TestUnit;
            sampleVM.TestLimit = result.TestLimit;
            sampleVM.TestResult = result.TestResult;

            return sampleVM;
        }

        public async Task<FieldScientistFMEnvTestsVM> GetFMEnvTemplates(FMEnvsCreateRequestVM model)
        {
            var fieldScientistsFmenvVm = new FieldScientistFMEnvTestsVM();

            //check the sample point location is existing
            var samplePointLocation = await _samplePointLocationStoreManager.DataStore.Find(x => x.Id == model.LocationId);
            if (samplePointLocation.Count() == 0) throw new KeyNotFoundException("Sample Point Location not found!");

            //As far as submitted is false, return the entity
            var fmenv = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.SamplePointLocationId == model.LocationId && x.Submitted == false)
                .Include(x => x.FMENVSamples)
                .FirstOrDefaultAsync();

            if (fmenv is null)
            {
                //create a sample test
                var addNewFmenvSample = new FMENVField { SamplePointLocationId = model.LocationId };
                await _fmenvFieldStoreManager.DataStore.Add(addNewFmenvSample);
                await _fmenvFieldStoreManager.Save();

                var fieldLocation = new FieldLocation { FMENVFieldId = addNewFmenvSample.Id };
                await _fileLocationStoreManager.DataStore.Add(fieldLocation);
                await _fileLocationStoreManager.Save();

                var fmenvSamples = new List<FMENVFieldResult>();

                var dprTemplates = GetAllFmenvTemplates().Result;
                foreach (var template in dprTemplates)
                {
                    fmenvSamples.Add(new FMENVFieldResult
                    {
                        FMENVFieldId = addNewFmenvSample.Id,
                        TestLimit = template.TestLimit,
                        TestName = template.TestName,
                        TestResult = template.TestResult,
                        TestUnit = template.TestUnit
                    });
                }

                await _fMENVFieldResult.DataStore.AddRange(fmenvSamples);
                await _fMENVFieldResult.Save();

                addNewFmenvSample.FieldLocations = fieldLocation;
                addNewFmenvSample.FMENVSamples = fmenvSamples;

                fieldScientistsFmenvVm = addNewFmenvSample;
                return fieldScientistsFmenvVm;
            }

            fieldScientistsFmenvVm = fmenv;
            return fieldScientistsFmenvVm;
        }

        public PagedResponse<AllFieldScientistFMEnveTestsVM> GetAllFMEnvTest(int pageSize, int pageNumber, string keyword)
        {
            var listAllFieldScientistFMEnvTestsVM = new List<AllFieldScientistFMEnveTestsVM>();
            var listOfFMEnvField = new List<FMENVField>();
            var totalFMEnvField = 0;
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true)
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.FMENVSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true).Count();
            }
            else
            {
                listOfFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.FMENVSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower())).Count();
            }

            listAllFieldScientistFMEnvTestsVM = listOfFMEnvField.Select(x => (AllFieldScientistFMEnveTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistFMEnveTestsVM> { Data = listAllFieldScientistFMEnvTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalFMEnvField };
        }

        public PagedResponse<AllFieldScientistFMEnveTestsVM> GetAllFMEnvTestByClientName(int pageSize, int pageNumber, string keyword)
        {
            var listAllFieldScientistFMEnvTestsVM = new List<AllFieldScientistFMEnveTestsVM>();
            var listOfFMEnvField = new List<FMENVField>();
            var totalFMEnvField = 0;
            var searchKey = keyword;

            listOfFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.SamplePointLocation.Client.Name.ToLower().Contains(searchKey.ToLower()))
                .OrderByDescending(x => x.TimeModified)
                .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                .Include(x => x.FieldLocations)
                .Include(x => x.PamsUser)
                .Include(x => x.FMENVSamples)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            totalFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.SamplePointLocation.Client.Name.ToLower().Contains(searchKey.ToLower())).Count();

            listAllFieldScientistFMEnvTestsVM = listOfFMEnvField.Select(x => (AllFieldScientistFMEnveTestsVM)x).ToList();
            return new PagedResponse<AllFieldScientistFMEnveTestsVM> { Data = listAllFieldScientistFMEnvTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalFMEnvField };
        }

        public PagedResponse<AllFieldScientistFMEnveTestsVM> GetAllFMEnvTestByAnalystId(int pageSize, int pageNumber, string keyword)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var listAllFieldScientistFMEnvTestsVM = new List<AllFieldScientistFMEnveTestsVM>();
            var listOfFMEnvField = new List<FMENVField>();
            var totalFMEnvField = 0;
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString())
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.FMENVSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString()).Count();
            }
            else
            {
                listOfFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString() && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.FMENVSamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalFMEnvField = _fmenvFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString() && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower())).Count();
            }

            listAllFieldScientistFMEnvTestsVM = listOfFMEnvField.Select(x => (AllFieldScientistFMEnveTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistFMEnveTestsVM> { Data = listAllFieldScientistFMEnvTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalFMEnvField };
        }

        public async Task<byte[]> DownloadFMEnvTestToExcel(FmEnvResultVM model)
        {
            var arrOfByte = new byte[0];

            var resultTemplate = await GetFMEnvTestForPrinting(model.LocationId, model.FMEnvFieldId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("FMEnv Test Result Template");

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
                
                worksheet.Cell(2, 1).Value = "FMEnv Limit";
                
                worksheet.Cell(3, 1).Value = $"{resultTemplate.ClientName}";
                worksheet.Cell(3, 2).Value = $"{resultTemplate.SamplePointName}";
                worksheet.Cell(3, 3).Value = $"{resultTemplate.Location.Latitude}";
                worksheet.Cell(3, 4).Value = $"{resultTemplate.Location.Longitude}";

                var count = 5;
                foreach (var fmenvResult in resultTemplate.FMENVSamples)
                {
                    worksheet.Cell(1, count).Value = $"{fmenvResult.TestName} {fmenvResult.TestUnit}";
                    worksheet.Cell(2, count).Value = $"{fmenvResult.TestLimit}";
                    worksheet.Cell(3, count).Value = $"{fmenvResult.TestResult} ";
                    count++;
                }

                worksheet.Cell(1, count).Value = "Time";
                worksheet.Cell(3, count).Value = $"{resultTemplate.Time.ToString("Y")}"; //check this out

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    arrOfByte = content;

                    return arrOfByte;
                }
            }
        }

        public async Task<ImageVM> DownloadFMEnvTestPhoto(FmEnvResultVM model)
        {
            var resultModel = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == model.FMEnvFieldId && x.SamplePointLocationId == model.LocationId)
                .Include(x => x.ImageModel)
                .FirstOrDefaultAsync();

            var image = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

            return image;
        }

        public async Task<AllFieldScientistFMEnveTestsVM> GetFMEnvTestById(FmEnvResultVM model)
        {
            return await GetFMEnvTestForPrinting(model.LocationId, model.FMEnvFieldId);
        }

        private async Task<AllFieldScientistFMEnveTestsVM> GetFMEnvTestForPrinting(long samplelocationId, long fmenvFieldId)
        {
            var imageDetail = new ImageVM();
            var allFieldScientistFMEnvTestsVM = new AllFieldScientistFMEnveTestsVM();

            var resultModel = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == fmenvFieldId && x.SamplePointLocationId == samplelocationId)
                .OrderByDescending(x => x.TimeModified)
                .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                .Include(x => x.FieldLocations)
                .Include(x => x.PamsUser)
                .Include(x => x.ImageModel)
                .Include(x => x.FMENVSamples)
                .FirstOrDefaultAsync();

            allFieldScientistFMEnvTestsVM = resultModel;

            //get image as base64 string
            if (!(resultModel.ImageModelId is null))
            {
                imageDetail = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

                allFieldScientistFMEnvTestsVM.ImageDetails = imageDetail;
            }
            
            return allFieldScientistFMEnvTestsVM;
        }

        public async Task<AnalysisDaysCount> FMEnvTestCountPastSevenDays()
        {
            var analysisDaysCount = new AnalysisDaysCount();

            var result = await _fmenvFieldStoreManager.DataStore.GetAllQuery()
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
