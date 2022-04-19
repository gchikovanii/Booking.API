using Booking.API.Constants;
using Booking.Application.Commands.WishListCommands;
using Booking.Application.Queries.HotelQuery;
using Booking.Application.Services.Abstraction.ICloudinaryService;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICloudinaryService _cloudinaryService;

        public WishListController(IMediator mediator, ICloudinaryService cloudinaryService)
        {
            _mediator = mediator;
            _cloudinaryService = cloudinaryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetWishListOfHottels([FromQuery]GetHotelQuery input)
        {
            return Ok(await _mediator.Send(input));
        }


        [HttpPost(nameof(CreateWishList))]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> CreateWishList([FromBody] CreateWishListCommand input)
        {
            return Ok(await _mediator.Send(input));
        }


        [HttpDelete]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> Delete([FromQuery]DeleteWishListCommand input)
        {
            return Ok(await _mediator.Send(input));
        }



    }
}
