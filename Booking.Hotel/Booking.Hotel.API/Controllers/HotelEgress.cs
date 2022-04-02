using Booking.Hotel.Data;
using Booking.Hotel.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Hotel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelEgress : ControllerBase
    {
        private readonly IHotelStore _hotelStore;
        private readonly ILogger<HotelEgress> _logger;

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
        [HttpPost("Details/{hotelCode}", Name = "v1/HotelDetails")]
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
    }
}
