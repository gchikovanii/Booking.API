using Booking.Application.Models.Hotels;
using Booking.Application.Services.Abstraction.ICloudinaryService;
using Booking.Application.Services.Abstraction.IHotelService;
using Booking.Domain.Entities.HotelAggregate;
using Booking.Infrastructure.Repository.Abstraction;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Implementation.HotelService
{
    public class HotelImageService : IHotelImageService
    {
        private readonly IHotelImageRepository _hotelImageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        public HotelImageService(IHotelImageRepository hotelImageRepository, ICloudinaryService cloudinaryService)
        {
            _hotelImageRepository = hotelImageRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> UploadImage(CreateHotelImageDto input)
        {
            var result = await _cloudinaryService.UploadImage(input.File);
            await _hotelImageRepository.Create(new HotelImage()
            {
                HotelId = input.HotelId,
                CloudinaryId = result.PublicId,
                ImageUrl = result.Url.AbsoluteUri
            });
            return await _hotelImageRepository.SaveChangesAsync();
        }

        //Upload more than one images
        public async Task<bool> UplaodImages(List<CreateHotelImageDto> input)
        {
            List<Task<ImageUploadResult>> imageUploadResults = new List<Task<ImageUploadResult>>();
            foreach (var item in input)
            {
                imageUploadResults.Add(_cloudinaryService.UploadImage(item.File));
            }
            var uploadImages = await Task.WhenAll(imageUploadResults);

            foreach (var image in uploadImages)
            {
                await _hotelImageRepository.Create(new HotelImage()
                {
                    HotelId = input.FirstOrDefault().HotelId,
                    CloudinaryId = image.PublicId,
                    ImageUrl = image.Url.AbsoluteUri
                });
            }
            return await _hotelImageRepository.SaveChangesAsync();
        }

    }
}
