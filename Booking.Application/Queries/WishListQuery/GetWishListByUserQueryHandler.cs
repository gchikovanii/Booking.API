using Booking.Application.Models;
using Booking.Infrastructure.Repository.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Queries.WishListQuery
{
    public class GetWishListByUserQueryHandler : IRequestHandler<GetWishListByUserQuery, List<HotelDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetWishListByUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<HotelDto>> Handle(GetWishListByUserQuery request, CancellationToken cancellationToken)
        {
           var user = await _userRepository.GetById(request.UserId);
           if(user.Hotels.Count > 0)
           {
                return user.Hotels.Select(i => new HotelDto()
                {
                    HotelName = i.HotelName,
                    Address = i.Address,
                    MaxPrice = i.MaxPrice,
                    MinPrice = i.MinPrice,
                    Longtitude = i .Longtitude,
                    Latitude = i .Latitude
                }).ToList();
           }
            else
                return new List<HotelDto>();
        }
    }
}
