using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    [DataContract]
    public class HotelPhotos
    {
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Uri { get; set; }
    }
}
