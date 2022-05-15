using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface ISettingsService
    {
        Task<Setting> UpdateSettings(Settings setting);
        Task<Setting> GetSettings();
        Task<Setting> UpdateSettings(string Lab_Dir);
    }
}
