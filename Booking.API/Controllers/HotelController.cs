using Booking.API.Constants;
using Booking.Application.Abstraction;
using Booking.Application.Commands.HotelCommands;
using Booking.Application.Filters;
using Booking.Application.Models.Hotels;
using Booking.Application.Queries.HotelQuery;
using Booking.Application.Services.Abstraction.IHotelService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHotelService _hotelService;
        private readonly IHotelImageService _hotelImageService;

        public HotelController(IMediator mediator, IHotelService hotelService, IHotelImageService hotelImageService)
        {
            _mediator = mediator;
            _hotelService = hotelService;
            _hotelImageService = hotelImageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _mediator.Send(new GetHotelQuery());
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelsById(id);
            return Ok(hotel);
        }
        [HttpGet(nameof(GetPaiginatedHotels))]
        public async Task<IActionResult> GetPaiginatedHotels(int index,int pageSize)
        {
            var hotels = await _hotelService.GetHotelsPage(index, pageSize);
            return Ok(hotels);
        }


        [HttpPost]
        [Authorize(Roles = UserType.AdminSuperVisor)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (ImageNotUploadedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> UpdateHotel([FromQuery] UpdateHotelCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> DeleteHotel([FromQuery] DeleteHotelCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(nameof(UploadImageOnCloudinary))]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> UploadImageOnCloudinary([FromForm]CreateHotelImageDto input)
        {
            return Ok(await _hotelImageService.UploadImage(input));
        }
    }
}
