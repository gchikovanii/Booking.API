using Booking.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Abstraction
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task CreateWishList(int HotelID, int UserId);
        Task<AppUser> GetById(int id);
    }
}
