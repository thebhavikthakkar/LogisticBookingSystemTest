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
    public class GetBookingQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly GetBookingsByDateRangeQueryHandler _handler;

        public GetBookingQueryHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _handler = new GetBookingsByDateRangeQueryHandler(_bookingRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ValidQuery_ReturnsListOfBookings()
        {
            // Arrange
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = DateTime.UtcNow.AddDays(7);
            GetBookingsByDateRangeQuery query = new(
                startDate,
                endDate
            );
            List<Booking> bookings = new()
            {
                new Booking { Id = Guid.NewGuid(), Date = startDate.AddDays(1) },
                new Booking { Id = Guid.NewGuid(), Date = startDate.AddDays(2) },
                new Booking { Id = Guid.NewGuid(), Date = startDate.AddDays(3) }
            };
            _ = _bookingRepositoryMock
                .Setup(x => x.GetBookingsByDateRangeAsync(startDate, endDate))
                .ReturnsAsync(bookings);

            // Act
            List<Booking> result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _ = result.Should().NotBeNull();
            _ = result.Should().BeEquivalentTo(bookings);
            _ = result.Count.Should().Be(bookings.Count);
            _ = result.All(b => b.Date >= startDate && b.Date <= endDate).Should().BeTrue();
            _bookingRepositoryMock.Verify(x => x.GetBookingsByDateRangeAsync(startDate, endDate), Times.Once);
        }
    }
}
