using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.WishListCommands
{
    public class DeleteWishListCommandHandler : IRequestHandler<DeleteWishListCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteWishListCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteWishListCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.UserId);
            var hotel = user.Result.Hotels.SingleOrDefault(i => i.Id == request.HotelId);
            
            if(hotel != null)
            {
                user.Result.Hotels.Remove(hotel);
                return await _userRepository.SaveChangesAsync();
            }
            else
                return false;

        }
    }
}
