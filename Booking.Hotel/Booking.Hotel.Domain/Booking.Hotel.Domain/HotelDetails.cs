using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    [Serializable]
    public class HotelDetails
    {
        [DataMember]
        public Guid HotelCode { get; set; }

        public bool IsRecommanded { get; set; }

        public bool IsPopular { get; set; }

        public int Ratings { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Location Location { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public HashSet<HotelPhotos> Photos { get; set; }

        [DataMember]
        public HashSet<string> Facilities { get; set; }

        [DataMember]
        public HotelRateCard RateDetals { get; set; }

        [DataMember]
        public int HotelReviews { get; set; }
    }

}
