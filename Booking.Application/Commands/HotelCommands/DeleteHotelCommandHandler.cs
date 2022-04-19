using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.HotelCommands
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, bool>
    {
        private readonly IHotelRepository _hotelRepository;
        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<bool> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetQuerry(i => i.Id == request.Id).SingleOrDefaultAsync();
            if (hotel != null)
            {
                _hotelRepository.Delete(hotel);
                return await _hotelRepository.SaveChangesAsync();
            }
            else
                return false;
        }
    }
}
