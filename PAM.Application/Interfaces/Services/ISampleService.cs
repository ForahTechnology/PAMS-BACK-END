using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    /// <summary>
    /// This interface handles all sample related jobs in this application.
    /// </summary>
    public interface ISampleService
    {
        /// <summary>
        /// This method creates sampling taken by a field analyst for any client in this application.
        /// </summary>
        /// <param name="sampling"></param>
        /// <returns></returns>
        Task<Sampling> CreateSampling(SamplingDTO sampling);
        /// <summary>
        /// This method returns a sampling object whose Id is passed.
        /// </summary>
        /// <param name="samplingId"></param>
        /// <returns></returns>
        SamplingResponse GetSampling(Guid samplingId);
        /// <summary>
        /// This method returns a collection of sampling objects that belongs to a particular client whose id is passed.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<IEnumerable<SamplingResponse>> GetAllSamplings(Guid clientId);
        /// <summary>
        /// This method returns all the samplings that are available in the application.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SamplingResponse>> GetAllSamplings();
        Task<ClientSampleResponse> GetClientSampleTemplates(Guid ClientId);
        Task<bool> DeleteClientSamplings(Guid clientId);
        Task<AnalysisDaysCount> SampleTestCountPastSevenDays();
    }
}
