using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    [DataContract]
    public class BookingDetails
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]

        public Guid CustomerId { get; set; }
        [DataMember]

        public Guid HotelId { get; set; }
        [DataMember]

        public DateTime CheckIn { get; set; }
        [DataMember]

        public DateTime CheckOut { get; set; }

    }
}
