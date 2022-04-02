using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    [DataContract]
    public class HotelReviews
    {
        [DataMember]
        public Guid ReviewId { get; set; }
        [DataMember]

        public int GivenRating { get; set; }
        [DataMember]

        public string Details { get; set; }
    }

}
