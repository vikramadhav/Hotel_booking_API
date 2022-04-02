using Booking.Hotel.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Hotel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelEgress : ControllerBase
    {

        private readonly ILogger<HotelEgress> _logger;

        public HotelEgress(ILogger<HotelEgress> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<HotelDetails> Get(string hotelCode)
        {
            
        }
    }
}
