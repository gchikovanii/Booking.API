using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models.IdentityUsers
{
    public class UpdateUserPasswordDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
