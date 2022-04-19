using Booking.Domain.Entities.HotelAggregate;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class HotelImageRepository : Repository<HotelImage>, IHotelImageRepository
    {
        public HotelImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
