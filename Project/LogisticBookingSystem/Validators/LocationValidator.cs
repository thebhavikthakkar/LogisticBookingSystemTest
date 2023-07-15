using FluentValidation;
using LogisticBookingSystem.Models.Requests;

namespace LogisticBookingSystem.Validators
{
    public class LocationValidator : AbstractValidator<LocationModel>
    {
        public LocationValidator()
        {
            _ = RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required.")
                .MaximumLength(50).WithMessage("Location name cannot exceed 50 characters.");

            _ = RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Location address is required.")
                .MaximumLength(100).WithMessage("Location address cannot exceed 100 characters.");

            _ = RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Location capacity is required.")
                .GreaterThan(0).WithMessage("Location capacity must be greater than 0.");
        }
    }
}
