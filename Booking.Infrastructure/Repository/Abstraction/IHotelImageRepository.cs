using Booking.Domain.Entities.HotelAggregate;
using Booking.Infrastructure.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Abstraction
{
    public interface IHotelImageRepository : IRepository<HotelImage>
    {

    }
}
