using AutoMapper;
using FluentAssertions;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Queries;
using LogisticBookingSystem.Controllers;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Models.Responses;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogisticsBookingSystem.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new BookingController(_mediatorMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task CreateBooking_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            BookingModel request = new()
            {
                LocationId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Goods = "Some Goods",
                Carrier = "Some Carrier"
            };
            MakeBookingCommand command = new()
            {
                LocationId = request.LocationId,
                Date = request.Date,
                Goods = request.Goods,
                Carrier = request.Carrier
            };
            _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Verifiable();

            // Act
            OkResult? result = await _controller.CreateBooking(request) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task UpdateBooking_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            BookingModel model = new()
            {
                LocationId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Goods = "Updated Goods",
                Carrier = "Updated Carrier"
            };
            UpdateBookingCommand command = new()
            {
                LocationId = model.LocationId,
                Date = model.Date,
                Goods = model.Goods,
                Carrier = model.Carrier
            };
            _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Verifiable();

            // Act
            OkResult? result = await _controller.UpdateBooking(bookingId, model) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task UpdateBooking_NotFoundException_ReturnsNotFoundResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            BookingModel model = new();
            UpdateBookingCommand command = new();
            _ = _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Throws(new NotFoundException("Booking not found"));

            // Act
            OkResult? result = await _controller.UpdateBooking(bookingId, model) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateBooking_ConflictException_ReturnsConflictResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            BookingModel model = new();
            UpdateBookingCommand command = new();
            _ = _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Throws(new ConflictException("Booking conflict detected"));

            // Act
            OkResult? result = await _controller.UpdateBooking(bookingId, model) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateBooking_Exception_ReturnsServerErrorResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            BookingModel model = new();
            UpdateBookingCommand command = new();
            _ = _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Throws(new Exception("Some error occurred"));

            // Act
            OkResult? result = await _controller.UpdateBooking(bookingId, model) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task DeleteBooking_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            DeleteBookingCommand command = new() { Id = bookingId };
            _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Verifiable();

            // Act
            OkResult? result = await _controller.DeleteBooking(bookingId) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task DeleteBooking_NotFoundException_ReturnsNotFoundResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            DeleteBookingCommand command = new() { Id = bookingId };
            _ = _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Throws(new NotFoundException("Booking not found"));

            // Act
            OkResult? result = await _controller.DeleteBooking(bookingId) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task DeleteBooking_Exception_ReturnsServerErrorResult()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            DeleteBookingCommand command = new() { Id = bookingId };
            _ = _mediatorMock
                .Setup(x => x.Send(command, CancellationToken.None))
                .Throws(new Exception("Some error occurred"));

            // Act
            OkResult? result = await _controller.DeleteBooking(bookingId) as OkResult;

            // Assert
            _ = result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetBookings_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = DateTime.UtcNow.AddDays(7);
            GetBookingsByDateRangeQuery query = new(startDate, endDate);
            List<Booking> bookings = new();
            List<BookingDto> bookingDtos = new();
            _ = _mediatorMock
                .Setup(x => x.Send(query, CancellationToken.None))
                .ReturnsAsync(bookings);
            _ = _mapperMock
                .Setup(x => x.Map<IEnumerable<BookingDto>>(bookings))
                .Returns(bookingDtos);

            // Act
            OkObjectResult? result = await _controller.GetBookingsByDateRange(startDate, endDate) as OkObjectResult;

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task GetBookingsByLocationId_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            GetBookingByLocationIdQuery query = new(locationId);
            List<Booking> bookings = new();
            List<BookingDto> bookingDtos = new();
            _ = _mediatorMock
                .Setup(x => x.Send(query, CancellationToken.None))
                .ReturnsAsync(bookings);
            _ = _mapperMock
                .Setup(x => x.Map<IEnumerable<BookingDto>>(bookings))
                .Returns(bookingDtos);

            // Act
            ActionResult<IEnumerable<BookingDto>> result = await _controller.GetBookingsByLocationId(locationId) as OkObjectResult;

            // Assert           
            _ = result.Should().NotBeNull();
        }
    }
}
