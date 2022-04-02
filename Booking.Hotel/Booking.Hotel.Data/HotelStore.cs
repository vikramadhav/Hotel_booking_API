using Booking.Hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Hotel.Data
{
    /// <summary>
    /// Hote Store Interface Implementation
    /// </summary>
    /// <seealso cref="Booking.Hotel.Data.IHotelStore" />
    public class HotelStore : IHotelStore
    {
        /// <summary>
        /// Gets the hotel details.
        /// </summary>
        /// <param name="hotelCode">The hotel code.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<HotelDetails> GetHotelDetails(string hotelCode)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the hotels by preference.
        /// </summary>
        /// <param name="preferenceType">Type of the preference.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<List<HotelDetails>> GetHotelsByPreference(PreferenceType preferenceType, string radius, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the hotel by geo location.
        /// </summary>
        /// <param name="geoCoordinates">The geo coordinates.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<List<HotelDetails>> GetHotelByGeoLocation(GeoCoordinates geoCoordinates, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the booking.
        /// </summary>
        /// <param name="bookingDetails">The booking details.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<bool> AddBooking(BookingDetails bookingDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the hotel by filters.
        /// </summary>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="CheckIn">The check in.</param>
        /// <param name="Checkout">The checkout.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<List<HotelDetails>> FindHotelByFilters(string hotelName, DateTime CheckIn, DateTime Checkout, List<string> filters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the hote details store.
        /// </summary>
        /// <value>
        /// The hote details store.
        /// </value>
        private static HashSet<HotelDetails> HoteDetailsStore => new HashSet<HotelDetails>
        {
            new HotelDetails{ Description="Description 1",Facilities= new HashSet<string>{ "BreakFast","WIFI","Parking","Spa"},HotelCode=Guid.Parse("bb25ba04-6a60-4347-9c73-d92ba0a9b29f"),
                Location=new Location{ Address="Address 1",City="Dubai",Country="UAE",GeoCoordinates=new GeoCoordinates{ Latitude="123",Longitutde="123"}, GoogleLocationCode="",ZipCode=123233 },Name="Hotel 1",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("bb25ba04-6a60-4347-9c73-d92ba0a9b29f"),Price=100,RoomType="Standrad" }, IsPopular=true,IsRecommanded=true,Ratings=3,HotelReviews=300 },



           new HotelDetails{ Description="Description 2",Facilities= new HashSet<string>{ "BreakFast","WIFI","Parking",},HotelCode=Guid.Parse("988a977f-f776-49ab-b385-c0d2f4143c84"),
                Location=new Location{ Address="Address 2",City="Abu Dhabi",Country="UAE",GeoCoordinates=new GeoCoordinates{ Latitude="123",Longitutde="123"}, GoogleLocationCode="",ZipCode=3422324 },Name="Hotel 2",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("988a977f-f776-49ab-b385-c0d2f4143c84"),Price=200,RoomType="Deluxe" }, IsPopular=true,IsRecommanded=false,Ratings=2,HotelReviews=500 },



           new HotelDetails{ Description="Description 3",Facilities= new HashSet<string>{ "BreakFast","WIFI",},HotelCode=Guid.Parse("676601ee-1f9c-4ff0-9826-777f267ba2ac"),
                Location=new Location{ Address="Address 3",City="Texas",Country="USA",GeoCoordinates=new GeoCoordinates{ Latitude="123",Longitutde="23"}, GoogleLocationCode="",ZipCode=423 },Name="Hotel 3",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("676601ee-1f9c-4ff0-9826-777f267ba2ac"),Price=300,RoomType="Suite" }, IsPopular=false,IsRecommanded=true,Ratings=5,HotelReviews=600 },


        };
    }

}
