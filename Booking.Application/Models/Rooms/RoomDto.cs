﻿using Booking.Domain.Constants;
using Booking.Domain.Entities;
using Booking.Domain.Entities.HotelAggregate;
using Booking.Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Models
{
    public class RoomDto
    {
        public BedType? BedType { get; set; }
        public double? RoomSize { get; set; }
        public bool? Sofa { get; set; }
        public bool? TV { get; set; }
        public bool? AirConditioner { get; set; }
        public bool? MiniBar { get; set; }
        public bool? DrinkMachine { get; set; }
        public Hotel? Hotel { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
