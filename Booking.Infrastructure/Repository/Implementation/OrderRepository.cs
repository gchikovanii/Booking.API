using Booking.Domain.Entities.OrderAggregate;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
