using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.WishListCommands
{
    public class CreateWishListCommandHandler : IRequestHandler<CreateWishListCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public CreateWishListCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(CreateWishListCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.CreateWishList(request.HotelId, request.UserId);
            return await _userRepository.SaveChangesAsync();
        
        }
    }
}
