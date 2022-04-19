using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Abstraction.JwtAuth
{
    public interface IJwtAuthetificationManager
    {
        string Authenticate(bool status, string email, List<string> roles);
    }
}
