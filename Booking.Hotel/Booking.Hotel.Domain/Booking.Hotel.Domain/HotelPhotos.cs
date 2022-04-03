using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    /// <summary>
    /// Hotel photos DTO
    /// </summary>
    [DataContract]
    public class HotelPhotos
    {
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        [DataMember]
        public int Order { get; set; }
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [DataMember]
        public string Uri { get; set; }
    }
}
