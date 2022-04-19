using Booking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Queries.HotelQuery
{
    public class GetHotelQuery : IRequest<List<HotelDto>>
    {
        public int UserId { get; set; }

    }
}
