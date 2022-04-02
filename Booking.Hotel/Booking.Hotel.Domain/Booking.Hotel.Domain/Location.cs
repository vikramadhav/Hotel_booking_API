using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    [DataContract]
    public class Location
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public int ZipCode { get; set; }

        [DataMember] public GeoCoordinates GeoCoordinates { get; set; }

        [DataMember]
        public string GoogleLocationCode { get; set; }

    }
}
