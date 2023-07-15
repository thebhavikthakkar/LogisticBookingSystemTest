using FluentAssertions;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogisticsBookingSystem.Tests.Handlers
{
    [TestClass]
    public class MakeBookingCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly MakeBookingCommandHandler _handler;

        public MakeBookingCommandHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _handler = new MakeBookingCommandHandler(
                locationRepository: _locationRepositoryMock.Object,
                bookingRepository: _bookingRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task Handle_NonExistingLocation_ThrowsNotFoundException()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            MakeBookingCommand command = new() { LocationId = locationId };
            _ = _locationRepositoryMock
                .Setup(x => x.GetByIdAsync(locationId))
                .ReturnsAsync((Location)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            _ = await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Location not found.");
            _bookingRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Booking>()), Times.Never);
        }
    }
}
