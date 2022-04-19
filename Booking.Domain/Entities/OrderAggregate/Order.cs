using Booking.Domain.Entities.Base;
using Booking.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.OrderAggregate
{
    public class Order : Entity
    {
    
        public DateTimeOffset ChekInTime { get; set; }
        public DateTimeOffset ChekOutTime { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int UserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
