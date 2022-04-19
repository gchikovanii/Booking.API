using Booking.Domain.Entities.Base;
using Booking.Domain.Entities.HotelAggregate;
using Booking.Domain.Entities.RoomAggregate;
using Booking.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Hotel : Entity
    {
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public string? Longtitude { get; set; }
        public string? Latitude { get; set; }
        public ICollection<Room>? Rooms { get; set; }

        public ICollection<HotelRoomTypeNumbers>? NumberOfRoomsByTypes { get; set; }
        public ICollection<AppUser>? Users { get; set; }
        public ICollection<HotelImage>? HotelImages { get; set; }
    }
}
