using Booking.Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.UserAggregate
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole>? AppUserRoles { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
