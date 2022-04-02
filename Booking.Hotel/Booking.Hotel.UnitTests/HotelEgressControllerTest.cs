using Booking.Hotel.API;
using Booking.Hotel.API.Controllers;
using Booking.Hotel.Data;
using Booking.Hotel.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Booking.Hotel.UnitTests
{
    public class HotelEgressControllerTest
    {
        private readonly HotelEgress _hotelEgressContaoller;
        private readonly Mock<IHotelStore> _hotelStore;
        private readonly Mock<ILogger<HotelEgress>> _logger;

        public HotelEgressControllerTest()
        {
            var factory = new MockRepository(MockBehavior.Default);
            _hotelStore = factory.Create<IHotelStore>();
            _logger = factory.Create<ILogger<HotelEgress>>();
            _hotelEgressContaoller = new HotelEgress(_logger.Object, _hotelStore.Object);
        }
        private static HashSet<HotelDetails> HoteDetailsStore => new HashSet<HotelDetails>
        {
            new HotelDetails{ Description="Description 1",Facilities= new HashSet<string>{ "BreakFast","WIFI","Parking","Spa"},HotelCode=Guid.Parse("bb25ba04-6a60-4347-9c73-d92ba0a9b29f"),
                Location=new Location{ Address="Address 1",City="Dubai",Country="UAE",GeoCoordinates=new GeoCoordinates{ Latitude="123",Longitutde="123"}, GoogleLocationCode="",ZipCode=123233 },Name="Hotel 1",
                Photos =new HashSet<HotelPhotos> { new HotelPhotos { Order=1,Uri="https://picsum.photos/id/237/200/300" } , new HotelPhotos { Order=2,Uri= "https://picsum.photos/seed/picsum/200/300" } },
                RateDetals=new HotelRateCard{ HotelCode=Guid.Parse("bb25ba04-6a60-4347-9c73-d92ba0a9b29f"),Price=100,RoomType="Standrad" }, IsPopular=true,IsRecommanded=true,Ratings=3,HotelReviews=300 },
        };


        [Fact]
        public async Task GetHotelByPreferenceValidInputShouldReturnData()
        {
            //Arrage
            var requestFilter = new RequestFilters()
            {
                PreferenceType = PreferenceType.Popular,
                Radius = 2,
                PageDetails = new PagedResponse
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
            _hotelStore.Setup(x => x.GetHotelsByPreference(It.IsAny<PreferenceType>(), It.IsAny<int>(), It.IsAny<PagedResponse>())).ReturnsAsync(HoteDetailsStore.Where(x => x.IsPopular).ToList());
            //Act
            var response = await _hotelEgressContaoller.GetHotelByPreference(requestFilter, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetHotelByPreferenceInValidInputShouldReturnErrorMessage()
        {
            //Arrage
            var requestFilter = new RequestFilters()
            {
                PreferenceType = PreferenceType.Popular,
                Radius = 2,
                PageDetails = new PagedResponse
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
            _hotelStore.Setup(x => x.GetHotelsByPreference(It.IsAny<PreferenceType>(), It.IsAny<int>(), It.IsAny<PagedResponse>())).ReturnsAsync(() => null);
            //Act
            var response = await _hotelEgressContaoller.GetHotelByPreference(requestFilter, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
        }


        [Theory]
        [InlineData("ot")]
        [InlineData("Ho")]
        public async Task GetHotelByNameValidInputShouldReturnData(string hotelName)
        {
            //Arrage
            var requestFilter = new RequestFilters()
            {
                HotelName = hotelName,
                PageDetails = new PagedResponse
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
            _hotelStore.Setup(x => x.FindHotelByName(It.IsAny<string>(), It.IsAny<PagedResponse>())).ReturnsAsync(HoteDetailsStore.Where(x => x.Name.Contains("ot") && x.IsActive).ToList());
            //Act
            var response = await _hotelEgressContaoller.GetHotelByName(requestFilter, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetHotelByNameInValidInputShouldReturnErrorMessage()
        {
            //Arrage
            var requestFilter = new RequestFilters()
            {
                HotelName = "SomeName",
                PageDetails = new PagedResponse
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
            _hotelStore.Setup(x => x.FindHotelByName(It.IsAny<string>(), It.IsAny<PagedResponse>())).ReturnsAsync(() => null);
            //Act
            var response = await _hotelEgressContaoller.GetHotelByName(requestFilter, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
        }



        [Fact]
        public async Task AddBookingValidInputShouldReturnData()
        {
            //Arrage
            _hotelStore.Setup(x => x.AddBooking(It.IsAny<BookingDetails>())).ReturnsAsync(true);
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
            var response = await _hotelEgressContaoller.AddBooking(bookingDetail, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AddBookingInValidInputShouldReturnErrorMessage()
        {
            //Arrage
            _hotelStore.Setup(x => x.AddBooking(It.IsAny<BookingDetails>())).ReturnsAsync(false);
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
            var response = await _hotelEgressContaoller.AddBooking(bookingDetail, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
        }





        [Fact]
        public async Task RemoveBookingValidInputShouldReturnData()
        {
            //Arrage
            _hotelStore.Setup(x => x.AddBooking(It.IsAny<BookingDetails>())).ReturnsAsync(true);
            _hotelStore.Setup(x => x.RemoveBooking(It.IsAny<Guid>())).ReturnsAsync(true);
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
            _ = await _hotelEgressContaoller.AddBooking(bookingDetail, new CancellationTokenSource().Token).ConfigureAwait(false);
            var response = await _hotelEgressContaoller.RemoveBooking(bookingDetail.Id.ToString(), new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RemoveBookingInValidInputShouldReturnErrorMessage()
        {
            //Arrage
            _hotelStore.Setup(x => x.RemoveBooking(It.IsAny<Guid>())).ReturnsAsync(false);

            //Act
            var response = await _hotelEgressContaoller.RemoveBooking(Guid.NewGuid().ToString(), new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
        }


        [Theory]
        [InlineData("bb25ba04-6a60-4347-9c73-d92ba0a9b29f")]
        public async Task GetHotelDetailsValidInputShouldReturnData(string hotelCode)
        {
            //Arrage
            _hotelStore.Setup(x => x.GetHotelDetails(It.IsAny<string>())).ReturnsAsync(HoteDetailsStore.First());
            //Act
            var response = await _hotelEgressContaoller.GetHotelDetails(hotelCode, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("SomeString")]
        [InlineData("92f776-49ab-b385-c0d2f4143c84")]
        public async Task GetHotelDetailsInValidInputShouldReturnErrorMessage(string hotelCode)
        {
            //Arrage
            _hotelStore.Setup(x => x.GetHotelDetails(It.IsAny<string>())).ReturnsAsync(() => null);
            //Act
            var response = await _hotelEgressContaoller.GetHotelDetails(hotelCode, new CancellationTokenSource().Token).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<BadRequestObjectResult>();
        }

    }

}

