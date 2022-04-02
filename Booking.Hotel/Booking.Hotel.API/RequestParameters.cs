using Booking.Hotel.Domain;
using System.Runtime.Serialization;

namespace Booking.Hotel.API
{
    /// <summary>
    /// Helper Class for Querying Data Store
    /// </summary>
    [DataContract]
    public class RequestParameters
    {
        /// <summary>
        /// Gets or sets the type of the preference.
        /// </summary>
        /// <value>
        /// The type of the preference.
        /// </value>
        [DataMember]
        public PreferenceType PreferenceType { get; set; }
        /// <summary>
        /// Gets or sets the page details.
        /// </summary>
        /// <value>
        /// The page details.
        /// </value>
        [DataMember]
        public PagedResponse PageDetails { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        [DataMember]
        public int Radius { get; set; }
        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        [DataMember]
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the geo coordinates.
        /// </summary>
        /// <value>
        /// The geo coordinates.
        /// </value>
        [DataMember]
        public GeoCoordinates GeoCoordinates { get; set; }
    }
}
