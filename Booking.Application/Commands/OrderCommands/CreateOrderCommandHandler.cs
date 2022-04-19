using Booking.Domain.Entities.OrderAggregate;
using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Commands.OrderCommands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetQuerry(i => i.RoomId == request.RoomId);
            
            if (order.Where(i => (request.ChekInTime < i.ChekInTime && request.ChekOutTime < i.ChekInTime)
            || (request.ChekInTime > i.ChekOutTime)).Count() == 0)
            {
                await _orderRepository.Create(new Order()
                {
                    ChekInTime = request.ChekInTime,
                    ChekOutTime = request.ChekOutTime,
                    UserId = request.UserId,
                    RoomId = request.RoomId
                });
                return await _orderRepository.SaveChangesAsync();
            }
            else
                return false;
        }
    }
}
