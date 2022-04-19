using Booking.Application.Models;
using Booking.Application.Models.Hotels;
using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Queries.HotelQuery
{
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, List<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        public GetHotelQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<List<HotelDto>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _hotelRepository.GetQuerry().Include(i => i.HotelImages).ToListAsync();
            return hotels.Select(i => new HotelDto()
            {
                HotelName = i.HotelName,
                Address = i.Address,
                MinPrice = i.MinPrice,
                MaxPrice = i.MaxPrice,
                Longtitude = i.Longtitude,
                Latitude = i.Latitude,
                Images = i.HotelImages?.Select(o => new HotelImageDto()
                {
                    Id = o.Id,
                    Url = o.ImageUrl
                }).ToList()
            }).ToList();
        }
    }
}
