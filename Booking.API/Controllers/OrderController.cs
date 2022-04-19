using Booking.API.Constants;
using Booking.Application.Commands.OrderCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        

        [HttpPost]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> Create(CreateOrderCommand input)
        {
            return Ok(await _mediator.Send(input));
        }


    }
}
