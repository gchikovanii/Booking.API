using Booking.Application.Abstraction;
using Booking.Application.Models;
using Booking.Application.Models.Hotels;
using Booking.Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Implementation
{
   
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        //Get All Hotels
        //public async Task<List<HotelDto>> GetAllHotels()
        //{
        //    var hotels = await _hotelRepository.GetQuerry().Include(i => i.HotelImages).ToListAsync();
        //    return hotels.Select(i => new HotelDto()
        //    {
        //        HotelName = i.HotelName,
        //        Address = i.Address,
        //        MinPrice = i.MinPrice,
        //        MaxPrice = i.MaxPrice,
        //        Longtitude = i.Longtitude,
        //        Latitude = i.Latitude,
        //        Images = i.HotelImages?.Select(o => new HotelImageDto()
        //        {
        //            Id = o.Id,
        //            Url = o.ImageUrl
        //        }).ToList()
        //    }).ToList();




        //}

        //Get Hotels by Id
        public async Task<HotelDto> GetHotelsById(int id)
        {
            var hotel = await _hotelRepository.GetQuerry(i => i.Id == id).SingleOrDefaultAsync();
            return new HotelDto()
            {
                HotelName = hotel?.HotelName,
                Address = hotel?.Address,
                MaxPrice = hotel?.MaxPrice,
                MinPrice = hotel?.MinPrice,
                Longtitude = hotel?.Longtitude,
                Latitude = hotel?.Latitude
            };
        }
        
        //Paging filter page by page size (how many items we want to display on page)
        public async Task<List<HotelDto>> GetHotelsPage(int index, int pageSize)
        {
            var hotels = _hotelRepository.GetQuerry().Skip(index * pageSize).Take(pageSize);
            return await hotels.Select(i => new HotelDto()
            {
                HotelName = i.HotelName,
                Address = i.Address,
                MaxPrice = i.MaxPrice,
                MinPrice = i.MinPrice,
                Latitude = i.Latitude,  
                Longtitude = i.Longtitude
            }).ToListAsync();
        }
    }
}
