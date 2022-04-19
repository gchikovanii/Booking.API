using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.WishListCommands
{
    public class CreateWishListCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }
    }
}
