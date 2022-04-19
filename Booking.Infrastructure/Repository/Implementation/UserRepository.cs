using Booking.Domain.Entities;
using Booking.Domain.Entities.UserAggregate;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext;


        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        //Creates wish list
        public async Task CreateWishList(int HotelID,int UserId)
        {
            var user = await _dbcontext.Set<AppUser>().Include(i => i.Hotels).SingleOrDefaultAsync(i => i.Id == UserId);
            var hotel = await _dbcontext.Set<Hotel>().SingleOrDefaultAsync(i => i.Id == HotelID);
            if(!user.Hotels.Any(i => i.Id == HotelID))
                user.Hotels.Add(hotel);

        }

        //Find By Id
        public async Task<AppUser> GetById(int id)
        {
            return await _dbcontext.Set<AppUser>().Include(i => i.Hotels).SingleOrDefaultAsync(i => i.Id == id);
        }

    }
}
