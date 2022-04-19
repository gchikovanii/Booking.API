using Booking.Domain.Entities.RoomAggregate;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class HotelRoomTypeNumbersRepository : Repository<HotelRoomTypeNumbers>, IHotelRoomTypeNumbersRepository
    {
        public HotelRoomTypeNumbersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
