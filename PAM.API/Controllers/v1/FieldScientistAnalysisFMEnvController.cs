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
    public class FieldScientistAnalysisFMEnvController : BaseController
    {
        private readonly IFieldScientistAnalysisFMEnvService _fieldAnalysisService;

        public FieldScientistAnalysisFMEnvController(
            IFieldScientistAnalysisFMEnvService fieldAnalysisService)
        {
            _fieldAnalysisService = fieldAnalysisService;
        }

        #region Field FMENV Tests

        /// <summary>
        /// Create field fmenv test
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("field-add-new-fmenv-test")]
        public async Task<IActionResult> CreateFMENVTest(FMENVTemplatesVm model)
        {
            var response = await _fieldAnalysisService.CreateFMENVSampleTemplate(model);
            return Ok(response);
        }

        /// <summary>
        /// Get All tests under field fmenv 
        /// </summary>
        /// <returns></returns>
        [HttpGet("field-get-all-fmenv-tests")]
        public async Task<IActionResult> GetAllFMENVTests()
        {
            var response = await _fieldAnalysisService.GetAllFmenvTemplates();
            return Ok(response);
        }

        /// <summary>
        /// The endpoint updates the fmenv Test.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("field-update-fmenv-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> UpdateFMENVTest(UpdateFMENVTemplateVm model)
        {
            var response = await _fieldAnalysisService.UpdateFMenvTemplate(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint delete a fmenv Test.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpDelete("field-delete-fmenv-test")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteFMENVTest(Guid templateId)
        {
            var response = await _fieldAnalysisService.DeleteFMENVTemplate(templateId);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        #endregion

        #region FIELD FMENV

        /// <summary>
        /// This endpoint returns FMEnv Templates with all the Test templates that belongs to the Sample Location whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-fmenv-template")]
        [ProducesResponseType(200, Type = typeof(FieldScientistFMEnvTestsVM))]
        public async Task<IActionResult> GetFMEnvTemplates([FromQuery] FMEnvsCreateRequestVM model)
        {
            var response = await _fieldAnalysisService.GetFMEnvTemplates(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });

        }

        /// <summary>
        /// This endpoint returns FMEnv Test Template that belongs to FMEnv template whose id passed.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("get-fmenv-test-template")]
        [ProducesResponseType(200, Type = typeof(SampleFMEnvTestVM))]
        public async Task<IActionResult> GetFMEnvSpecificTestTemplatesByName([FromQuery] FMEnvTestName model)
        {
            var response = await _fieldAnalysisService.GetFMEnvSpecificTestTemplatesByName(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint update the value of each Test result of any test conducted under fmenv.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add-fmenv-test-Testresult-ForEachTest")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddOrUpdateFMEnvSpecificTestTestResultByName([FromQuery] FMEnvTestResult model)
        {
            var response = await _fieldAnalysisService.AddOrUpdateFMEnvSpecificTestTestResultByName(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// The endpoint for final submission of all result values under fmenv.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("submit-fmenv-test-Testresult")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> AddFMEnvTestResult([FromForm] AddFMEnvTestResults model)
        {
            var response = await _fieldAnalysisService.AddFMEnvTestResult(model);
            return Ok(new ResponseViewModel { Message = response != null ? "Query ok!" : "No record found!", Status = true, ReturnObject = response });
        }

        /// <summary>
        /// This endpoint Gets all the fmenv tests on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllFMEnvSubmittedTest")]
        public IActionResult GetAllFMEnvTest(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisService.GetAllFMEnvTest(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets all the fmenv tests on this application by Analyst Id.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllFMEnvSubmittedTestByAnalystId")]
        public IActionResult GetAllFMEnvTestByAnalystId(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            var clientList = _fieldAnalysisService.GetAllFMEnvTestByAnalystId(pageSize, pageNumber, keyword);
            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// This endpoint Gets FMVEnv test per Id for a location.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetFMVEnvSubmittedTestPerId")]
        public async Task<IActionResult> GetFMEnvTestById([FromQuery] FmEnvResultVM model)
        {
            var clientList = await _fieldAnalysisService.GetFMEnvTestById(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = clientList

            });
        }

        /// <summary>
        /// With this endpoint you can download fmenv test result
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-fmenv-test-result")]
        public async Task<IActionResult> DownloadDPRTestToExcel([FromQuery] FmEnvResultVM model)
        {
            var testData = await _fieldAnalysisService.DownloadFMEnvTestToExcel(model);

            return Ok(new ResponseViewModel
            {
                Status = true,
                Message = "Query ok",
                ReturnObject = testData

            }); ;
        }

        /// <summary>
        /// With this endpoint you can download photo submitted with the fmenv test
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("dowload-fmenv-test-photo")]
        public async Task<IActionResult> DownloadDPRPhoto([FromQuery] FmEnvResultVM model)
        {
            var testData = await _fieldAnalysisService.DownloadFMEnvTestPhoto(model);

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
