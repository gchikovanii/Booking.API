using Booking.Application.Services.Abstraction.JwtAuth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Implementation.JwtAuth
{
    public class JwtAuthetificationManager : IJwtAuthetificationManager
    {
        private readonly string _key;

        public JwtAuthetificationManager(string key)
        {
            _key = key;
        }

        public string Authenticate(bool status,string email,List<string> roles)
        {
            if (status)
            {
                var claims = new List<Claim>();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                claims.Add(new Claim(ClaimTypes.Email, email));
                var tokenKey = Encoding.ASCII.GetBytes(_key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescription = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescription);
                return tokenHandler.WriteToken(token);
            }
            return status.ToString();
        }
    }
}
