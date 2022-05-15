using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAMS.Application.Interfaces.Services;
using PAMS.DTOs.Request;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
    [ApiVersion("1.0")]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService settings;

        public SettingsController(
            ISettingsService settings
            )
        {
            this.settings = settings;
        }

        /// <summary>
        /// Get tax value
        /// </summary>
        /// <returns></returns>
        [HttpGet("tax")]
        public IActionResult GetTax()
        {
            var tax = settings.GetSettings().Result == null ? "7.5" : settings.GetSettings().Result.VAT;
            return Ok(new ResponseViewModel { Status = true, Message = "Querry Ok!", ReturnObject = tax });
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        /// <returns></returns>
        [HttpGet("allsettings")]
        public async Task<IActionResult> GetAllSettings() => Ok(new ResponseViewModel { Status = true, Message = "Querry Ok!", ReturnObject = await settings.GetSettings()});

        /// <summary>
        /// Update tax value
        /// </summary>
        /// <returns></returns>
        [HttpPut("tax")]
        public async Task<IActionResult> UpdateTax(decimal tax) => Ok(new ResponseViewModel { Status = true, Message = "Updated!", ReturnObject = await settings.UpdateSettings(new Settings { VAT = tax })});

        
        /// <summary>
        /// Update Lab Director name
        /// </summary>
        /// <returns></returns>
        [HttpPut("labdirector")]
        public async Task<IActionResult> UpdateLabDirectore(string name) => Ok(new ResponseViewModel { Status = true, Message = "Updated!", ReturnObject = await settings.UpdateSettings(name)});


    }
}
