using Booking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.HotelAggregate
{
    public class HotelImage : Entity
    {
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public string? ImageUrl { get; set; }
        public string? CloudinaryId { get; set; }
    }
}
