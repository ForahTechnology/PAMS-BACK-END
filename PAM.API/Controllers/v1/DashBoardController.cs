using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Interfaces.Services;
using PAMS.DTOs.Response;
using System.Net;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DashBoardController : BaseController
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(
            IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> ViewDashBoard()
        {
            var reports = await _dashBoardService.GetDashboard();
            return Ok(new ResponseViewModel { Status = true, Message = "Query ok!", ReturnObject = reports });
        }
    }
}
