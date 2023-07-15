using FluentValidation;
using LogisticBookingSystem.Models.Requests;

namespace LogisticBookingSystem.Validators
{
    public class BookingValidator : AbstractValidator<BookingModel>
    {
        public BookingValidator()
        {
            _ = RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Booking date is required.");


            _ = RuleFor(x => x.Goods)
                .NotEmpty().WithMessage("Goods description is required.");

            _ = RuleFor(x => x.Carrier)
                .NotEmpty().WithMessage("Carrier name is required.");

            _ = RuleFor(x => x.LocationId)
                .NotNull().WithMessage("Booking location is required.");

            _ = RuleFor(x => x.State)
                .NotEmpty().WithMessage("Booking state is required.");
        }
    }
}
