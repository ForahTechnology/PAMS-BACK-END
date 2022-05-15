using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Services;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;

namespace PAMS.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all client related activities.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ClientController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IClientService clientService;
        private readonly ISampleService sampleService;

        /// <summary>
        /// Constructor for dependency injections
        /// </summary>
        /// <param name="mapper"></param> 
        /// <param name="env"></param>
        /// <param name="clientService"></param>
        /// <param name="sampleService"></param>
        public ClientController(
            IMapper mapper, 
            IWebHostEnvironment env,
            IClientService clientService,
            ISampleService sampleService
            )
        {
            this.mapper = mapper;
            this.env = env;
            this.clientService = clientService;
            this.sampleService = sampleService;
        }

        /// <summary>
        /// This endpoint Gets all the  clients on this application.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetAllClient")] 
        public IActionResult ClientList(int pageSize=10, int pageNumber=1)
        {
                var clientList = clientService.GetAllClient(pageSize,pageNumber);
                return Ok(new ResponseViewModel
                {
                    Status = true,
                    Message = "Query ok",
                    ReturnObject = clientList

                }); ;
        }

        /// <summary>
        /// This endpoint Gets a single client by on this application.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetClient/{id}")] 
        public IActionResult GetClient([FromRoute]Guid id)
        {
                var client = clientService.GetClient(id);
                if (client == null )
                {
                    return NotFound(new ResponseViewModel
                    {
                        Status = false,
                        Message = "No such Client exists", 

                    }); 
                }
                return Ok(new ResponseViewModel
                {
                    Status = true,
                    Message = "Query ok",
                    ReturnObject = client

                });
        }


        /// <summary>
        /// This endpoint creates clients on this application.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost("CreateClient")] 
        public async Task<IActionResult> CreateClient([FromBody]CreateClientDTO client)
        {
            if(ModelState.IsValid)
            {
                try 
                {
                    var checkIfClientExists = clientService.GetClient(client.Email);
                    if(checkIfClientExists != null)
                    {
                        return BadRequest(new ResponseViewModel { 
                            Status = false,
                            Message = "Email already exist for another client"
                        });
                    }

                    var response = await clientService.CreateClient(client);
                    return Ok(new ResponseViewModel()
                    {
                        Status = true,
                        Message = "Client was successfully registered",
                        ReturnObject = response
                    });
                  }
                   catch (Exception ex)
                {
                    return BadRequest(new ResponseViewModel {
                        Message = ex.Message,
                        Status = false 
                    });
                }
            }
                return BadRequest(new ResponseViewModel {
                    Status = false, 
                    Message = "All fields are required!" 
                });
            }

        /// <summary>
        /// This endpoint Updates clients on this application.
        /// </summary>
        ///  <param name="client"></param>
        /// <returns></returns>
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody]UpdateClientDTO client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await clientService.UpdateClient(client);
                    if (response != null)
                    {
                        return Ok(new ResponseViewModel
                        {
                            Status = true,
                            Message = "Client Successfully updated",
                            ReturnObject = response
                        });
                    }

                    return NotFound(new ResponseViewModel
                    {
                        Status = false,
                        Message = "The requested client could not be found!"
                    });

                }

                return BadRequest(new ResponseViewModel
                {
                    Status = false,
                    Message = "Invalid inputs"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel
                {
                    Message = ex.Message,
                    Status = false
                });
            }

        }

        /// <summary>
        /// This endpoint Deletes clients on this application.
        /// </summary>
        ///  <param name="clientId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteClient/{clientId}")]
        public async Task<IActionResult> DeleteClient([FromRoute]Guid clientId)
        {
            try
            {
                if (clientId != Guid.Empty)
                {
                    var isDeleted = await clientService.DeleteClient(clientId);
                    if (isDeleted)
                    {
                        return Ok(new ResponseViewModel
                        {
                            Status = true,
                            Message = "Client Successfully Deleted",
                        });
                    }
                }

                return NotFound(new ResponseViewModel
                {
                    Status = false,
                    Message = "Client could not be found!"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel
                {
                    Message = ex.Message,
                    Status = false
                });
            }

        }

    }
}
