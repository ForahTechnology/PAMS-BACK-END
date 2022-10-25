using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Interfaces.Services;
using PAMS.DTOs.Request.FieldScientistRequest;
using PAMS.DTOs.Response;
using PAMS.DTOs.Response.FieldScientistResponse;
using System;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FieldScientistAnalysisDPRController : BaseController
    {
        private readonly IFieldScientistAnalysisDPRService _fieldAnalysisDPRService;
        public FieldScientistAnalysisDPRController(
            IFieldScientistAnalysisDPRService fieldAnalysisDPRService)
        {
            _fieldAnalysisDPRService = fieldAnalysisDPRService;
        }

        #region Field DPR Tests

        /// <summary>
        /// Create field dpr test
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("field-add-new-dpr-test")]
        public async Task<IActionResult> CreateDPRTest(DPRTemplatesVm model)
        {
            var response = await _fieldAnalysisDPRService.CreateDPRSampleTemplate(model);
            return Ok(response);
        }

        /// <summary>
        /// Get All tests under field dpr 
        /// </summary>
        /// <returns></returns>
        [HttpGet("field-get-all-dpr-tests")]
        public async Task<IActionResult> GetAllDPRTests()
        {
            var response = await _fieldAnalysisDPRService.GetAllDPRTemplates();
            return Ok(response);
        }

        /// <summary>
        /// The endpoint updates the dpr Test.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("field-update-dpr-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> UpdateDPRTest(UpdateTemplateVm model)
        {
            var response = await _fieldAnalysisDPRService.UpdateDPRTemplate(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint delete a dpr Test.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpDelete("field-delete-dpr-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteDPRTest(Guid templateId)
        {
            var response = await _fieldAnalysisDPRService.DeleteDPRTemplate(templateId);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        #endregion

        #region Field DPR

        /// <summary>
        /// This endpoint returns DPR Templates with all the Test templates that belongs to the Sample Location whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-dpr-template")]
        [ProducesResponseType(200, Type = typeof(FieldScientistDPRTestsVM))]
        public async Task<IActionResult> GetDPRTemplates([FromQuery] DPRsCreateRequestVM model)
        {
            var response = await _fieldAnalysisDPRService.GetDPRTemplates(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });

        }

        /// <summary>
        /// This endpoint returns DPR Test Template that belongs to DPR template whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-dpr-template-per-test")]
        [ProducesResponseType(200, Type = typeof(SampleDprTestVM))]
        public async Task<IActionResult> GetDPRSpecificTestTemplatesByName([FromQuery] DPRTestName model)
        {
            var response = await _fieldAnalysisDPRService.GetDPRSpecificTestTemplatesByName(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint update the value of each Test result of any test conducted under dpr.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add-dpr-TestResult-ForEachTest")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddOrUpdateDPRSpecificTestTestResultByName([FromForm] DPRTestResult model)
        {
            var response = await _fieldAnalysisDPRService.AddOrUpdateDPRSpecificTestTestResultByName(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint for final submission of all result values under dpr.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("submit-dpr-TestResult")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddDPRTestResult([FromForm] AddDPRTestResults model)
        {
            var response = await _fieldAnalysisDPRService.AddDPRTestResult(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint Gets all the dpr tests on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllDPRSubmittedTest")]
        public IActionResult GetAllDPRTest(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisDPRService.GetAllDPRTest(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets all the dpr tests by client name on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllDPRSubmittedTestByClientName")]
        public IActionResult GetAllDPRTestByClientName(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisDPRService.GetAllDPRTestByClientName(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets all the dpr tests on this application by Analyst Id.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllDPRSubmittedTestByAnalystId")]
        public IActionResult GetAllDPRTestByAnalstId(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisDPRService.GetAllDPRTestByAnalystId(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets DPR test per Id for a location.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetDPRSubmittedTestPerId")]
        public async Task<IActionResult> GetDPRTestById([FromQuery] DprsResultVM model)
        {
            var clientList = await _fieldAnalysisDPRService.GetDPRTestById(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// With this endpoint you can download dpr test result
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-dpr-test-result")]
        public async Task<IActionResult> DownloadDPRTestToExcel([FromQuery] DprsResultVM model)
        {
            var testData = await _fieldAnalysisDPRService.DownloadDPRTestToExcel(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = testData

            }); ;
        }

        /// <summary>
        /// With this endpoint you can download photo submitted with the dpr test
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-dpr-test-photo")]
        public async Task<IActionResult> DownloadDPRPhoto([FromQuery] DprsResultVM model)
        {
            var testData = await _fieldAnalysisDPRService.DownloadDPRTestPhoto(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = testData

            });
        }

        #endregion
    }
}
