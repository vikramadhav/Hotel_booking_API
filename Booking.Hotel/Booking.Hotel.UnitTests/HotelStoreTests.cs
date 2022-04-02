using Booking.Hotel.Data;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Booking.Hotel.Domain;
using System.Collections.Generic;
using System;

namespace Booking.Hotel.UnitTests
{
    public class HotelStoreTests
    {
        private readonly HotelStore _hotelStore;
        private readonly PagedResponse _pagedResponse;
        public HotelStoreTests()
        {
            _hotelStore = new HotelStore();
            _pagedResponse = new PagedResponse
            {
                PageNumber = 1,
                PageSize = 1
            };
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

        [Theory]
        [InlineData(PreferenceType.None, 2)]
        [InlineData(PreferenceType.Popular, 2)]
        [InlineData(PreferenceType.Recommanded, 2)]
        [InlineData(PreferenceType.TopRating, 2)]
        public async Task GetHotelsByPreferenceValidInputShouldReturnData(PreferenceType preferenceType, int radius)
        {
            //Arrage

            //Act
            var hotelResponse = await _hotelStore.GetHotelsByPreference(preferenceType, radius, _pagedResponse).ConfigureAwait(false);
            //Assert
            hotelResponse.Should().BeOfType<List<HotelDetails>>();
            hotelResponse.Should().HaveCountGreaterThanOrEqualTo(0);
            hotelResponse.ForEach(x => x.Description.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Name.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Location.Should().NotBeNull());
            hotelResponse.ForEach(x => x.RateDetals.Should().NotBeNull());
        }

        [Theory]
        [InlineData(PreferenceType.None, 2)]
        public async Task GetHotelsByPreferenceInValidInputShouldReturnNull(PreferenceType preferenceType, int radius)
        {
            //Arrange
            _pagedResponse.PageNumber = 50;
            _pagedResponse.PageSize = 50;

            //Act
            var hotelResponse = await _hotelStore.GetHotelsByPreference(preferenceType, radius, _pagedResponse).ConfigureAwait(false);

            //Assert
            hotelResponse.Should().BeEmpty();
        }

        [Theory]
        [InlineData("ot")]
        [InlineData("1")]
        public async Task FindHotelByNameValidInputShouldReturnData(string name)
        {
            //Arrage
            _pagedResponse.PageNumber = 1;
            _pagedResponse.PageSize = 50;

            //Act
            var hotelResponse = await _hotelStore.FindHotelByName(name, _pagedResponse).ConfigureAwait(false);

            //Assert
            hotelResponse.Should().BeOfType<List<HotelDetails>>();
            hotelResponse.Should().HaveCountGreaterThanOrEqualTo(1);
            hotelResponse.ForEach(x => x.Description.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Name.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Location.Should().NotBeNull());
            hotelResponse.ForEach(x => x.RateDetals.Should().NotBeNull());
        }

        [Theory]
        [InlineData("Unknown")]
        public async Task FindHotelByNameInValidInputShouldReturnNull(string name)
        {
            //Arrange
            _pagedResponse.PageNumber = 50;
            _pagedResponse.PageSize = 50;

            //Act
            var hotelResponse = await _hotelStore.FindHotelByName(name, _pagedResponse).ConfigureAwait(false);

            //Assert
            hotelResponse.Should().BeEmpty();
        }


        [Theory]
        [InlineData(33.844843, -45.54911)]
        [InlineData( -45.54911, 33.844843)]
        public async Task GetHotelByGeoLocationValidInputShouldReturnData(double longitude,double latitude)
        {
            //Arange
            var geoCordinate = new GeoCoordinates
            {
                Latitude = latitude,
                Longitutde = longitude
            };
            _pagedResponse.PageNumber = 1;
            _pagedResponse.PageSize = 50;
            //Act
            var hotelResponse = await _hotelStore.GetHotelByGeoLocation(geoCordinate, _pagedResponse,100).ConfigureAwait(false);

            //Assert
            hotelResponse.Should().NotBeEmpty();
            hotelResponse.Should().BeOfType<List<HotelDetails>>();
            hotelResponse.Should().HaveCountGreaterThanOrEqualTo(1);
            hotelResponse.ForEach(x => x.Description.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Name.Should().NotBeNullOrWhiteSpace());
            hotelResponse.ForEach(x => x.Location.Should().NotBeNull());
            hotelResponse.ForEach(x => x.RateDetals.Should().NotBeNull());
        }

        [Fact]
        public async Task AddBookingValidInputShouldReturnTrue()
        {
            //Arrage
            var bookingDetail = new BookingDetails
            {
                CheckIn = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                CheckOut = (int)(DateTime.UtcNow.AddDays(3) - new DateTime(1970, 1, 1)).TotalSeconds,
                CustomerId = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                isActive = true,
                RoomId = new Random().Next(1, 100)
            };

            //Act
            var bookingResponse = await _hotelStore.AddBooking(bookingDetail).ConfigureAwait(false);

            //Assert
            bookingResponse.Should().BeTrue();
        }

        [Fact]
        public async Task AddBookingInValidInputShouldReturnFalse()
        {
            //Arrage
            var checkInSecond = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            var checkoutSecond = (int)(DateTime.UtcNow.AddDays(3) - new DateTime(1970, 1, 1)).TotalSeconds;
            var CustomerId = Guid.NewGuid();
            var HotelId = Guid.NewGuid();

            var RoomId = new Random().Next(1, 100);

            var bookingDetail = new BookingDetails
            {
                CheckIn = checkInSecond,
                CheckOut = checkoutSecond,
                CustomerId = CustomerId,
                HotelId = HotelId,
                Id = Guid.NewGuid(),
                isActive = true,
                RoomId = RoomId
            };

            //Act

            _ = await _hotelStore.AddBooking(bookingDetail).ConfigureAwait(false);

            // Make Duplicate Booking Which Should Fail
            var bookingResponse = await _hotelStore.AddBooking(bookingDetail).ConfigureAwait(false);

            //Assert
            bookingResponse.Should().BeFalse();
        }

        [Fact]
        public async Task RemoveBookingValidInputShouldReturnTrue()
        {
            //Arrage
            var id = Guid.NewGuid();
            var bookingDetail = new BookingDetails
            {
                CheckIn = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                CheckOut = (int)(DateTime.UtcNow.AddDays(3) - new DateTime(1970, 1, 1)).TotalSeconds,
                CustomerId = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                Id = id,
                isActive = true,
                RoomId = new Random().Next(1, 100)
            };

            //Act
            _ = await _hotelStore.AddBooking(bookingDetail).ConfigureAwait(false);
            var bookingResponse = await _hotelStore.RemoveBooking(id).ConfigureAwait(false);

            //Assert
            bookingResponse.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveBookingInValidInputShouldReturnFalse()
        {
            //Arrage
            var id = Guid.NewGuid();

            //Act
            var bookingResponse = await _hotelStore.RemoveBooking(id).ConfigureAwait(false);

            //Assert
            bookingResponse.Should().BeFalse();
        }
    }
}
