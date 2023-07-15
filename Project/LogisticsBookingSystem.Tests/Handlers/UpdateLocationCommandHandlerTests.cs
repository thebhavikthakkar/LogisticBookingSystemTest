using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogisticsBookingSystem.Tests.Handlers
{
    [TestClass]
    public class UpdateLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly UpdateLocationCommandHandler _handler;

        public UpdateLocationCommandHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _handler = new UpdateLocationCommandHandler(_locationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_UpdatesLocation()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            Location location= new Location
            {
                Id = Guid.NewGuid()
            };
            _locationRepositoryMock.Setup(r => r.GetByIdAsync(location.Id))
                .ReturnsAsync(location);

            UpdateLocationCommand command = new()
            {
                Id = location.Id,
                Name = "Updated Location",
                Address = "Updated Address",
                Capacity = 20
            };
            _locationRepositoryMock
                .Setup(x => x.UpdateAsync(location))
                .Verifiable();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _locationRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Location>()), Times.Once);
        }
    }
}
