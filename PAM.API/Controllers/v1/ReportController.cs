using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Helpers;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReportController : BaseController
    {
        private readonly IReportService reportService;
        private readonly IWebHostEnvironment env;
        private readonly IMajorUtility majorUtility;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportService"></param>
        /// <param name="env"></param>
        /// <param name="majorUtility"></param>
        public ReportController(
            IReportService reportService,
            IWebHostEnvironment env,
            IMajorUtility majorUtility
            )
        {
            this.reportService = reportService;
            this.env = env;
            this.majorUtility = majorUtility;
        }
        /// <summary>
        /// Create report for sampling
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody]CreateReport report)
        {
            if (report.ClientID != Guid.Empty && report.SamplingID != Guid.Empty && report.Lab_Analyst !=null)
            {
                var response = await reportService.CreateReport(report);
                if (response == null)
                    return NotFound(new ResponseViewModel { Message = "Client could not be found!" });
                return Ok(new ResponseViewModel {Status=true, Message="Report created!",ReturnObject = response });
            }
            return BadRequest(new ResponseViewModel { Message="All fields are required!"});
        }

        /// <summary>
        /// Returns the report whose id is passed
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [HttpGet("getreport/{reportid}")]
        public async Task<IActionResult> GetReport([FromRoute] Guid reportid)
        {
            if (reportid != Guid.Empty)
            {
                var report = await reportService.GetReport(reportid);
                if (report != null)
                {
                    return Ok(new ResponseViewModel { Status = true, Message = "Query ok!", ReturnObject = report });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!", Status = false });

            }

            return BadRequest(new ResponseViewModel { Message = "Invalid client id supplied!", Status = false });
        }

        /// <summary>
        /// Get downloadable version of report in pdf.
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [HttpGet("downloadreport/{reportid}")]
        public async Task<IActionResult> DownloadReport([FromRoute] Guid reportid)
        {
            if (reportid != Guid.Empty)
            {

                var report = await reportService.GetReport(reportid);
                if (report != null)
                {
                    var fileName = $"Report_{report.ClientName}_{DateTime.Now.ToString()}";
                    fileName = fileName.Replace(@"\", "_");
                    fileName = fileName.Replace(" ", "_");
                    fileName = fileName.Replace("/", "_");
                    fileName = fileName.Replace(":", "_");
                    var response = await reportService.GetReportFileString(report, Path.Combine(env.WebRootPath, "EmailTemplates"));
                    //Generating PDF version of invoice
                    var file = majorUtility.GeneratePDF(response, "Report");
                    majorUtility.Logger("Report downloaded! : DownloadReport()");
                    return File(file, "application/octet-stream", $"{fileName}.pdf");
                }
                return NotFound(new ResponseViewModel { Message = "No record found!", Status = false });
            }

            return BadRequest(new ResponseViewModel { Message = "Invalid report id supplied!", Status = false });
        }


        /// <summary>
        /// Returns all the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet("allreports")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await reportService.GetReports();
            return Ok(new ResponseViewModel { Status = true, Message = "Query ok!", ReturnObject = reports });
        }

        /// <summary>
        /// Returns all reports that belongs to a client.
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        [HttpGet("clientreports/{clientid}")]
        public async Task<IActionResult> GetClientReports([FromRoute] Guid clientid)
        {
            if (clientid != Guid.Empty)
            {
                var reports = await reportService.GetReports(clientid); 
                if (reports.Any())
                {
                    return Ok(new ResponseViewModel { Status = true, Message = "Query ok!", ReturnObject = reports }); 
                }
                return NotFound(new ResponseViewModel { Message = "Client doesn't have any report.", Status = true });
            }

            return BadRequest(new ResponseViewModel { Message = "Invalid client id supplied!", Status = false });
        }

        /// <summary>
        /// Returns the report whose sample id is passed
        /// </summary>
        /// <param name="sampleId"></param>
        /// <returns></returns>
        [HttpGet("getreportBySampleId/{sampleId}")]
        public async Task<IActionResult> GetReportBySampleId([FromRoute] Guid sampleId)
        {
            if (sampleId != Guid.Empty)
            {
                var report = await reportService.GetReportBySampleId(sampleId);
                if (report != null)
                {
                    return Ok(new ResponseViewModel { Status = true, Message = "Query ok!", ReturnObject = report });
                }
                return NotFound(new ResponseViewModel { Message = "No record found!", Status = false });

            }

            return BadRequest(new ResponseViewModel { Message = "Invalid sample id supplied!", Status = false });
        }

        /// <summary>
        /// Update an existing report.
        /// </summary>
        /// <returns></returns>
        [HttpPut("updatereport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReport update)
        {
            if (update.ID != Guid.Empty && update.SamplingID != Guid.Empty)
            {
                var response = await reportService.UpdateReport(update);
                if (response != null)
                {
                    return Ok(new ResponseViewModel { Message = "Updated successfully!", Status = true, ReturnObject = response });
                }
                return NotFound(new ResponseViewModel { Message = "Report for update does not exist.", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "All fields are required!", Status = false });
        }

        /// <summary>
        /// Deletes the report whose id is passed
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        [HttpDelete("deletereport/{reportid}")]
        public async Task<IActionResult> DeleteReport([FromRoute] Guid reportid)
        {
            if (reportid != Guid.Empty)
            {
                var report = await reportService.DeleteReport(reportid);
                if (report)
                {
                    return Ok(new ResponseViewModel { Status = true, Message = "Report deleted!" }); 
                }
                return NotFound(new ResponseViewModel { Message = "Report could not be found!", Status = false });
            }
            return BadRequest(new ResponseViewModel { Message = "Invalid report id supplied!", Status = false });
        }

    }
}