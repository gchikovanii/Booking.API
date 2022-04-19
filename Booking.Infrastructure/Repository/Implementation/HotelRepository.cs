using Booking.Domain.Entities;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
