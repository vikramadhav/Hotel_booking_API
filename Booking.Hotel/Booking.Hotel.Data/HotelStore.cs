using Booking.Hotel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Microsoft.Extensions.Logging;

namespace Booking.Hotel.Data
{
    /// <summary>
    /// Hote Store Interface Implementation
    /// </summary>
    /// <seealso cref="Booking.Hotel.Data.IHotelStore" />
    public class HotelStore : IHotelStore
    {
        /// <summary>
        /// The booking details
        /// </summary>
        private readonly List<BookingDetails> _bookingDetails;


        private readonly ILogger<HotelStore> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelStore"/> class.
        /// </summary>
        public HotelStore(ILogger<HotelStore> logger)
        {
            _logger = logger;
            _bookingDetails = new List<BookingDetails>();
        }

        /// <summary>
        /// Gets the hotel details.
        /// </summary>
        /// <param name="hotelCode">The hotel code.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<HotelDetails> GetHotelDetails(string hotelCode)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(GetHotelDetails)} Started at {DateTime.UtcNow} ");

                return Guid.TryParse(hotelCode, out Guid hotelGuid)
                        ? await Task.FromResult(HoteDetailsStore.FirstOrDefault(x => x.HotelCode == hotelGuid && x.IsActive))
                        : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(GetHotelDetails)} Erroed at {DateTime.UtcNow} ");
                return null;
            }
        }

        /// <summary>
        /// Gets the hotels by preference.
        /// </summary>
        /// <param name="preferenceType">Type of the preference.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<List<HotelDetails>> GetHotelsByPreference(PreferenceType preferenceType, int radius, PagedResponse pagedResponse)
        {
            //Filterdown Number of Hotel Based on Radius from Current Postion of User

            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(GetHotelsByPreference)} Started at {DateTime.UtcNow} ");

                if (preferenceType == PreferenceType.Recommanded)
                {
                    return await GetRecommendedHotels(pagedResponse).ConfigureAwait(false);
                }

                if (preferenceType == PreferenceType.Popular)
                {
                    return await GetPopularHotels(pagedResponse).ConfigureAwait(false);
                }

                if (preferenceType == PreferenceType.TopRating)
                {
                    return await getTopRatedHotels(pagedResponse).ConfigureAwait(false);
                }
                return await Task.FromResult(
                      HoteDetailsStore.Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                      .Take(pagedResponse.PageSize)
                      .ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(GetHotelsByPreference)} Erroed at {DateTime.UtcNow} ");
                return null;
            }
        }

        /// <summary>
        /// Gets the top rated hotels.
        /// </summary>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        private static async Task<List<HotelDetails>> getTopRatedHotels(PagedResponse pagedResponse)
        {
            return await Task.FromResult(
                                  HoteDetailsStore.Where(x => x.Ratings > 3 && x.IsActive).Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                                  .Take(pagedResponse.PageSize)
                                  .ToList());
        }

        /// <summary>
        /// Gets the popular hotels.
        /// </summary>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        private static async Task<List<HotelDetails>> GetPopularHotels(PagedResponse pagedResponse)
        {
            return await Task.FromResult(
                                  HoteDetailsStore.Where(x => x.IsPopular && x.IsActive).Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                                  .Take(pagedResponse.PageSize)
                                  .ToList());
        }

        /// <summary>
        /// Gets the recommended hotels.
        /// </summary>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        private static async Task<List<HotelDetails>> GetRecommendedHotels(PagedResponse pagedResponse)
        {
            return await Task.FromResult(
                   HoteDetailsStore.Where(x => x.IsRecommanded && x.IsActive).Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                   .Take(pagedResponse.PageSize)
                   .ToList());
        }

        /// <summary>
        /// Gets the hotel by geo location.
        /// </summary>
        /// <param name="geoCoordinates">The geo coordinates.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <param name="radius">The radius.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<List<HotelDetails>> GetHotelByGeoLocation(GeoCoordinates geoCoordinates, PagedResponse pagedResponse, int radius = 5)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(GetHotelByGeoLocation)} Started at {DateTime.UtcNow} ");

                var currentLocation = new GeoCoordinate(geoCoordinates.Latitude, geoCoordinates.Longitutde);

                var hotelResponse = HoteDetailsStore.Select(x =>
                  new
                  {
                      HotelDetails = x,
                      Distance = currentLocation.GetDistanceTo(new GeoCoordinate(x.Location.GeoCoordinates.Latitude, x.Location.GeoCoordinates.Longitutde))
                  });
                return Task.FromResult(hotelResponse.OrderBy(x => x.Distance).Where(x => x.Distance < radius).Select(x => x.HotelDetails)
                    .Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                       .Take(pagedResponse.PageSize)
                       .ToList());
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(GetHotelByGeoLocation)} Erroed at {DateTime.UtcNow} ");
                return Task.FromResult<List<HotelDetails>>(null);
            }
        }

        /// <summary>
        /// Adds the booking.
        /// </summary>
        /// <param name="bookingDetails">The booking details.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<bool> AddBooking(BookingDetails bookingDetails)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(AddBooking)} Started at {DateTime.UtcNow} ");

                var existingBooking = _bookingDetails.Where(x => x.CustomerId == bookingDetails.CustomerId && x.HotelId == bookingDetails.HotelId && x.RoomId == bookingDetails.RoomId && x.isActive);
                if (existingBooking != null)
                {
                    //Check if Booking is Falling in Same Time Range
                    var isSameBooking = existingBooking.Any(x => x.CheckIn >= bookingDetails.CheckIn && x.CheckOut <= bookingDetails.CheckOut);
                    if (!isSameBooking)
                    {
                        _bookingDetails.Add(bookingDetails);
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
                _bookingDetails.Add(bookingDetails);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(AddBooking)} Erroed at {DateTime.UtcNow} ");
                return Task.FromResult<bool>(false);
            }

        }

        /// <summary>
        /// Set the Booking as InActive
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <returns></returns>
        public Task<bool> RemoveBooking(Guid bookingId)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(RemoveBooking)} Started at {DateTime.UtcNow} ");

                var bookingDetails = _bookingDetails.Find(x => x.Id == bookingId);
                if (bookingDetails != null)
                {
                    bookingDetails.isActive = false;
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(RemoveBooking)} Erroed at {DateTime.UtcNow} ");
                return Task.FromResult<bool>(false);
            }
        }

        /// <summary>
        /// Finds the hotel by filters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="pagedResponse">The paged response.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<List<HotelDetails>> FindHotelByName(string name, PagedResponse pagedResponse)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelStore)}:{nameof(FindHotelByName)} Started at {DateTime.UtcNow} ");
                return await Task.FromResult(
                         HoteDetailsStore.Where(x => x.Name.Contains(name) && x.IsActive).Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
                         .Take(pagedResponse.PageSize)
                         .ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"{nameof(HotelStore)}:{nameof(RemoveBooking)} Erroed at {DateTime.UtcNow} ");
                return await Task.FromResult<List<HotelDetails>>(null).ConfigureAwait(false);
            }
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
                Location=new Location{ Address="Address 1",City="Dubai",Country="UAE",GeoCoordinates=new GeoCoordinates{ Latitude=44.968046,Longitutde=-14.420307}, GoogleLocationCode="",ZipCode=123233 },Name="Hotel 1",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("bb25ba04-6a60-4347-9c73-d92ba0a9b29f"),Price=100,RoomType="Standrad" }, IsPopular=true,IsRecommanded=true,Ratings=3,HotelReviews=300 },

           new HotelDetails{ Description="Description 2",Facilities= new HashSet<string>{ "BreakFast","WIFI","Parking",},HotelCode=Guid.Parse("988a977f-f776-49ab-b385-c0d2f4143c84"),
                Location=new Location{ Address="Address 2",City="Abu Dhabi",Country="UAE",GeoCoordinates=new GeoCoordinates{ Latitude=44.33328,Longitutde=-89.132008}, GoogleLocationCode="",ZipCode=3422324 },Name="Hotel 2",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("988a977f-f776-49ab-b385-c0d2f4143c84"),Price=200,RoomType="Deluxe" }, IsPopular=true,IsRecommanded=false,Ratings=2,HotelReviews=500 },

           new HotelDetails{ Description="Description 3",Facilities= new HashSet<string>{ "BreakFast","WIFI",},HotelCode=Guid.Parse("676601ee-1f9c-4ff0-9826-777f267ba2ac"),
                Location=new Location{ Address="Address 3",City="Texas",Country="USA",GeoCoordinates=new GeoCoordinates{ Latitude=33.755787,Longitutde=-11.359998}, GoogleLocationCode="",ZipCode=423 },Name="Hotel 3",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("676601ee-1f9c-4ff0-9826-777f267ba2ac"),Price=300,RoomType="Suite" }, IsPopular=false,IsRecommanded=true,Ratings=5,HotelReviews=600 },
        };
    }

}
