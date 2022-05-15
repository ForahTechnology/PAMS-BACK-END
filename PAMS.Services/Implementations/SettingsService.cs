using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly IStoreManager<Setting> settingsStoreManager;

        public SettingsService(
            IStoreManager<Setting> settingsStoreManager
            )
        {
            this.settingsStoreManager = settingsStoreManager;
        }
        public async Task<Setting> GetSettings()
        {
            return settingsStoreManager.DataStore.GetAllQuery().FirstOrDefault();
        }

        public async Task<Setting> UpdateSettings(Settings settings)
        {
            var setting = settingsStoreManager.DataStore.GetAllQuery().FirstOrDefault();
            if (setting != null)
            {
                setting.VAT = settings.VAT.ToString();
                settingsStoreManager.DataStore.Update(setting);

            }
            else
            {
                setting = new Setting
                {
                    VAT = settings.VAT.ToString()
                };
                await settingsStoreManager.DataStore.Add(setting);
            }
            await settingsStoreManager.Save();
            return setting;
        }
        public async Task<Setting> UpdateSettings(string Lab_Dir)
        {
            var setting = settingsStoreManager.DataStore.GetAllQuery().FirstOrDefault();
            if (setting != null)
            {
                setting.Lab_Director = Lab_Dir;
                settingsStoreManager.DataStore.Update(setting);
            }
            else
            {
                setting = new Setting
                {
                    Lab_Director = Lab_Dir
                };
                await settingsStoreManager.DataStore.Add(setting);
            }
            await settingsStoreManager.Save();
            return setting;
        }
    }
}
