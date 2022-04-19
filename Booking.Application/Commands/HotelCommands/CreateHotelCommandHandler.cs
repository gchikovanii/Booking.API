using Booking.Application.Extensions;
using Booking.Application.Filters;
using Booking.Application.Models.Hotels;
using Booking.Application.Services.Abstraction.ICloudinaryService;
using Booking.Application.Services.Abstraction.IHotelService;
using Booking.Domain.Entities;
using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.HotelCommands
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, bool>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageService _hotelImageService;
        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IHotelImageService hotelImageService)
        {
            _hotelRepository = hotelRepository;
            _hotelImageService = hotelImageService;
        }
        public async Task<bool> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = new Hotel()
            {
                HotelName = request.HotelName,
                Address = request.Address,
                MinPrice = (decimal)request.MinPrice,
                MaxPrice = (decimal)request.MaxPrice,
                Latitude = request.Latitude,
                Longtitude = request.Longtitude
            };

            await _hotelRepository.Create(hotel);
            var result = await _hotelRepository.SaveChangesAsync();

            var imageResult = await _hotelImageService.UplaodImages(request.GetImageFiles()
                .Select(i => new CreateHotelImageDto()
                {
                    File = i,
                    HotelId = hotel.Id
                }).ToList());

            if (!imageResult)
                throw new ImageNotUploadedException("Image hasn't uploaded" + (result == true ? "Hotel added successfully" : "Hotel hasn't been added"));
            return result;
        }
    }
}
