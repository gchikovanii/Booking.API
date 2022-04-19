using Booking.Application.Abstraction;
using Booking.Application.Models;
using Booking.Application.Models.Rooms;
using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Booking.Domain.Entities.RoomAggregate;
using Booking.Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRoomTypeNumbersRepository _hotelRoomTypeNumbersRepository;
        public RoomService(IRoomRepository roomRepository, IHotelRoomTypeNumbersRepository hotelRoomTypeNumbersRepository)
        {
            _roomRepository = roomRepository;
            _hotelRoomTypeNumbersRepository = hotelRoomTypeNumbersRepository;
        }

        //Get All Rooms
        public async Task<List<RoomDto>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetCollectionAsync();
            return rooms.Select(i => new RoomDto
            {
                BedType = i.BedType,
                MiniBar = i.MiniBar,
                AirConditioner = i.AirConditioner,
                DrinkMachine = i.DrinkMachine,
                Hotel = i.Hotel,
                RoomSize = i.RoomSize,
                Sofa = i.Sofa,
                TV = i.TV
            }).ToList();    
        }
        //Get Rooms By Hotel ID
        public async Task<List<RoomDto>> GetRoomByHotelId(int id)
        {
            var rooms =  _roomRepository.GetQuerry(i => i.Hotel.Id == id);
            return await rooms.Select(i => new RoomDto()
            {
                BedType = i.BedType,
                MiniBar = i.MiniBar,
                AirConditioner = i.AirConditioner,
                DrinkMachine = i.DrinkMachine,
                Hotel = i.Hotel,
                RoomSize = i.RoomSize,
                Sofa = i.Sofa,
                TV = i.TV
            }).ToListAsync();

        }

        //Create Room
        public async Task<bool> CreateRoom(CreateRoomDto input)
        {
            var room = new Room()
            {
                BedType = input.BedType,
                MiniBar = input.MiniBar,
                AirConditioner = input.AirConditioner,
                DrinkMachine = input.DrinkMachine,
                RoomSize = input.RoomSize,
                Sofa = input.Sofa,
                TV = input.TV,
                HotelId = input.HotelId
            };
            await _roomRepository.Create(room);
            var success = await _roomRepository.SaveChangesAsync();
            if (success)
                await UpdateNumberOfRooms(new HotelRoomTypeNumbersDto { HotelId = input.HotelId, BedType = (BedType)input.BedType, RoomsCount = 1 });
            return success;
        }

        //Update Number of Rooms
        public async Task<bool> UpdateNumberOfRooms(HotelRoomTypeNumbersDto input)
        {
            var result = await _hotelRoomTypeNumbersRepository.GetQuerry(i => i.BedType == input.BedType && i.HotelId == input.HotelId).SingleOrDefaultAsync();
            
            if (result != null)
            {
                result.NumberOfRooms += input.RoomsCount;
                _hotelRoomTypeNumbersRepository.Update(result);
            }
            else
            {
                await _hotelRoomTypeNumbersRepository.Create(new HotelRoomTypeNumbers()
                {
                    BedType = (BedType)input.BedType,
                    HotelId = input.HotelId,
                    NumberOfRooms = input.RoomsCount
                });
            }
            return await _hotelRoomTypeNumbersRepository.SaveChangesAsync();
        }
        //Update Room
        public async Task<bool> UpdateRoom(int id,UpdateRoomDto input)
        {
            var room = await _roomRepository.GetQuerry(i => i.Id == id).SingleOrDefaultAsync();
            if (room != null)
            {
                if (input.BedType != null)
                    room.BedType = (BedType)input.BedType;
                if (input.MiniBar != null)
                    room.MiniBar = (bool)input.MiniBar;
                if (input.AirConditioner != null)
                    room.AirConditioner = (bool)input.AirConditioner;
                if (input.DrinkMachine != null)
                    room.DrinkMachine = (bool)input.DrinkMachine;
                if (input.Sofa != null)
                    room.Sofa = (bool)input.Sofa;
                if (input.TV != null)
                    room.TV = (bool)input.TV;
                if (input.RoomSize != null)
                    room.RoomSize = (double)input.RoomSize;
                if (input.Hotel != null)
                    room.Hotel = input.Hotel;
                _roomRepository.Update(room);
                return await _roomRepository.SaveChangesAsync();
            }
            else
            {
                return false;
            }

        }

        //Delete Room
        public async Task<bool> DeleteRoom(DeleteRoomDto input)
        {
            var room = await _roomRepository.GetQuerry(i => i.HotelId == input.HotelId && i.BedType == input.BedType).SingleOrDefaultAsync();
            if (room != null)
                _roomRepository.Delete(room);
            var success = await _roomRepository.SaveChangesAsync();
            if (success)
            {
                await UpdateNumberOfRooms(new HotelRoomTypeNumbersDto()
                {
                    BedType = input.BedType,
                    HotelId = input.HotelId,
                    RoomsCount = -input.Amount
                });
            }
            return success;

        }

       

    }
}
