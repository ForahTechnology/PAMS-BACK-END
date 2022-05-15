using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    /// <summary>
    /// This handles all client related activities in the application.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// This method returns all the clients this application.
        /// </summary>
        /// <returns></returns>
        PagedResponse<ClientResponse> GetAllClient(int pageSize, int pageNumber);

        /// <summary>
        /// This method returns a client whose id is passed.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Client GetClient(Guid Id);
       
        /// <summary>
        /// This method returns a client whose email is passed.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        Client GetClient(string Email);


        /// <summary>
        /// This method creates client in this application.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<Client> CreateClient(CreateClientDTO client);

        /// <summary>
        /// This method updates a client's details in the application.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<Client> UpdateClient(UpdateClientDTO client);

        /// <summary>
        /// This method deletes a client record from the database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteClient(Guid Id);

        /// <summary>
        /// This method adds a new sample template to the list of client's sample templates.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ClientSampleResponse> AddClientSampleTemplate(CreateClientSample model);
        /// <summary>
        /// This method removes a template from client list of sample templates.
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        Task<bool> RemoveClientSampleTemplate(List<RemoveClientSample> templates);
        /// <summary>
        /// This method deletes client sample template whose id is passed.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        Task<bool> RemoveClientSampleTemplate(Guid templateId);
        List<Client> GetAllClientMobile();
        int Count();
    }
}
