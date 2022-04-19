using Booking.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.HotelCommands
{
    public class CreateHotelCommand : IRequest<bool>
    {
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string? Longtitude { get; set; }
        public string? Latitude { get; set; }
        public List<string>? Images { get; set; }
    
        public List<IFormFile>? GetImageFiles()
        {
            try
            {
                var imageFiles = new List<IFormFile>();
                if (Images.Count == 0 || Images == null)
                    return default;
                foreach (var image in Images)
                {
                    imageFiles.Add(image.ConvertBase64ToImage());
                }
                return imageFiles;
            }
            catch (Exception)
            {
                return default;
            }

        }
    }

}
