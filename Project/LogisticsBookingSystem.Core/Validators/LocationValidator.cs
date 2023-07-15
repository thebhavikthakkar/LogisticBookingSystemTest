using FluentValidation;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;

namespace LogisticsBookingSystem.Core.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        private readonly ILocationRepository locationRepository;
        public LocationValidator(ILocationRepository locationRepository)
        {

            this.locationRepository = locationRepository;

            _ = RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required.")
                .MaximumLength(50).WithMessage("Location name cannot exceed 50 characters.");

            _ = RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Location address is required.")
                .MaximumLength(100).WithMessage("Location address cannot exceed 100 characters.");

            _ = RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Location capacity must be greater than 0.");
        }


    }
}
