using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace LogisticBookingSystem.Tests.Application.Handlers
{
    [TestClass]
    public class UpdateBookingCommandHandlerTests
    {

        [TestMethod]
        public async Task Handle_NonExistingLocation_ThrowsNotFoundException()
        {
            // Arrange
            var locationId = Guid.NewGuid();

            var locationRepositoryMock = new Mock<ILocationRepository>();
            locationRepositoryMock.Setup(r => r.GetByIdAsync(locationId))
                .ReturnsAsync((Location)null);

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var handler = new UpdateBookingCommandHandler(locationRepositoryMock.Object, bookingRepositoryMock.Object);
            var command = new UpdateBookingCommand
            {
                LocationId = locationId,
                Date = DateTime.Today,
                Time = "10:00",
                Goods = "Goods",
                Carrier = "Carrier"
            };
            var cancellationToken = new CancellationToken();

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, cancellationToken));

            locationRepositoryMock.Verify(r => r.GetByIdAsync(locationId), Times.Once);
            bookingRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Booking>()), Times.Never);
        }

        // Add more test cases for other scenarios (e.g., validation, booking process rules, exceptions)
    }
}
