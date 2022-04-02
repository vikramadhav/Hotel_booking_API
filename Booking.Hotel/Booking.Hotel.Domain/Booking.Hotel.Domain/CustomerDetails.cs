using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    [DataContract]
    public class CustomerDetails
    {
        [DataMember]
        public Guid CustomerId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string IdentificationDetails { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
    }
}
