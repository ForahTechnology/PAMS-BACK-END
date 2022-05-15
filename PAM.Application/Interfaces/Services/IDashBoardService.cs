using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IDashBoardService
    {
        Task<DashboardVm> GetDashboard();
    }
}
