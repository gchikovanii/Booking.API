using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public DateTimeOffset ChekInTime { get; set; }
        public DateTimeOffset ChekOutTime { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
