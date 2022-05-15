using PAMS.Domain.Entities;
using PAMS.Domain.Enums;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IReportService
    {
        /// <summary>
        /// This creates report for a particular sampling
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        Task<ReportResponse> CreateReport(CreateReport report);
        /// <summary>
        /// This returns the report whose id is passed.
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task<ReportResponse> GetReport(Guid reportId);
        /// <summary>
        /// This returns all the available reports.
        /// </summary>
        /// <returns></returns>
        Task<List<ReportResponse>> GetReports();
        /// <summary>
        /// This returns all the reports that belongs to a client whose id is passed.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<List<ReportResponse>> GetReports(Guid clientId);
        /// <summary>
        /// This returns the report that belongs to a sample whose id is passed.
        /// </summary>
        /// <param name="sampleId"></param>
        /// <returns></returns>
        Task<ReportResponse> GetReportBySampleId(Guid sampleId);
        /// <summary>
        /// Updates a report object
        /// </summary>
        /// <returns></returns>
        Task<ReportResponse> UpdateReport(UpdateReport update);
        /// <summary>
        /// This deletes the report whose id is passed.
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task<bool> DeleteReport(Guid reportId);
        /// <summary>
        /// Update physico chemical analysis
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        Task<PhysicoChemicalAnalysis> UpdatePhysico(PhysicoChemicalAnalysis analysis);
        /// <summary>
        /// Deletes physico chemical analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePhysico(Guid id);
        /// <summary>
        /// Updates micro biological analysis
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        Task<MicroBiologicalAnalysis> UpdateMicroBio(MicroBiologicalAnalysis analysis);
        /// <summary>
        /// Deletes micro biological analysis
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteMicroBio(Guid id);
        /// <summary>
        /// Get physico analysis by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PhysicoChemicalAnalysis> GetPhysico(Guid id);
        /// <summary>
        /// Get micro analysis by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MicroBiologicalAnalysis> GetMicro(Guid id);
        //Generate the report file string for conversion into pdf.
        Task<string> GetReportFileString(ReportResponse reportResponse, string path);
    }
}
