using System;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    /// <summary>
    /// Hotel Review DTO
    /// </summary>
    [DataContract]
    public class HotelReviews
    {
        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        /// <value>
        /// The review identifier.
        /// </value>
        [DataMember]
        public Guid ReviewId { get; set; }
        /// <summary>
        /// Gets or sets the given rating.
        /// </summary>
        /// <value>
        /// The given rating.
        /// </value>
        [DataMember]

        public int GivenRating { get; set; }
        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        [DataMember]

        public string Details { get; set; }
    }

}
