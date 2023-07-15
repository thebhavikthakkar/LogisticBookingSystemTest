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
    public class DeleteBookingCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly DeleteBookingCommandHandler _handler;

        public DeleteBookingCommandHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _handler = new DeleteBookingCommandHandler(
                locationRepository: null,
                bookingRepository: _bookingRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task Handle_ExistingBooking_DeletesBooking()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            _ = _bookingRepositoryMock
                .Setup(x => x.GetByIdAsync(bookingId))
                .ReturnsAsync(new Booking());

            // Act
            await _handler.Handle(new DeleteBookingCommand { Id = bookingId }, CancellationToken.None);

            // Assert
            _bookingRepositoryMock.Verify(x => x.DeleteAsync(bookingId), Times.Once);
        }

        [TestMethod]
        public async Task Handle_NonExistingBooking_ThrowsNotFoundException()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            _ = _bookingRepositoryMock
                .Setup(x => x.GetByIdAsync(bookingId))
                .ReturnsAsync((Booking)null);

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(new DeleteBookingCommand { Id = bookingId }, CancellationToken.None);

            // Assert
            _ = await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Booking not found.");
            _bookingRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
