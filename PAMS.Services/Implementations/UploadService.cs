using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Application.Interfaces.Services;
using PAMS.Domain.Entities;
using PAMS.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Services.Implementations
{
    public class UploadService : IUploadService
    {
        private readonly IStoreManager<ImageModel> _imageStoreManager;

        public UploadService(
            IStoreManager<ImageModel> imageStoreManager
            )
        {
            _imageStoreManager = imageStoreManager;
        }

        public async Task<string> DeleteFileFromDatabase(long id)
        {
            var existingImage = await _imageStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (existingImage is null) return $"Image with id {id} does not exist";

            await _imageStoreManager.DataStore.Delete(existingImage.Id);
            await _imageStoreManager.Save();

            return "Success";
        }

        public async Task<ImageVM> GetFileFromDatabase(long? id)
        {
            var file = await _imageStoreManager.DataStore.GetAllQuery()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (file == null)
                return null;

            var data = new ImageVM()
            {
                FileBase64 = Convert.ToBase64String(file.LogoBase64),
                FullName = file.Name + file.Extension
            };

            return data;
        }

        public async Task<string> UpdateImageInDatabase(IFormFile file, long? id)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var existingImage = await _imageStoreManager.DataStore.GetAllQuery()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (existingImage is null) return $"Image with id {id} does not exist";
           
            existingImage.FileType = file.ContentType;
            existingImage.TimeModified = DateTime.Now;
            existingImage.Extension = extension;
            existingImage.Name = fileName;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                existingImage.LogoBase64 = stream.ToArray();
            }

             _imageStoreManager.DataStore.Update(existingImage);
            await _imageStoreManager.Save();

            return "Success";
        }

        public async Task<long> UploadImageToDatabase(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var uploadImage = new ImageModel
            {               
                Extension = extension,
                FileType = file.ContentType,
                Name = fileName,
            };

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                uploadImage.LogoBase64 = stream.ToArray();
            }
            
            await _imageStoreManager.DataStore.Add(uploadImage);
            await _imageStoreManager.Save();

            return uploadImage.Id;
        }
    }
}
