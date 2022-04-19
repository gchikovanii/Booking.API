using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.IdentityUsers
{
    public class UpdateUserNameDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
