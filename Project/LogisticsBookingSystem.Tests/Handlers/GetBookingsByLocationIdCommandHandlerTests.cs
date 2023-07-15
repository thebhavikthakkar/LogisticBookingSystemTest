using FluentAssertions;
using LogisticBookingSystem.Application.Handlers;
using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogisticsBookingSystem.Tests.Handlers
{
    [TestClass]
    public class GetBookingsByLocationIdCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly GetBookingsByLocationIdCommandHandler _handler;

        public GetBookingsByLocationIdCommandHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _handler = new GetBookingsByLocationIdCommandHandler(_bookingRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ValidQuery_ReturnsBookingsByLocationId()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            GetBookingByLocationIdQuery query = new(locationId);
            List<Booking> bookings = new()
            {
                new Booking { Id = Guid.NewGuid(), LocationId = locationId },
                new Booking { Id = Guid.NewGuid(), LocationId = locationId },
                new Booking { Id = Guid.NewGuid(), LocationId = locationId }
            };
            _ = _bookingRepositoryMock
                .Setup(x => x.GetBookingsByLocationIdAsync(locationId))
                .ReturnsAsync(bookings);

            // Act
            IEnumerable<Booking> result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.Should().BeEquivalentTo(bookings);
            _ = result.Should().HaveCount(bookings.Count);
            _bookingRepositoryMock.Verify(x => x.GetBookingsByLocationIdAsync(locationId), Times.Once);
        }
    }
}
