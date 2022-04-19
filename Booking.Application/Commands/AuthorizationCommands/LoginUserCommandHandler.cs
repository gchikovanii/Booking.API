using Booking.Application.Services.Abstraction.JwtAuth;
using Booking.Domain.Entities.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.AuthorizationCommands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtAuthetificationManager _jwtAuthetManager;
        public LoginUserCommandHandler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IJwtAuthetificationManager jwtAuthetManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthetManager = jwtAuthetManager;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, false);
            var roles = await _userManager.GetRolesAsync(user);

            return _jwtAuthetManager.Authenticate(result.Succeeded, user.Email, roles.ToList());

        }
    }
}
