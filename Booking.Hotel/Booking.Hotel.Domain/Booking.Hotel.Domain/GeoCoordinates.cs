using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    [DataContract]
    public class GeoCoordinates
    {
        [DataMember]
        public string Longitutde { get; set; }

        [DataMember]
        public string Latitude { get; set; }
    }
}
