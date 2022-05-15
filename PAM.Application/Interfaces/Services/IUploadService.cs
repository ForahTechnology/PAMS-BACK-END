using Microsoft.AspNetCore.Http;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Application.Interfaces.Services
{
    public interface IUploadService
    {
        Task<long> UploadImageToDatabase(IFormFile file);

        Task<string> UpdateImageInDatabase(IFormFile file, long? id);

        Task<ImageVM> GetFileFromDatabase(long? id);

        Task<string> DeleteFileFromDatabase(long id);
    }
}
