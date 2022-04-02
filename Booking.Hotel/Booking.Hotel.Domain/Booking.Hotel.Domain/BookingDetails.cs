using System;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{

    /// <summary>
    /// Booking Details DTO
    /// </summary>
    [DataContract]
    public class BookingDetails
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [DataMember]

        public Guid CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        [DataMember]

        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>
        /// The room identifier.
        /// </value>
        [DataMember]
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the check in. EPOCH Time
        /// </summary>
        /// <value>
        /// The check in.
        /// </value>
        [DataMember]
        public long CheckIn { get; set; }
        /// <summary>
        /// Gets or sets the check out. EPOCH Time
        /// </summary>
        /// <value>
        /// The check out.
        /// </value>
        [DataMember]

        public long CheckOut { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this Booking is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool isActive { get; set; }


    }
}
