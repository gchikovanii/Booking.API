using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.Hotels
{
    public class CreateHotelDto
    {
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string? Longtitude { get; set; }
        public string? Latitude { get; set; }
        public string? Image { get; set; }
    }
}
