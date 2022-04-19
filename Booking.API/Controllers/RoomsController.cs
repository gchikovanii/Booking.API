using Booking.API.Constants;
using Booking.Application.Abstraction;
using Booking.Application.Models.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoom()
        {
            return Ok(await _roomService.GetAllRooms());
        }
        [HttpGet("{HotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(int HotelId)
        {
            return Ok(await _roomService.GetRoomByHotelId(HotelId));
        }
        [HttpPost]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> CreateRoom(CreateRoomDto input)
        {
            return Ok(await _roomService.CreateRoom(input));
        }
        [HttpPut("{RoomId}")]

        public async Task<IActionResult> UpdateRoom(int RoomId, UpdateRoomDto input)
        {
            return Ok(await _roomService.UpdateRoom(RoomId, input));
        }
        [HttpDelete]
        [Authorize(Roles = UserType.AdminSuperVisor)]

        public async Task<IActionResult> DeleteRoom(DeleteRoomDto input)
        {
            return Ok(await _roomService.DeleteRoom(input));
        }

    }
}
