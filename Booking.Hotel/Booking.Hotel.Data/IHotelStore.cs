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
        /// Finds the hotel by filters.
        /// </summary>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="CheckIn">The check in.</param>
        /// <param name="Checkout">The checkout.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> FindHotelByFilters(string hotelName, DateTime CheckIn, DateTime Checkout, List<string> filters);
        /// <summary>
        /// Gets the hotel by geo location.
        /// </summary>
        /// <param name="geoCoordinates">The geo coordinates.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> GetHotelByGeoLocation(GeoCoordinates geoCoordinates, int count);
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
        /// <param name="count">The count.</param>
        /// <returns></returns>
        Task<List<HotelDetails>> GetHotelsByPreference(PreferenceType preferenceType, string radius, int count);
    }
}