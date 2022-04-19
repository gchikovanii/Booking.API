using Booking.Domain.Constants;
using Booking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.RoomAggregate
{
    public class HotelRoomTypeNumbers : Entity
    {
        public BedType BedType { get; set; }
        public int NumberOfRooms { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}
