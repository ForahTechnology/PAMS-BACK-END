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
    public class FieldScientistAnalysisNesreaService : IFieldScientistAnalysisNesreaService
    {
        private readonly UserManager<PamsUser> _userManager;
        private readonly IContextAccessor _contextAccessor;
        private readonly IUploadService _imageUploadService;

        private readonly IStoreManager<SamplePointLocation> _samplePointLocationStoreManager;
        private readonly IStoreManager<NESREAField> _nesreaFieldStoreManager;
        private readonly IStoreManager<FMENVField> _fmenvFieldStoreManager;
        private readonly IStoreManager<Client> _clientStoreManager;
        private readonly IStoreManager<FieldLocation> _fileLocationStoreManager;

        private readonly IStoreManager<NESREATemplate> _nesreaTemplate;
        private readonly IStoreManager<NESREAFieldResult> _nesreaFieldResult;

        public FieldScientistAnalysisNesreaService(
            UserManager<PamsUser> userManager,
            IContextAccessor contextAccessor,
            IUploadService imageUploadService,
            IStoreManager<Client> clientStoreManager,
            IStoreManager<FMENVField> fmenvFieldStoreManager,
            IStoreManager<NESREAField> nesreaFieldStoreManager,
            IStoreManager<SamplePointLocation> samplePointLocationStoreManager,
            IStoreManager<FieldLocation> fileLocationStoreManager,
            IStoreManager<NESREAFieldResult> nesreaFieldResult,
            IStoreManager<NESREATemplate> nesreaTemplate
            )
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _imageUploadService = imageUploadService;
            _clientStoreManager = clientStoreManager;
            _fmenvFieldStoreManager = fmenvFieldStoreManager;
            _nesreaFieldStoreManager = nesreaFieldStoreManager;
            _samplePointLocationStoreManager = samplePointLocationStoreManager;
            _fileLocationStoreManager = fileLocationStoreManager;
            _nesreaFieldResult = nesreaFieldResult;
            _nesreaTemplate = nesreaTemplate;
        }

        #region Nesrea Field Template

        public async Task<List<NESREATemplate>> CreateNesreaSampleTemplate(NesreaTemplatesVm model)
        {
            var nesreaTemplates = new List<NESREATemplate>();

            foreach (var template in model.NesreaTemplates)
            {
                nesreaTemplates.Add(new NESREATemplate
                {
                    ID = Guid.NewGuid(),
                    TestLimit = template.TestLimit,
                    TestName = template.TestName,
                    TestResult = template.TestResult,
                    TestUnit = template.TestUnit
                });
            }

            await _nesreaTemplate.DataStore.AddRange(nesreaTemplates);
            await _nesreaTemplate.Save();
            return nesreaTemplates;
        }

        public async Task<List<NESREATemplate>> GetAllNesreaTemplates()
        {
            return await _nesreaTemplate.DataStore.GetAllQuery().ToListAsync();
        }

        public async Task<string> UpdateNesreaTemplate(UpdateNesreaTemplateVm model)
        {
            var template = await _nesreaTemplate.DataStore.GetById(model.ID);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            template.TestResult = (String.IsNullOrWhiteSpace(model.TestResult)) ? template.TestResult : model.TestResult;
            template.TestLimit = (String.IsNullOrWhiteSpace(model.TestLimit)) ? template.TestLimit : model.TestLimit;
            template.TestUnit = (String.IsNullOrWhiteSpace(model.TestUnit)) ? template.TestUnit : model.TestUnit;
            template.TestName = (String.IsNullOrWhiteSpace(model.TestName)) ? template.TestName : model.TestName;
            template.ModifiedDate = DateTime.UtcNow.AddHours(1);

            _nesreaTemplate.DataStore.Update(template);
            await _nesreaTemplate.Save();

            return "Success";
        }

        public async Task<string> DeleteNesreaTemplate(Guid templateId)
        {
            var template = await _nesreaTemplate.DataStore.GetById(templateId);
            if (template == null) throw new KeyNotFoundException("Template not found!");

            await _nesreaTemplate.DataStore.Delete(template.ID);
            await _nesreaTemplate.Save();

            return "Success";
        }

        #endregion

        #region Sample Point Location 

        public async Task<long> AddLocationByClientId(CreateSampleLocationRequest model)
        {
            var client = await _clientStoreManager.DataStore.GetById(model.ClientId);
            if (client == null) throw new KeyNotFoundException($"Client with id '{model.ClientId}' does not exist");

            var locationDetial = new SamplePointLocation
            {
                ClientID = client.ID,
                Name = model.Name,
                Description = model.Description
            };

            await _samplePointLocationStoreManager.DataStore.Add(locationDetial);
            await _samplePointLocationStoreManager.Save();

            return locationDetial.Id;
        }

        public async Task<long> DeleteSampleLocation(long sampleLocationId)
        {
            var location = await _samplePointLocationStoreManager.DataStore.GetAllQuery()
                .Include(x => x.FMENVFields).ThenInclude(x => x.FieldLocations)
                .Include(x => x.DPRFields).ThenInclude(x => x.FieldLocations)
                .Include(x => x.NESREAFields).ThenInclude(x => x.FieldLocations)
                .FirstOrDefaultAsync(x => x.Id == sampleLocationId);
            if (location == null) throw new KeyNotFoundException($"Sample Location with id '{sampleLocationId}' does not exist");

            await _samplePointLocationStoreManager.DataStore.Delete(location.Id);
            await _samplePointLocationStoreManager.Save();

            return location.Id;
        }

        public async Task<List<SampleLocationVM>> GetAllSampleLocationsByClientId(Guid clientId)
        {
            var sampleLocationVMs = new List<SampleLocationVM>();

            var listOfLocations = await _samplePointLocationStoreManager.DataStore.GetAllQuery()
                .Where( x => x.ClientID == clientId)
                .ToListAsync();

            sampleLocationVMs = listOfLocations.Select(x => (SampleLocationVM)x).ToList();

            return sampleLocationVMs;
        }

        public int LocationsCount()
        {
            return _samplePointLocationStoreManager.DataStore.GetAll().Result.Count();
        }

        public async Task<SampleLocationVM> UpdateSampleLocation(UpdateSampleLocationRequest updateSampleLocationRequest)
        {
            var location = await _samplePointLocationStoreManager.DataStore.GetById(updateSampleLocationRequest.SampleLocationId);
            if (location == null) throw new KeyNotFoundException($"Sample LOcation with id '{updateSampleLocationRequest.SampleLocationId}' does not exist");

            location.TimeModified = DateTime.UtcNow;
            location.Name = (updateSampleLocationRequest.Name == null) ? location.Name : updateSampleLocationRequest.Name;
            location.Description = (updateSampleLocationRequest.Description == null) ? location.Description : updateSampleLocationRequest.Description;

            _samplePointLocationStoreManager.DataStore.Update(location);
            await _samplePointLocationStoreManager.Save();

            var sampleLocationVM = new SampleLocationVM
            {
                Description = location.Description,
                Name = location.Name,
            };

            return sampleLocationVM;
        }

        #endregion

        #region Field NESREA

        public async Task<FieldScientistNesreaTestsVM> GetNesreaTemplates(CreateNesreamVM model)
        {
            var fieldScientistsVM = new FieldScientistNesreaTestsVM();

            //check if sample point location is existing
            var samplePointLocation = await _samplePointLocationStoreManager.DataStore.Find(x => x.Id == model.LocationId);
            if (samplePointLocation.Count() == 0) throw new KeyNotFoundException("Sample Point Location not found!");

            //As far as submitted is false, return the entity
            var nesrea = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.SamplePointLocationId == model.LocationId && x.Submitted == false)
                .Include(x => x.NESREASamples)
                .FirstOrDefaultAsync();

            if (nesrea is null)
            {
                //create a sample test
                var addNewNesreaSample = new NESREAField { SamplePointLocationId = model.LocationId };
                await _nesreaFieldStoreManager.DataStore.Add(addNewNesreaSample);
                await _nesreaFieldStoreManager.Save();

                var fieldLocation = new FieldLocation { NESREAFieldId = addNewNesreaSample.Id };
                await _fileLocationStoreManager.DataStore.Add(fieldLocation);
                await _fileLocationStoreManager.Save();

                var nesreaSamples = new List<NESREAFieldResult>();

                var nesreaTemplates = GetAllNesreaTemplates().Result;
                foreach (var template in nesreaTemplates)
                {
                    nesreaSamples.Add(new NESREAFieldResult
                    {
                        NESREAFieldId = addNewNesreaSample.Id,
                        TestLimit = template.TestLimit,
                        TestName = template.TestName,
                        TestResult = template.TestResult,
                        TestUnit = template.TestUnit
                    });
                }
                await _nesreaFieldResult.DataStore.AddRange(nesreaSamples);
                await _nesreaFieldResult.Save();

                addNewNesreaSample.FieldLocations = fieldLocation;
                addNewNesreaSample.NESREASamples = nesreaSamples;
                
                fieldScientistsVM = addNewNesreaSample;
                return fieldScientistsVM;
            }

            fieldScientistsVM = nesrea;
            return fieldScientistsVM;
        }

        public async Task<SampleTestVM> GetNesreaSpecificTestTemplatesByName(NesreaTestName model)
        {
            var sampleVM = new SampleTestVM();
            var nesrea = new NESREAField();

            nesrea = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Include(x => x.NESREASamples)
                .FirstOrDefaultAsync(x => x.Id == model.NesreaFieldId);

            if (nesrea == null) throw new KeyNotFoundException("Nesrea with ID not found");

            var result = nesrea.NESREASamples.FirstOrDefault(x => x.Id == model.Id);

            if (result == null) return sampleVM;

            sampleVM.Id = result.Id;
            sampleVM.NESREAFieldId = nesrea.Id;
            sampleVM.TestName = result.TestName;
            sampleVM.TestUnit = result.TestUnit;
            sampleVM.TestLimit = result.TestLimit;
            sampleVM.TestResult = result.TestResult;
           
            return sampleVM;
        }

        public async Task<string> AddOrUpdateNesreaSpecificTestTestResultByName(NesreaTestResult model)
        {
            var nesrea = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == model.NesreaFieldId)
                .Include(x => x.NESREASamples)
                .FirstOrDefaultAsync();
            if (nesrea is null) return "Nesrea with Id Not Found";

            var result = nesrea.NESREASamples.FirstOrDefault(x => x.Id == model.Id);
            if (result is null) return "Nesrea Sample Not Found";

            result.TestLimit = (model.TestLimit is null) ? result.TestLimit : model.TestLimit;
            result.TestResult = (model.TestResult is null) ? result.TestResult : model.TestResult;
            result.TimeModified = DateTime.Now;

            _nesreaFieldStoreManager.DataStore.Update(nesrea);
            await _nesreaFieldStoreManager.Save();

            return "Success";
        }

        public async Task<string> AddNesreaTestResult(AddNesreaTestResults model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var imageId = await _imageUploadService.UploadImageToDatabase(model.Picture);

            var nesrea = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == model.NesreaFieldId && x.SamplePointLocationId == model.samplePtId)
                .Include(x => x.FieldLocations)
                .Include(x => x.NESREASamples)
                .FirstOrDefaultAsync();
            if (nesrea == null) throw new KeyNotFoundException("Nesrea with Id not found!");

            nesrea.ImageModelId = imageId;
            nesrea.PamsUserId = userId.ToString();
            nesrea.Submitted = true;
            nesrea.TimeModified = DateTime.UtcNow.AddHours(1); //DateTime.UtcNow reads GMT time Zone, so I added +1 for Nigerian Time
            nesrea.FieldLocations.Latitude = model.Latitude;
            nesrea.FieldLocations.Longitude = model.Longitude;

            if (model.NesreaTemplates.Any())
            {
                foreach (var sampleResult in model.NesreaTemplates)
                {
                    var result = nesrea.NESREASamples.FirstOrDefault(x => x.Id == sampleResult.Id);

                    result.TestLimit = (sampleResult.TestLimit is null) ? result.TestLimit : sampleResult.TestLimit;
                    result.TestResult = sampleResult.TestResult;

                    //remove for testing
                    _nesreaFieldResult.DataStore.Update(result);
                    await _nesreaFieldResult.Save();
                }
            }
           
           
            _nesreaFieldStoreManager.DataStore.Update(nesrea);
            await _nesreaFieldStoreManager.Save();

            return "Success";
        }

        public PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTest(int pageSize, int pageNumber, string keyword)
        {
            var listAllFieldScientistNesreaTestsVM = new List<AllFieldScientistNesreaTestsVM>();
            var listOfNesreaField = new List<NESREAField>();
            var totalNesreaField = 0;
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true)
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.NESREASamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true).Count();
            }
            else
            {
                listOfNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.NESREASamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                   .Where(x => x.Submitted == true && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower())).Count();
            }
            
            listAllFieldScientistNesreaTestsVM = listOfNesreaField.Select(x => (AllFieldScientistNesreaTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistNesreaTestsVM> { Data = listAllFieldScientistNesreaTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalNesreaField };
        }


        public PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTestByClientName(int pageSize, int pageNumber, string keyword)
        {
            var listAllFieldScientistNesreaTestsVM = new List<AllFieldScientistNesreaTestsVM>();
            var listOfNesreaField = new List<NESREAField>();
            var totalNesreaField = 0;
            var searchKey = keyword;

            listOfNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Client.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.NESREASamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

            totalNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.SamplePointLocation.Client.Name.ToLower().Contains(searchKey.ToLower())).Count();

            listAllFieldScientistNesreaTestsVM = listOfNesreaField.Select(x => (AllFieldScientistNesreaTestsVM)x).ToList();
            return new PagedResponse<AllFieldScientistNesreaTestsVM> { Data = listAllFieldScientistNesreaTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalNesreaField };
        }

        public PagedResponse<AllFieldScientistNesreaTestsVM> GetAllNesreaTestByAnalystId(int pageSize, int pageNumber, string keyword)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var listAllFieldScientistNesreaTestsVM = new List<AllFieldScientistNesreaTestsVM>();
            var listOfNesreaField = new List<NESREAField>();
            var totalNesreaField = 0;
            var searchKey = keyword;

            if (searchKey is null)
            {
                listOfNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString())
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.NESREASamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString()).Count();
            }
            else
            {
                listOfNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString() && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower()))
                    .OrderByDescending(x => x.TimeModified)
                    .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                    .Include(x => x.FieldLocations)
                    .Include(x => x.PamsUser)
                    .Include(x => x.NESREASamples)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalNesreaField = _nesreaFieldStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Submitted == true && x.PamsUserId == userId.ToString() && x.SamplePointLocation.Name.ToLower().Contains(searchKey.ToLower())).Count();
            }

            listAllFieldScientistNesreaTestsVM = listOfNesreaField.Select(x => (AllFieldScientistNesreaTestsVM)x).ToList();

            return new PagedResponse<AllFieldScientistNesreaTestsVM> { Data = listAllFieldScientistNesreaTestsVM, PageNumber = pageNumber, PageSize = pageSize, Total = totalNesreaField };
        }

        public async Task<AnalysisDaysCount> NesreaTestCountPastSevenDays()
        {
            var analysisDaysCount = new AnalysisDaysCount();

            var result = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true).ToListAsync();

            var one = DateTime.Today;
            var two = DateTime.Today.AddDays(1);

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

        public async Task<byte[]> DownloadNesreaTestToExcel(NesreamResultVM model)
        {
            var arrOfByte = new byte[0];

            var resultTemplate = await GetNesreaTestForPrinting(model.LocationId, model.NesreaFieldId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Nasrea Test Result Template");

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
                
                worksheet.Cell(2, 1).Value = "NESREA Limit";
                
                worksheet.Cell(3, 1).Value = $"{resultTemplate.ClientName}";
                worksheet.Cell(3, 2).Value = $"{resultTemplate.SamplePointName}";
                worksheet.Cell(3, 3).Value = $"{resultTemplate.Location.Latitude}";
                worksheet.Cell(3, 4).Value = $"{resultTemplate.Location.Longitude}";

                var count = 5;
                foreach (var nesreaResult in resultTemplate.NesreaSamples)
                {
                    worksheet.Cell(1, count).Value = $"{nesreaResult.TestName} {nesreaResult.TestUnit}";
                    worksheet.Cell(2, count).Value = $"{nesreaResult.TestLimit}";
                    worksheet.Cell(3, count).Value = $"{nesreaResult.TestResult} ";
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

        public async Task<ImageVM> DownloadNesreaTestPhoto(NesreamResultVM model)
        {
            var resultModel = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == model.NesreaFieldId && x.SamplePointLocationId == model.LocationId)
                .Include(x => x.ImageModel)
                .FirstOrDefaultAsync();

            var image = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

            return image;
        }

        public async Task<AllFieldScientistNesreaTestsVM> GetNesreaTestById(NesreamResultVM model)
        {
            return await GetNesreaTestForPrinting(model.LocationId, model.NesreaFieldId);
        }

        private async Task<AllFieldScientistNesreaTestsVM> GetNesreaTestForPrinting(long samplelocationId, long nasreaFieldId)
        {
            var imageDetail = new ImageVM();
            var allFieldScientistNesreaTestsVM = new AllFieldScientistNesreaTestsVM();

            var resultModel = await _nesreaFieldStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Submitted == true && x.Id == nasreaFieldId && x.SamplePointLocationId == samplelocationId)
                .OrderByDescending(x => x.TimeModified)
                .Include(x => x.SamplePointLocation).ThenInclude(x => x.Client)
                .Include(x => x.FieldLocations)
                .Include(x => x.ImageModel)
                .Include(x => x.PamsUser)
                .Include(x => x.NESREASamples)
                .FirstOrDefaultAsync();

            allFieldScientistNesreaTestsVM = resultModel;

            //get image as base64 string
            if (!(resultModel.ImageModelId is null))
            {
                imageDetail = await _imageUploadService.GetFileFromDatabase(resultModel.ImageModelId);

                allFieldScientistNesreaTestsVM.ImageDetails = imageDetail;
            }

            return allFieldScientistNesreaTestsVM;
        }

        #endregion
    }
}
