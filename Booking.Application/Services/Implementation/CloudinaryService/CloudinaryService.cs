using Booking.Application.ConfigurationOptions;
using Booking.Application.Services.Abstraction.ICloudinaryService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Implementation.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        public readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySetting> cloudinarySetting)
        {
            var setting = cloudinarySetting.Value;
            var account = new Account() { Cloud = setting.Cloud, ApiKey = setting.ApiKey, ApiSecret = setting.ApiSecret };
            _cloudinary = new Cloudinary(account);
        }
        //Upload images 
        public async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            var result = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var strem = file.OpenReadStream();
                var uploadParams = new ImageUploadParams() { File = new FileDescription(file.FileName, strem)};
                result = await _cloudinary.UploadAsync(uploadParams);

            }
            return result;
        }
        //Delete images from cloudinary
        public async Task<DeletionResult> DeleteImage(string publicID)
        {
            var result = await _cloudinary.DestroyAsync(new DeletionParams(publicID));
            return result;
        }
    }
}
