using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    /// <summary>
    /// Hotel Location DTO
    /// </summary>
    [DataContract]
    public class Location
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [DataMember]
        public int ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the geo coordinates.
        /// </summary>
        /// <value>
        /// The geo coordinates.
        /// </value>
        [DataMember] public GeoCoordinates GeoCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the google location code.
        /// </summary>
        /// <value>
        /// The google location code.
        /// </value>
        [DataMember]
        public string GoogleLocationCode { get; set; }

    }
}
