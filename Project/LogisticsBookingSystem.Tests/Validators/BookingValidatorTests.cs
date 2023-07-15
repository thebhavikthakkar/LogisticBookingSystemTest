using FluentValidation.TestHelper;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace LogisticBookingSystem.Tests.Validators
{
    [TestClass]
    public class BookingValidatorTests
    {
        private readonly BookingValidator _validator;

        public BookingValidatorTests()
        {
            _validator = new BookingValidator();
        }

        [TestMethod]
        public void Validate_BookingModel_Valid()
        {
            // Arrange
            var booking = new BookingModel
            {
                Date = DateTime.Now,
                Goods = "Goods",
                Carrier = "Carrier",
                LocationId = Guid.NewGuid(),
                State = (int)LogisticsBookingSystem.Core.Enum.BookingState.Completed,
            };

            // Act
            var result = _validator.TestValidate(booking);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
