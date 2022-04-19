using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.HotelCommands
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, bool>
    {
        private readonly IHotelRepository _hotelRepository;
        public UpdateHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<bool> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetQuerry(i => i.Id == request.Id).SingleOrDefaultAsync();
            if (hotel != null)
            {
                if (request.HotelName != null)
                    hotel.HotelName = request.HotelName;
                if (request.Address != null)
                    hotel.Address = request.Address;
                if (request.Longtitude != null)
                    hotel.Longtitude = request.Longtitude;
                if (request.Latitude != null)
                    hotel.Latitude = request.Latitude;
                if (request.MinPrice != 0)
                    hotel.MinPrice = request.MinPrice;
                if (request.MaxPrice != 0)
                    hotel.MaxPrice = request.MaxPrice;
                return await _hotelRepository.SaveChangesAsync();
            }
            else
                return false;
        }
    }
}
