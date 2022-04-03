using System;
using System.Runtime.Serialization;

namespace Booking.Hotel.Domain
{
    /// <summary>
    ///  Customer Data DTO
    /// </summary>
    [DataContract]
    public class CustomerDetails
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [DataMember]
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the identification details.
        /// </summary>
        /// <value>
        /// The identification details.
        /// </value>
        [DataMember]
        public string IdentificationDetails { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember]
        public string EmailAddress { get; set; }
        /// <summary>
        /// Gets or sets the mobile number along with Country Code
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [DataMember]
        public string MobileNumber { get; set; }
    }
}
