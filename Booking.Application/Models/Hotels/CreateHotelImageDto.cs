using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.Hotels
{
    public class CreateHotelImageDto
    {
        public IFormFile? File { get; set; }
        public int HotelId { get; set; }
    }
}
