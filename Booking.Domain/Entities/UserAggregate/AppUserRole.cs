using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.UserAggregate
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser? User { get; set; }
        public AppRole? Role { get; set; }
    }
}
