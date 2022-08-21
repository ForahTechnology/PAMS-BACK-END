using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Interfaces.Services;
using PAMS.DTOs.Request.FieldScientistRequest;
using PAMS.DTOs.Response;
using PAMS.DTOs.Response.FieldScientistResponse;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FieldScientistAnalysisNesreaController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IFieldScientistAnalysisNesreaService _fieldAnalysisService;

        public FieldScientistAnalysisNesreaController(
            IReportService reportService,
            IFieldScientistAnalysisNesreaService fieldAnalysisService)
        {
            _fieldAnalysisService = fieldAnalysisService;
            _reportService = reportService;
        }

        #region Field NESREA Tests

        /// <summary>
        /// Create field nesrea test
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("field-add-new-nesrea-test")]
        public async Task<IActionResult> CreateNESREATest(NesreaTemplatesVm model)
        {
            var response = await _fieldAnalysisService.CreateNesreaSampleTemplate(model);
            return Ok(response);
        }

        /// <summary>
        /// Get All tests under field nesrea 
        /// </summary>
        /// <returns></returns>
        [HttpGet("field-get-all-nesrea-tests")]
        public async Task<IActionResult> GetAllNESREATests()
        {
            var response = await _fieldAnalysisService.GetAllNesreaTemplates();
            return Ok(response);
        }

        /// <summary>
        /// The endpoint updates the nesrea Test.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("field-update-nesrea-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> UpdateNESREATest(UpdateNesreaTemplateVm model)
        {
            var response = await _fieldAnalysisService.UpdateNesreaTemplate(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint delete a nesrea Test.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpDelete("field-delete-nesrea-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteNESREATest(Guid templateId)
        {
            var response = await _fieldAnalysisService.DeleteNesreaTemplate(templateId);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        #endregion

        #region FIELD NESREA 

        /// <summary>
        /// This endpoint returns Nesrea Templates with all the Test templates that belongs to the Sample Location whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-nesrea-template")]
        [ProducesResponseType(200, Type = typeof(FieldScientistNesreaTestsVM))]
        public async Task<IActionResult> GetNesreaTemplates([FromQuery] CreateNesreamVM model)
        {
            var response = await _fieldAnalysisService.GetNesreaTemplates(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });

        }

        /// <summary>
        /// This endpoint returns Nesrea Test Template that belongs to nesrea template whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-nesrea-test-template")]
        [ProducesResponseType(200, Type = typeof(SampleTestVM))]
        public async Task<IActionResult> GetNesreaSpecificTestTemplatesByName([FromQuery] NesreaTestName model)
        {
            var response = await _fieldAnalysisService.GetNesreaSpecificTestTemplatesByName(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint update the value of each Test result of any test conducted under nasrea.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add-nesrea-test-Testresult-ForEachTest")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddOrUpdateNesreaSpecificTestTestResultByName([FromQuery] NesreaTestResult model)
        {
            var response = await _fieldAnalysisService.AddOrUpdateNesreaSpecificTestTestResultByName( model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint for final submission of all result values under nasrea.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("submit-nesrea-test-Testresult")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddNesreaTestResult([FromForm] AddNesreaTestResults model)
        {
            var response = await _fieldAnalysisService.AddNesreaTestResult(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint Gets all the nesrea tests on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllNesreaSubmittedTest")]
        public IActionResult GetAllNesreaTest(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisService.GetAllNesreaTest(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets all the nesrea tests by client name on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllNesreaSubmittedTestByClientName")]
        public IActionResult GetAllNesreaTestByClientName(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisService.GetAllNesreaTestByClientName(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets all the nesrea tests on this application Submitted by the Analyst.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllNesreaSubmittedTestByAnalystId")]
        public IActionResult GetAllNesreaTestByAnalyst(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisService.GetAllNesreaTestByAnalystId(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets nesrea test per Id for a location.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetNesreaSubmittedTestPerId")]
        public async Task<IActionResult> GetNesreaTestById([FromQuery] NesreamResultVM model)
        {
            var response = await _fieldAnalysisService.GetNesreaTestById(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = response

            });
        }

        /// <summary>
        /// With this endpoint you can download nesrea test result
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-nesrea-test-result")]
        public async Task<IActionResult> DownloadNesreaTestToExcel([FromQuery] NesreamResultVM model)
        {
            var testData = await _fieldAnalysisService.DownloadNesreaTestToExcel(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = testData

            });
        }

        /// <summary>
        /// With this endpoint you can download photo submitted with the nesrea test
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-nesrea-test-photo")]
        public async Task<IActionResult> DownloadNesreaPhoto([FromQuery] NesreamResultVM model)
        {
            var testData = await _fieldAnalysisService.DownloadNesreaTestPhoto(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = testData

            });
        }

        #endregion

        #region Location

        /// <summary>
        /// Add Sample Location On Client Site
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add-client-location")]
        public async Task<IActionResult> AddLocationByClientId([FromBody] CreateSampleLocationRequest model)
        {
            if (model.ClientId == Guid.Empty || model.Name == null) throw new NullReferenceException("All fields are required!");
            var response = await _fieldAnalysisService.AddLocationByClientId(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = response

            });
        }

        /// <summary>
        /// This endpoint gets all sample locations under a particular Client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("get-all-Sample-locations-for-a-Client")]
        [ProducesResponseType(200, Type = typeof(List<SampleLocationVM>))]
        public async Task<IActionResult> GetAllSampleLocationsByClientId([FromQuery] Guid clientId)
        {
            var response = await _fieldAnalysisService.GetAllSampleLocationsByClientId(clientId);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// Update a Sample Location by Id
        /// </summary>
        /// <param name="updateSampleLocationRequest"></param>
        /// <returns></returns>
        [HttpPut("Update-a-client-sample-location")]
        public async Task<IActionResult> UpdateSampleLocation([FromQuery] UpdateSampleLocationRequest updateSampleLocationRequest)
        {
            var response = await _fieldAnalysisService.UpdateSampleLocation(updateSampleLocationRequest);
            return Ok(new ResponseViewModel { Message = "Location Updated", ReturnObject = response });
        }

        /// <summary>
        /// Delete a Sample Location by Id
        /// </summary>
        /// <param name="sampleLocationId"></param>
        /// <returns></returns>
        [HttpDelete("delete-a-client-sample-location/{sampleLocationId}")]
        public async Task<IActionResult> DeleteSampleLocation([FromRoute] long sampleLocationId)
        {
            var response = await _fieldAnalysisService.DeleteSampleLocation(sampleLocationId);
            return Ok(new ResponseViewModel { Message = "Location Deleted", ReturnObject = response });
        }

        #endregion
    }
}
