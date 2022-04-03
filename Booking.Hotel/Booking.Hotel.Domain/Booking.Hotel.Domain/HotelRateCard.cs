using System;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    /// <summary>
    /// Hotel Rate CARD DTO
    /// </summary>
    [DataContract]
    public class HotelRateCard
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
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        [DataMember]
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [DataMember]
        public decimal Price { get; set; }


        /// <summary>
        /// Gets or sets the currency for Transcations
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; } = "AED";
    }

}
