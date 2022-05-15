using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using PAMS.DTOs.Request.PhysicoTemplates;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AnalysisController : BaseController
    {
        private readonly IReportService reportService;
        private readonly IMapper mapper;
        private readonly IAnalysisService analysisService;

        public AnalysisController(
            IReportService reportService,
            IMapper mapper,
            IAnalysisService analysisService
            )
        {
            this.reportService = reportService;
            this.mapper = mapper;
            this.analysisService = analysisService;
        }


        /// <summary>
        /// Get physico chemical analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("physicochemicalanalysis/{id}")]
        public async Task<IActionResult> GetPhysico([FromRoute] Guid id)
        {
            if (id != Guid.Empty)
            {
                var analysis = await reportService.GetPhysico(id);
                if (analysis != null)
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = analysis });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }
        
        /// <summary>
        /// update physico chemical analysis
        /// </summary>
        /// <param name="physico"></param>
        /// <returns></returns>
        [HttpPut("physicochemicalanalysis")]
        public async Task<IActionResult> UpdatePhysico(PhysicoChemicalAnalysis physico)
        {
            if (physico != null)
            {
                var response = await reportService.UpdatePhysico(physico);
                if (response != null)
                {
                    return Ok(new ResponseViewModel { Message = "Updated successfully!", Status = true, ReturnObject = response });
                }
                return NotFound(new ResponseViewModel { Message = "Analysis for update does not exist.", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!" });
        }

        /// <summary>
        /// Delete physico chemical analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("physicochemicalanalysis/{id}")]
        public async Task<IActionResult> DeletePhysico([FromRoute] Guid id)
        {

            if (id != Guid.Empty)
            {
                var response = await reportService.DeletePhysico(id);
                if (response)
                {
                    return Ok(new ResponseViewModel { Message = "Record deleted!", Status = true });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }


        /// <summary>
        /// Get micro biological analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("microbiologicalanalysis/{id}")]
        public async Task<IActionResult> GetMicro([FromRoute] Guid id)
        {

            if (id != Guid.Empty)
            {
                var response = await reportService.GetMicro(id);
                if (response != null)
                {
                    return Ok(new ResponseViewModel { Message = "Query ok!", Status = true, ReturnObject = response });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }

        /// <summary>
        /// update micro biological analysis
        /// </summary>
        /// <param name="micro"></param>
        /// <returns></returns>
        [HttpPut("microbiologicalanalysis")]
        public async Task<IActionResult> UpdateMicro(MicroBiologicalAnalysis micro)
        {
            if (micro != null)
            {
                var response = await reportService.UpdateMicroBio(micro);
                if (response != null)
                {
                    return Ok(new ResponseViewModel { Message = "Updated successfully!", Status = true, ReturnObject = response });
                }
                return NotFound(new ResponseViewModel { Message = "Analysis for update does not exist.", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!" });
        }

        /// <summary>
        /// Delete  micro biological analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("microbiologicalanalysis/{id}")]
        public async Task<IActionResult> DeleteMicro([FromRoute] Guid id)
        {

            if (id != Guid.Empty)
            {
                var response = await reportService.DeleteMicroBio(id);
                if (response)
                {
                    return Ok(new ResponseViewModel { Message = "Record deleted!", Status = true });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!" });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid id supplied!" });
        }

        /// <summary>
        /// Upload physico-chemical analysis template
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("uploadtemplate")]
        [ProducesResponseType(200, Type= typeof(IEnumerable<ParameterUploadResponse>))]
        public IActionResult GenerateTemplate(IFormFile file)
        {
            if (file == null) return BadRequest(new ResponseViewModel { Message = "Invalid file!" });
            var fileName = file.FileName;
            if (!fileName.ToLower().Contains(".csv"))
                return BadRequest(new ResponseViewModel { Message = "Only csv files are allowed!" });
            var records = new List<ParameterUploadResponse>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                IgnoreBlankLines = false,
                TrimOptions = TrimOptions.Trim
            };
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<ParameterUploadResponse>().ToList();
            }
            records.RemoveAt(0);
            records.RemoveAt(records.Count - 1);
            records.RemoveAt(records.Count - 1);
            var response = new ResponseViewModel
            {
                Message = $"{records.Count} records found!",
                ReturnObject = records,
                Status = true
            };
            return Ok(response);
        }

        /// <summary>
        /// Create Fmv
        /// </summary>
        /// <param name="fmenv"></param>
        /// <returns></returns>
        [HttpPost("create-fmenv")]
        public async Task<IActionResult> CreateFMV([FromBody] CreateFmv fmenv)
        {
            if (fmenv.Parameters == null || string.IsNullOrWhiteSpace(fmenv.SectorName) || !fmenv.Parameters.Any()) throw new NullReferenceException("All fields are required!");
            var parameters = new List<FMEnvParameterTemplate>();
            foreach (var param in fmenv.Parameters)
            {
                parameters.Add(new FMEnvParameterTemplate {
                 Limit = param.Limit,
                  Unit = param.Unit,
                   Parameter = param.Parameter
                });
            }
            var newFmv = new FMEnv {
             CreateOn = DateTime.UtcNow,
              SectorName =  fmenv.SectorName,
               Parameters =parameters
            };
            await analysisService.CreateTemplateAsync(newFmv);
            return Ok(newFmv);
        }

        /// <summary>
        /// Create FMENv microbial template
        /// </summary>
        /// <param name="microbial"></param>
        /// <returns></returns>
        [HttpPost("create-fmenv-microbial")]
        public async Task<IActionResult> CreateFmEnvMibrobial(CreateMicrobial microbial)
        {
            if (microbial.Parameters == null || !microbial.Parameters.Any() || microbial.PhysicoId == Guid.Empty) throw new NullReferenceException("All fields are required!");
            var response = await analysisService.CreateFmenvMicrobialTemplateAsync(microbial);
            return Ok(response);
        }
        /// <summary>
        /// Get all FMEnv templates
        /// </summary>
        /// <returns></returns>
        [HttpGet("fmenv-templates")]
        public async Task<IActionResult> GetAllFMEnvTemplates()
        {
            return Ok(new ResponseViewModel { Message = "Available FMEnv templates", ReturnObject = await analysisService.GetAllFMEnvTemplates(), Status = true });
        }

        /// <summary>
        /// Create NESREA template
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        [HttpPost("create-nesrea")]
        public async Task<IActionResult> CreateNESREA(CreateTemp temp)
        {
            if (temp.Parameters == null || !temp.Parameters.Any()) throw new NullReferenceException("All fields are required!");
            var parameters = new List<NESREAParameterTemplate>();
            foreach (var param in temp.Parameters)
            {
                parameters.Add(new NESREAParameterTemplate
                {
                    Limit = param.Limit,
                    Unit = param.Unit,
                    Parameter = param.Parameter
                });
            }
            var nesrea = new NESREA
            {
                CreateOn = DateTime.UtcNow,
                NESREAParameterTemplates = parameters
            };
            await analysisService.CreateTemplateAsync(nesrea);
            return Ok(nesrea);
        }

        /// <summary>
        /// Create nesrea microbial template
        /// </summary>
        /// <param name="microbial"></param>
        /// <returns></returns>
        [HttpPost("create-nesrea-microbial")]
        public async Task<IActionResult> CreateNESREAMibrobial(CreateMicrobial microbial)
        {
            if (microbial.Parameters == null || !microbial.Parameters.Any() || microbial.PhysicoId == Guid.Empty) throw new NullReferenceException("All fields are required!");
            var response = await analysisService.CreateNesreaMicrobialTemplateAsync(microbial);
            return Ok(response);
        }

        /// <summary>
        /// Get all FMEnv templates
        /// </summary>
        /// <returns></returns>
        [HttpGet("nesrea-templates")]
        public async Task<IActionResult> GetAllNesreaTemplates()
        {
            return Ok(new ResponseViewModel { Message = "Available NESREA templates", ReturnObject = await analysisService.GetAllNESREATemplates(), Status = true });
        }


        /// <summary>
        /// Create DPR template
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        [HttpPost("create-dpr")]
        public async Task<IActionResult> CreateDPR(CreateTemp temp)
        {
            if (temp.Parameters == null || !temp.Parameters.Any()) throw new NullReferenceException("All fields are required!");
            var parameters = new List<DPRParameterTemplate>();
            foreach (var param in temp.Parameters)
            {
                parameters.Add(new DPRParameterTemplate
                {
                    Limit = param.Limit,
                    Unit = param.Unit,
                    Parameter = param.Parameter
                });
            }
            var dpr = new DPR
            {
                CreateOn = DateTime.UtcNow,
                DPRParameterTemplates = parameters
            };
            await analysisService.CreateTemplateAsync(dpr);
            return Ok(dpr);
        }
        /// <summary>
        /// Create dpr microbial template
        /// </summary>
        /// <param name="microbial"></param>
        /// <returns></returns>
        [HttpPost("create-dpr-microbial")]
        public async Task<IActionResult> CreateDPRMibrobial(CreateMicrobial microbial)
        {
            if (microbial.Parameters == null || !microbial.Parameters.Any() || microbial.PhysicoId == Guid.Empty) throw new NullReferenceException("All fields are required!");
            var response = await analysisService.CreateDprMicrobialTemplateAsync(microbial);
            return Ok(response);
        }
        /// <summary>
        /// Get all FMEnv templates
        /// </summary>
        /// <returns></returns>
        [HttpGet("dpr-templates")]
        public async Task<IActionResult> GetAllDPRTemplates()
        {
            return Ok(new ResponseViewModel { Message = "Available DPR templates", ReturnObject = await analysisService.GetAllDPRTemplates(), Status = true });
        }
        
        /// <summary>
        /// Create air quality template
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("air-quality-templates")]
        public async Task<IActionResult> CreateAirQualityTemplate([FromBody] AirQualityTemplateRequest request)
        {
            var response = await analysisService.CreateAirQualityTemplate(request);
            return Ok(new ResponseViewModel { Message = "Template created", ReturnObject = response });
        }
        /// <summary>
        /// Update air quality template
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("air-quality-templates")]
        public async Task<IActionResult> UpdateAirQualityTemplate([FromBody] AirQualityTemplate request)
        {
            var response = await analysisService.UpdateAirQualityTemplate(request);
            return Ok(new ResponseViewModel { Message = "Template updated", ReturnObject = response });
        }

        /// <summary>
        /// Get air quality template by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("air-quality-templates/{Id}")]
        public IActionResult GetAirQualityTemplateById([FromRoute] Guid Id)
        {
            var response = analysisService.GetAirQualityTemplateById(Id);
            return Ok(new ResponseViewModel { Message = "Record found", ReturnObject = response });
        }
        /// <summary>
        /// Delete air quality template
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("air-quality-templates/{Id}")]
        public IActionResult DeleteAirQualityTemplate([FromRoute] Guid Id)
        {
            var response = analysisService.DeleteAirQualityTemplate(Id);
            return Ok(new ResponseViewModel { Message = "Record found", ReturnObject = response });
        }
        /// <summary>
        /// Get all air quality template
        /// </summary>
        /// <returns></returns>
        [HttpGet("air-quality-templates")]
        public async Task<IActionResult> GetAllAirQualityTemplate()
        {
            var response = await analysisService.GetAllAirQualityTemplate();
            return Ok(new ResponseViewModel { Message = $"{response.Count} record(s) found", ReturnObject = response });
        }

        /// <summary>
        /// Add air quality parameter
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("air-quality-parameters")]
        public async Task<IActionResult> AddAirQualityParameter([FromBody] AddAirQualityParameterRequest request)
        {
            var response = await analysisService.AddAirQualityParameter(request);
            return Ok(new ResponseViewModel { Message = "Parameter added", ReturnObject = response });
        }
        
        /// <summary>
        /// Update air quality parameter
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("air-quality-parameters")]
        public async Task<IActionResult> UpdateAirQualityParameter([FromBody] AirQualityParameter request)
        {
            var response = await analysisService.UpdateAirQualityParameter(request);
            return Ok(new ResponseViewModel { Message = "Parameter updates", ReturnObject = response });
        }

        /// <summary>
        /// Get air quality parameter by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("air-quality-parameters/{Id}")]
        public async Task<IActionResult> GetAirQualityParameter([FromRoute] Guid Id)
        {
            var response = await analysisService.GetAirQualityParameter(Id);
            return Ok(new ResponseViewModel { Message = "Record found", ReturnObject = response });
        }
        
        /// <summary>
        /// Get all air quality parameter for a template
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        [HttpGet("air-quality-parameters/{TemplateId}/all")]
        public async Task<IActionResult> GetAllAirQualityParameterForTemplate([FromRoute] Guid TemplateId)
        {
            var response = await analysisService.GetAllAirQualityParameter(TemplateId);
            return Ok(new ResponseViewModel { Message = $"{response.Count} record(s) found", ReturnObject = response });
        }
        
        /// <summary>
        /// Delete air quality parameter
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("air-quality-parameters/{Id}")]
        public async Task<IActionResult> DeleteAirQualityParameter([FromRoute] Guid Id)
        {
            var response = await analysisService.DeleteAirQualityParameter(Id);
            return Ok(new ResponseViewModel { Message = "Parameter added", ReturnObject = response });
        }

        /// <summary>
        /// Create air quality sample
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-air-quality-sample")]
        public async Task<IActionResult> CreateAirQualitySample([FromBody] AirQualitySampleRequest request)
        {
            var response = await analysisService.CreateAirQualitySample(request);
            return Ok(new ResponseViewModel { ReturnObject = response, Message = "Sample created", Status = true });
        }

        /// <summary>
        /// Delete air quality sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-air-quality-sample")]
        public async Task<IActionResult> DeleteAirQualitySample([FromQuery] Guid id)
        {
            var response = await analysisService.DeleteAirQualitySample(id);
            return Ok(new ResponseViewModel { ReturnObject = response, Message = "Sample delete", Status = true });
        }

        /// <summary>
        /// Get air quality sample by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-air-quality-sample")]
        public async Task<IActionResult> GetAirQualitySampleById([FromQuery] Guid id)
        {
            var response = await analysisService.GetAirQualitySampleByBy(id);
            return Ok(new ResponseViewModel { ReturnObject = response, Message = "Record found", Status = true });
        }
        
        /// <summary>
        /// Get all air quality sample
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-air-quality-samples")]
        public async Task<IActionResult> GetAllAirQualitySamples()
        {
            var response = await analysisService.GetAllAirQualitySamples();
            return Ok(new ResponseViewModel { ReturnObject = response, Message = $"{response.Count()} Record(s) found", Status = true });
        }

    }
}
