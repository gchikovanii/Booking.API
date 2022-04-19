using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Abstraction.ICloudinaryService
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImage(IFormFile file);
        Task<DeletionResult> DeleteImage(string publicId);
    }
}
