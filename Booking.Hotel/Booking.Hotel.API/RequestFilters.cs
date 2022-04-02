using Booking.Hotel.Domain;
using System.Runtime.Serialization;

namespace Booking.Hotel.API
{
    [DataContract]
    public class RequestFilters
    {
        [DataMember]
        public PreferenceType PreferenceType { get; set; }
        [DataMember]
        public PagedResponse PageDetails { get; set; }
        [DataMember]
        public int Radius { get; set; }
        [DataMember]
        public string HotelName { get; set; }
    }
}
