using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    /// <summary>
    /// Hotel Details DTO
    /// </summary>
    [Serializable]
    public class HotelDetails
    {
        /// <summary>
        /// Gets or sets the hotel code.
        /// </summary>
        /// <value>
        /// The hotel code.
        /// </value>
        [DataMember]
        public Guid HotelCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recommanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is recommanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecommanded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is popular.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is popular; otherwise, <c>false</c>.
        /// </value>
        public bool IsPopular { get; set; }

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        public int Ratings { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [DataMember]
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        [DataMember]
        public HashSet<HotelPhotos> Photos { get; set; }

        /// <summary>
        /// Gets or sets the facilities.
        /// </summary>
        /// <value>
        /// The facilities.
        /// </value>
        [DataMember]
        public HashSet<string> Facilities { get; set; }

        /// <summary>
        /// Gets or sets the rate detals.
        /// </summary>
        /// <value>
        /// The rate detals.
        /// </value>
        [DataMember]
        public HotelRateCard RateDetals { get; set; }

        /// <summary>
        /// Gets or sets the hotel reviews.
        /// </summary>
        /// <value>
        /// The hotel reviews.
        /// </value>
        [DataMember]
        public int HotelReviews { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsActive { get; set; } = true;
    }

}
