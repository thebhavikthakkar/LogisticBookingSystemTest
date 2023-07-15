using FluentAssertions;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsBookingSystem.Tests.Handlers
{
    [TestClass]
    public class ProcessBookingCommandHandlerTests
    {
        private Mock<ILocationRepository> _locationRepositoryMock;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private ProcessBookingCommandHandler _handler;

        public ProcessBookingCommandHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _locationRepositoryMock = new Mock<ILocationRepository>();

            _handler = new ProcessBookingCommandHandler(
                locationRepository: _locationRepositoryMock.Object,
                bookingRepository: _bookingRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task Handle_ValidBookingId_UpdateBookingState()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            Booking booking = new Booking
            {
                Id = bookingId,
                State = BookingState.Arrived,
               
                // Set other properties as needed
            };
            booking.Location = new Location
            {
                Id = Guid.NewGuid(),
                Bookings = new List<Booking> { booking }
            };
            // Setup the mock repository to return the booking
            _bookingRepositoryMock.Setup(r => r.GetByIdAsync(bookingId))
                .ReturnsAsync(booking);
            
            _locationRepositoryMock.Setup(r => r.GetByIdAsync(booking.Location.Id))
                .ReturnsAsync(booking.Location);

            // Act
            await _handler.Handle(new ProcessBookingCommand { BookingId = bookingId }, CancellationToken.None);

            // Assert
            // Verify that the booking state has been updated to the next status
            Assert.AreEqual(BookingState.Loading, booking.State);
        }

        [TestMethod]
        public async Task Handle_InvalidBookingId_ThrowsNotFoundException()
        {
            // Arrange
            Guid bookingId = Guid.NewGuid();
            _ = _locationRepositoryMock
                .Setup(x => x.GetByIdAsync(bookingId))
                .ReturnsAsync((Location)null);

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(new ProcessBookingCommand { BookingId = bookingId }, CancellationToken.None);

            // Assert
            _ = await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Booking not found.");
            _bookingRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }

}
