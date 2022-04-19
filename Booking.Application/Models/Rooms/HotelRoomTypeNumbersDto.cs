using Booking.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.Rooms
{
    public class HotelRoomTypeNumbersDto
    {
        public int HotelId { get; set; }
        public BedType BedType { get; set; }
        public int RoomsCount { get; internal set; }
    }
}