using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    /// <summary>
    /// Geo Cordinate DTO
    /// </summary>
    [DataContract]
    public class GeoCoordinates
    {
        /// <summary>
        /// Gets or sets the longitutde.
        /// </summary>
        /// <value>
        /// The longitutde.
        /// </value>
        [DataMember]
        [Range(-90, 90, ErrorMessage = "The field {0} Should be In between 90 to -90.")]
        public double Longitutde { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [DataMember]
        [Range(-90, 90, ErrorMessage = "The field {0} Should be In between 90 to -90.")]
        public double Latitude { get; set; }
    }
}
