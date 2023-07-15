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
    public class BookingCompletionValidatorTests
    {
        private Mock<ILocationRepository> _locationRepositoryMock;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private BookingCompletionValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _validator = new BookingCompletionValidator(_locationRepositoryMock.Object, _bookingRepositoryMock.Object);
        }

        [TestMethod]
        public void Validate_BookingCanBeCompleted_Valid()
        {
            // Arrange
            var booking = new Booking
            {
                State = BookingState.Unloading
            };

            // Act
            var result = _validator.TestValidate(booking);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Validate_BookingCannotBeCompleted_Invalid()
        {
            // Arrange
            var booking = new Booking
            {
                State = BookingState.Arrived
            };

            // Act
            var result = _validator.TestValidate(booking);

            // Assert
            result.ShouldHaveValidationErrorFor(b => b)
                .WithErrorMessage("Booking cannot be completed.");
        }
    }
}
