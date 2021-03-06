using Booking.Hotel.Data;
using Booking.Hotel.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Hotel.API.Controllers
{
    /// <summary>
    /// Base Controller for Hotel Transactions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <seealso cref="ControllerBase" />
    [Route("v1/api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class HotelEgress : ControllerBase
    {
        /// <summary>
        /// The hotel store
        /// </summary>
        private readonly IHotelStore _hotelStore;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HotelEgress> _logger;

        private readonly AsyncRetryPolicy _retryPolicy;


        /// <summary>
        /// Initializes a new instance of the <see cref="HotelEgress" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="hotelStore">The hotel store.</param>
        public HotelEgress(ILogger<HotelEgress> logger, IHotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _retryPolicy = Policy
        .Handle<Exception>()
        .RetryAsync(2);
            _logger = logger;
        }

        /// <summary>
        /// Returns Hotel Deails based on Filter Condinitions
        /// </summary>
        /// <param name="hotelCode">hotelCode : GUID Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("details/{hotelCode}", Name = "v1/HotelDetails")]
        public async Task<ActionResult> GetHotelDetails([FromRoute] string hotelCode, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && !string.IsNullOrWhiteSpace(hotelCode))
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelDetails)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () => await _hotelStore.GetHotelDetails(hotelCode).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelDetails)} Finished at {DateTime.UtcNow} ");

                return response.Result != null ? Ok(response.Result) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        /// <summary>
        /// Gets the hotel by preferences.
        /// </summary>
        /// <param name="requestDetails">The request details.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("GetHotels", Name = "v1/GetHotels")]
        public async Task<ActionResult> GetHotelByPreference([FromBody] RequestParameters requestDetails, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && requestDetails != null)
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByPreference)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
                await _hotelStore.GetHotelsByPreference(requestDetails.PreferenceType, requestDetails.Radius, requestDetails.PageDetails).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByPreference)} Finished at {DateTime.UtcNow} ");

                return response.Result != null ? Ok(response.Result) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        /// <summary>
        /// Gets Hotel by Name.
        /// </summary>
        /// <param name="requestDetails">The request details.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("byName", Name = "v1/GetHotelByName")]
        public async Task<ActionResult> GetHotelByName([FromBody] RequestParameters requestDetails, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && !string.IsNullOrWhiteSpace(requestDetails.HotelName))
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByName)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
                await _hotelStore.FindHotelByName(requestDetails.HotelName, requestDetails.PageDetails ?? new PagedResponse()).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByName)} Finished at {DateTime.UtcNow} ");

                return response.Result != null ? Ok(response.Result) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }

        /// <summary>
        /// Adds the booking.
        /// </summary>
        /// <param name="bookingDetails">The booking details.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("book", Name = "v1/AddBooking")]
        public async Task<ActionResult> AddBooking([FromBody] BookingDetails bookingDetails, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(AddBooking)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () => await _hotelStore.AddBooking(bookingDetails).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(AddBooking)} Finished at {DateTime.UtcNow} ");

                return response.Result ? Ok(response.Result) : BadRequest("Unable to Book Hotel and Given Details");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        /// <summary>
        /// Remove the booking.
        /// </summary>
        /// <param name="bookingid">The bookingid.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("cancel", Name = "v1/RemoveBooking")]
        public async Task<ActionResult> RemoveBooking([FromRoute] string bookingid, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && Guid.TryParse(bookingid, out Guid id))
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(RemoveBooking)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () => await _hotelStore.RemoveBooking(id).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(RemoveBooking)} Finished at {DateTime.UtcNow} ");

                return response.Result ? Ok(response.Result) : BadRequest("Unable to Remove Booking");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        /// <summary>
        /// Arrange Hotel by nearby distance
        /// </summary>
        /// <param name="requestParams">The request parameters.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("nearby", Name = "v1/GetHotelByGeoLocation")]
        public async Task<ActionResult> GetHotelByGeoLocation([FromBody] RequestParameters requestParams, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && requestParams.GeoCoordinates != null)
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(RemoveBooking)} Started at {DateTime.UtcNow} ");

                var response = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
                await _hotelStore.GetHotelByGeoLocation(requestParams.GeoCoordinates, requestParams.PageDetails ?? new PagedResponse()).ConfigureAwait(false)).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(RemoveBooking)} Finished at {DateTime.UtcNow} ");

                return response.Result != null ? Ok(response.Result) : BadRequest("Unable to Remove Booking");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


    }
}
