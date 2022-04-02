using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    [DataContract]
    public class HotelRateCard
    {
        [DataMember]
        public Guid HotelCode { get; set; }

        [DataMember]
        public string RoomType { get; set; }

        [DataMember]
        public decimal Price { get; set; }
    }

}
