using Booking.Hotel.Data;
using Booking.Hotel.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Hotel.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelEgress"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="hotelStore">The hotel store.</param>
        public HotelEgress(ILogger<HotelEgress> logger, IHotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _logger = logger;
        }

        /// <summary>
        /// Returns Hotel Deails for Valid Input
        /// </summary>
        /// <param name="hotelCode">hotelCode : GUID Type</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Details/{hotelCode}", Name = "v1/HotelDetails")]
        public async Task<ActionResult> GetHotelDetails([FromRoute] string hotelCode, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && !string.IsNullOrWhiteSpace(hotelCode))
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelDetails)} Started at {DateTime.UtcNow} ");

                var response = await _hotelStore.GetHotelDetails(hotelCode).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelDetails)} Finished at {DateTime.UtcNow} ");

                return response != null ? Ok(response) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        /// <summary>
        /// Gets the hotel by preference.
        /// </summary>
        /// <param name="requestDetails">The request details.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("GetHotels", Name = "v1/GetHotels")]
        public async Task<ActionResult> GetHotelByPreference([FromBody] RequestFilters requestDetails, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && requestDetails != null)
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByPreference)} Started at {DateTime.UtcNow} ");

                var response = await _hotelStore.GetHotelsByPreference(requestDetails.PreferenceType, requestDetails.Radius, requestDetails.PageDetails).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByPreference)} Finished at {DateTime.UtcNow} ");

                return response != null ? Ok(response) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("byName", Name = "v1/GetHotelByName")]
        public async Task<ActionResult> GetHotelByName([FromBody] RequestFilters requestDetails, CancellationToken ct)
        {
            if (!ct.IsCancellationRequested && !string.IsNullOrWhiteSpace(requestDetails.HotelName))
            {
                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByName)} Started at {DateTime.UtcNow} ");

                var response = await _hotelStore.FindHotelByName(requestDetails.HotelName, requestDetails.PageDetails ?? new PagedResponse()).ConfigureAwait(false);

                _logger.LogInformation($"{nameof(HotelEgress)}:{nameof(GetHotelByName)} Finished at {DateTime.UtcNow} ");

                return response != null ? Ok(response) : BadRequest("Unable to find Hotel Deatils for Given Parameters");
            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


        [ProducesResponseType(200, Type = typeof(HotelDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("byName", Name = "v1/GetHotelByName")]
        public async Task<ActionResult> AddBooking(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
            {

            }
            return BadRequest("No Parameters Recieved or Cancellation Requested");
        }


    }
}
