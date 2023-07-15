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
    public class DeleteLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly DeleteLocationCommandHandler _handler;

        public DeleteLocationCommandHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _handler = new DeleteLocationCommandHandler(_locationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ExistingLocation_DeletesLocation()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            _ = _locationRepositoryMock
                .Setup(x => x.GetByIdAsync(locationId))
                .ReturnsAsync(new Location());

            // Act
            await _handler.Handle(new DeleteLocationCommand { Id = locationId }, CancellationToken.None);

            // Assert
            _locationRepositoryMock.Verify(x => x.DeleteAsync(locationId), Times.Once);
        }

        [TestMethod]
        public async Task Handle_NonExistingLocation_ThrowsNotFoundException()
        {
            // Arrange
            Guid locationId = Guid.NewGuid();
            _ = _locationRepositoryMock
                .Setup(x => x.GetByIdAsync(locationId))
                .ReturnsAsync((Location)null);

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(new DeleteLocationCommand { Id = locationId }, CancellationToken.None);

            // Assert
            _ = await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Location not found.");
            _locationRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
