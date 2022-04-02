using Booking.Hotel.Data;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Booking.Hotel.UnitTests
{
    public class HotelStoreTests
    {
        private readonly HotelStore _hotelStore;

        public HotelStoreTests()
        {
            _hotelStore = new HotelStore();
        }

        [Theory]
        [InlineData("bb25ba04-6a60-4347-9c73-d92ba0a9b29f")]
        [InlineData("988a977f-f776-49ab-b385-c0d2f4143c84")]
        [InlineData("676601ee-1f9c-4ff0-9826-777f267ba2ac")]
        public async Task GetHotelDetailsValidInputShouldReturnData(string hotelCode)
        {
            //Arrage

            //Act
            var hotelResponse = await _hotelStore.GetHotelDetails(hotelCode).ConfigureAwait(false);

            //Assert
            hotelResponse.Description.Should().NotBeNullOrWhiteSpace();
            hotelResponse.Name.Should().NotBeNullOrWhiteSpace();
            hotelResponse.Location.Should().NotBeNull();
            hotelResponse.RateDetals.Should().NotBeNull();
        }

        [Theory]
        [InlineData("SomeString")]
        [InlineData("92f776-49ab-b385-c0d2f4143c84")]
        public async Task GetHotelDetailsInValidInputShouldReturnNull(string hotelCode)
        {
            //Arrage

            //Act
            var hotelResponse = await _hotelStore.GetHotelDetails(hotelCode).ConfigureAwait(false);

            //Assert
            hotelResponse.Should().BeNull();
        }

    }
}
