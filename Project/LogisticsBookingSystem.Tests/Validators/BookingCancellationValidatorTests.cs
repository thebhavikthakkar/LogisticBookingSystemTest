using FluentValidation.TestHelper;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Core.Validators;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogisticsBookingSystem.Tests.Validators
{
    [TestClass]
    public class BookingCancellationValidatorTests
    {
        private Mock<ILocationRepository> _locationRepositoryMock;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private BookingCancellationValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _validator = new BookingCancellationValidator(_locationRepositoryMock.Object, _bookingRepositoryMock.Object);
        }

        [TestMethod]
        public void Validate_BookingCanBeCancelled_Valid()
        {
            // Arrange
            var booking = new Booking
            {
                State = BookingState.Arrived
            };

            // Act
            var result = _validator.TestValidate(booking);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Validate_BookingCannotBeCancelled_Invalid()
        {
            // Arrange
            var booking = new Booking
            {
                State = BookingState.Completed
            };

            // Act
            var result = _validator.TestValidate(booking);

            // Assert
            result.ShouldHaveValidationErrorFor(b => b)
                .WithErrorMessage("Booking cannot be cancelled.");
        }
    }
}
