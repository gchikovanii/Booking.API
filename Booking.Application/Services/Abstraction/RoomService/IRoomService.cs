using Booking.Application.Models;
using Booking.Application.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Abstraction
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllRooms();
        Task<List<RoomDto>> GetRoomByHotelId(int id);
        Task<bool> CreateRoom(CreateRoomDto input);
        Task<bool> UpdateNumberOfRooms(HotelRoomTypeNumbersDto input);
        Task<bool> UpdateRoom(int id, UpdateRoomDto input);
        Task<bool> DeleteRoom(DeleteRoomDto input);
    }
}
