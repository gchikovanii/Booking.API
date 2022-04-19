using Booking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Abstraction
{
    public interface IHotelService
    {
        Task<HotelDto> GetHotelsById(int id);
        Task<List<HotelDto>> GetHotelsPage(int index, int pageSize);
    }
}
