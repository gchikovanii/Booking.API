using Booking.Application.Models.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.Abstraction.IHotelService
{
    public interface IHotelImageService
    {
        Task<bool> UploadImage(CreateHotelImageDto input);
        Task<bool> UplaodImages(List<CreateHotelImageDto> input);
    }
}
