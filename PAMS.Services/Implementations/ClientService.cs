using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IStoreManager<Client> clientStoreManager;
        private readonly ISampleService sampleService;
        private readonly IStoreManager<ClientSample> clientSampleStoreManager;
        private readonly IStoreManager<Invoice> invoiceStoreManager;
        private readonly IStoreManager<ContactPerson> contactPersonStoreManager;
        private readonly IAnalysisService analysisService;

        public ClientService(
            IStoreManager<Client> ClientStoreManager,
            ISampleService sampleService,
            IStoreManager<ClientSample> clientSampleStoreManager,
            IStoreManager<Invoice> InvoiceStoreManager,
            IStoreManager<ContactPerson>  contactPersonStoreManager,
            IAnalysisService analysisService
            )
        {
            this.clientStoreManager = ClientStoreManager;
            this.sampleService = sampleService;
            this.clientSampleStoreManager = clientSampleStoreManager;
            this.invoiceStoreManager = InvoiceStoreManager;
            this.contactPersonStoreManager = contactPersonStoreManager;
            this.analysisService = analysisService;
        }

        public async Task<Client> CreateClient(CreateClientDTO client)
        {
            //Creating the client object
            var newClient = new Client
            {
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                RegisteredDate = DateTime.Now,
                ContactPerson = client.ContactPerson,
                PhoneNumber = client.PhoneNumber,
            };
            var templates = new List<ClientSample>();
            await clientStoreManager.DataStore.Add(newClient);
            //Save predefined samples for client
            if (client.SampleTemplates.Any())
            {
                foreach (var temp in client.SampleTemplates)
                {
                    switch (temp.Type)
                    {
                        case Domain.Enums.PhysicoChemicalAnalysisType.NESREA:
                            var nesreaRes = await analysisService.GetNesreaTemplateById(temp.TemplateId);
                            if (nesreaRes != null)
                            {
                                templates.Add(new ClientSample
                                {
                                    ClientID = newClient.ID,
                                    SampleTemplateID = temp.TemplateId,
                                    Type = temp.Type
                                });

                            }
                            break;


                        case Domain.Enums.PhysicoChemicalAnalysisType.FMEnv:

                            var fmenvRes = await analysisService.GetFMEnvTemplateById(temp.TemplateId);
                            if (fmenvRes != null)
                            {
                                templates.Add(new ClientSample
                                {
                                    ClientID = newClient.ID,
                                    SampleTemplateID = temp.TemplateId,
                                    Type = temp.Type
                                });

                            }

                            break;


                        case Domain.Enums.PhysicoChemicalAnalysisType.DPR:
                            var dprRes = await analysisService.GetDprTemplateById(temp.TemplateId);
                            if (dprRes != null)
                            {
                                templates.Add(new ClientSample
                                {
                                    ClientID = newClient.ID,
                                    SampleTemplateID = temp.TemplateId,
                                    Type = temp.Type
                                });
                            }

                            break;


                        default:
                            break;
                    }
                }
            }
            if (templates.Any())
            {
                await clientSampleStoreManager.DataStore.AddRange(templates);
            }
            await clientStoreManager.Save();
            return newClient;
        }

        public async Task<bool> DeleteClient(Guid Id)
        {
            await sampleService.DeleteClientSamplings(Id);
            var clientSampleTemplates = await clientSampleStoreManager.DataStore.GetAllQuery().Where(c => c.ClientID == Id).ToListAsync();
            if (clientSampleTemplates.Any())
            {
                foreach (var template in clientSampleTemplates)
                {
                    await clientStoreManager.DataStore.Delete(template.ID);
                } 
            }
            
            var invoices = await invoiceStoreManager.DataStore.GetAllQuery().Where(i => i.ClientID == Id).ToListAsync();
            if (invoices.Any())
            {
                foreach (var invoice in invoices)
                {
                    await invoiceStoreManager.DataStore.Delete(invoice.ID);
                } 
            }

            var response = await clientStoreManager.DataStore.Delete(Id);
            await clientSampleStoreManager.Save();
            
            return response;
        }

        public int Count()
        {
            var total = clientStoreManager.DataStore.GetAll().Result.Count();
            return total;
        }
        
        public PagedResponse<ClientResponse> GetAllClient(int pageSize, int pageNumber)
        {
            var total2 = clientStoreManager.DataStore.GetAll().Result.Count();
            var total = clientStoreManager.DataStore.GetAllQuery().Count();

            var response = clientStoreManager.DataStore.GetAllQuery()
                .Include("Samplings")
                .Include("Samplings.MicroBiologicalAnalyses")
                .Include("Samplings.PhysicoChemicalAnalyses")
                .Include("SamplePointLocations")
                .Include("Invoices").Select(c => new ClientResponse
                {
                    Address = c.Address,
                    Email = c.Email,
                    ID = c.ID,
                    Invoices = c.Invoices,
                    Name = c.Name,
                    RegisteredDate = c.RegisteredDate,
                    ContactPerson = c.ContactPerson,
                    Samplings = c.Samplings,
                    SamplePointLocations = c.SamplePointLocations,
                    Templates = clientSampleStoreManager.DataStore.GetAllQuery()
                    .Where(ct => ct.ClientID == c.ID)
                    .Select(s => s.Type.ToString())
                    .ToList()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PagedResponse<ClientResponse> { Data = response, PageNumber = pageNumber, PageSize = pageSize, Total = total };
        }
        
        public List<Client> GetAllClientMobile()
        {
            return clientStoreManager.DataStore.GetAllQuery().AsNoTracking().ToList();
        }

        public Client GetClient(Guid Id)
        {
            return clientStoreManager.DataStore.GetAllQuery()
                .Where(c => c.ID == Id)
                .Include("Samplings")
                .Include("Samplings.MicroBiologicalAnalyses")
                .Include("Samplings.PhysicoChemicalAnalyses")
                .Include("Invoices")
                .Include("SamplePointLocations")
                .FirstOrDefault();            
        }

        public Client GetClient(string Email)
        {
            return clientStoreManager.DataStore.GetAllQuery().FirstOrDefault(c => c.Email == Email);
        }

        public async Task<Client> UpdateClient(UpdateClientDTO client)
        {
            var clientToUpdate = GetClient(client.ID);
            if (clientToUpdate != null)
            {
                clientToUpdate.Name = client.Name ?? clientToUpdate.Name;
                clientToUpdate.ContactPerson = client.ContactPerson ?? clientToUpdate.ContactPerson;
                clientToUpdate.Address = client.Address ?? clientToUpdate.Address;
                clientToUpdate.Email = client.Email ?? clientToUpdate.Email;
                clientStoreManager.DataStore.Update(clientToUpdate);
                await clientStoreManager.Save();
                return clientToUpdate;
            }
            return null;
        }

        public async Task<ClientSampleResponse> AddClientSampleTemplate(CreateClientSample model)
        {
            var client = GetClient(model.ClientId);
            //Save predefined samples for client
            if (model.SampleTemplates.Any() && client != null)
            {
                    var templates = new List<ClientSample>();
                    //Save predefined samples for client
                    if (model.SampleTemplates.Any())
                    {
                        foreach (var temp in model.SampleTemplates)
                        {
                            switch (temp.Type)
                            {
                                case Domain.Enums.PhysicoChemicalAnalysisType.NESREA:
                                    var nesreaRes = await analysisService.GetNesreaTemplateById(temp.TemplateId);
                                    if (nesreaRes != null)
                                    {
                                        templates.Add(new ClientSample
                                        {
                                            ClientID = model.ClientId,
                                            SampleTemplateID = temp.TemplateId,
                                            Type = temp.Type
                                        });

                                    }
                                    break;


                                case Domain.Enums.PhysicoChemicalAnalysisType.FMEnv:

                                    var fmenvRes = await analysisService.GetFMEnvTemplateById(temp.TemplateId);
                                    if (fmenvRes != null)
                                    {
                                        templates.Add(new ClientSample
                                        {
                                            ClientID = model.ClientId,
                                            SampleTemplateID = temp.TemplateId,
                                            Type = temp.Type
                                        });

                                    }

                                    break;


                                case Domain.Enums.PhysicoChemicalAnalysisType.DPR:
                                    var dprRes = await analysisService.GetDprTemplateById(temp.TemplateId);
                                    if (dprRes != null)
                                    {
                                        templates.Add(new ClientSample
                                        {
                                            ClientID = model.ClientId,
                                            SampleTemplateID = temp.TemplateId,
                                            Type = temp.Type
                                        });
                                    }

                                    break;


                                default:
                                    break;
                            }
                        }
                    }
                    if (templates.Any())
                    {
                        await clientSampleStoreManager.DataStore.AddRange(templates);
                    }
                await clientSampleStoreManager.Save();
                return await sampleService.GetClientSampleTemplates(model.ClientId);
            }
            return default;
        }

        public async Task<bool> RemoveClientSampleTemplate(List<RemoveClientSample> templates)
        {
            if (templates.Any())
            {
                foreach (var temp in templates)
                {
                    var template = clientSampleStoreManager.DataStore.GetAllQuery().FirstOrDefault(t => t.ClientID == temp.ClientId && t.SampleTemplateID == temp.SampleTemplateId);
                    if (template != null)
                    {
                        await clientSampleStoreManager.DataStore.Delete(template.ID);
                        
                    }
                }
                await clientSampleStoreManager.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveClientSampleTemplate(Guid templateId)
        {
            var response = await clientSampleStoreManager.DataStore.Delete(templateId);
            return response;
        }

        public async Task<ContactPerson> CreateContactPerson(CreateContactPerson contactPerson)
        {
            var client = await clientStoreManager.DataStore.GetById(contactPerson.ClientId);
            if (client == null) throw new KeyNotFoundException($"Client with id '{contactPerson.ClientId}' not found");
            var person = new ContactPerson
            {
                ClientId = contactPerson.ClientId,
                Email = contactPerson.Email,
                Name = contactPerson.Name,
                PhoneNumber = contactPerson.PhoneNumber
            };
            await contactPersonStoreManager.DataStore.Add(person);
            await contactPersonStoreManager.Save();
            return person;
        }

        public async Task<ContactPerson> GetContactPerson(Guid Id)
        {
            var person = await contactPersonStoreManager.DataStore.GetById(Id);
            if (person == null) throw new KeyNotFoundException("Contact person not found");
            return person;
        }

        public async Task<IEnumerable<ContactPerson>> GetContactPersons()
        {
            var persons = await contactPersonStoreManager.DataStore.GetAll();
            return persons;
        }

        public async Task<bool> DeleteContactPerson(Guid Id)
        {
            var person = await contactPersonStoreManager.DataStore.GetById(Id);
            if (person == null) throw new KeyNotFoundException("Contact person not found");
            return true;
        }
    }
}
