using Booking.Hotel.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Hotel.Data
{
    /// <summary>
    /// Hotel Store Interface
    /// </summary>
    public interface IHotelStore
    {
        /// <summary>
        /// Adds the booking.
        /// </summary>
        /// <param name="bookingDetails">The booking details.</param>
        /// <returns></returns>
        Task<bool> AddBooking(BookingDetails bookingDetails);

        /// <summary>
        /// Set the Booking as InActive
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <returns></returns>
        Task<bool> RemoveBooking(Guid bookingId);
        /// <summary>
        /// Finds the hotel by filters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> FindHotelByName(string name, PagedResponse pagedResponse);
        /// <summary>
        /// Gets the hotel by geo location.
        /// </summary>
        /// <param name="geoCoordinates">The geo coordinates.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> GetHotelByGeoLocation(GeoCoordinates geoCoordinates, PagedResponse pagedResponse);
        /// <summary>
        /// Gets the hotel details.
        /// </summary>
        /// <param name="hotelCode">The hotel code.</param>
        /// <returns></returns>
        Task<HotelDetails> GetHotelDetails(string hotelCode);
        /// <summary>
        /// Gets the hotels by preference.
        /// </summary>
        /// <param name="preferenceType">Type of the preference.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> GetHotelsByPreference(PreferenceType preferenceType, int radius, PagedResponse pagedResponse);
    }
}