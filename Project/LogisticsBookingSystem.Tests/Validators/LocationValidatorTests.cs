using FluentValidation.TestHelper;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogisticBookingSystem.Tests.Validators
{
    [TestClass]
    public class LocationValidatorTests
    {
        private LocationValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new LocationValidator();
        }

        [TestMethod]
        public void Validate_LocationModel_Valid()
        {
            // Arrange
            var location = new LocationModel
            {
                Name = "Location",
                Address = "123 Main St",
                Capacity = 10
            };

            // Act
            var result = _validator.TestValidate(location);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [DataTestMethod]
        [DataRow(null, null, null)]
        public void Validate_LocationModel_Invalid(string name, string address, int capacity)
        {
            // Arrange
            var location = new LocationModel
            {
                Name = name,
                Address = address,
                Capacity = capacity
            };

            // Act
            var result = _validator.TestValidate(location);

            // Assert
            result.ShouldHaveValidationErrorFor(l => l.Name);
            result.ShouldHaveValidationErrorFor(l => l.Address);
            result.ShouldHaveValidationErrorFor(l => l.Capacity);
        }
    }
}
