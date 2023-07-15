using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogisticsBookingSystem.Tests.Handlers
{
    [TestClass]
    public class MakeLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly MakeLocationCommandHandler _handler;

        public MakeLocationCommandHandlerTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _handler = new MakeLocationCommandHandler(_locationRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_CreatesLocation()
        {
            // Arrange
            MakeLocationCommand command = new()
            {
                Name = "Test Location",
                Address = "123 Test St",
                Capacity = 10
            };
            _locationRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Location>()))
                .Verifiable();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _locationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Location>()), Times.Once);
        }
    }
}
