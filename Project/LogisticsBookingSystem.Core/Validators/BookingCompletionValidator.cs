using FluentValidation;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Repositories;

namespace LogisticsBookingSystem.Core.Validators
{
    public class BookingCompletionValidator : AbstractValidator<Booking>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IBookingRepository bookingRepository;
        public BookingCompletionValidator(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            this.locationRepository = locationRepository;
            this.locationRepository = locationRepository;
           
            _ = RuleFor(booking => booking)
                .Must(CanBeCompleted)
                .WithMessage("Booking cannot be completed.");
        }
       
        private bool CanBeCompleted(Booking booking)
        {
            if (booking.State != BookingState.Unloading)
            {
                return false;
            }

            return true;
        }

      
    }
}
