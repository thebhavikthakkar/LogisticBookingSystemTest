using FluentValidation;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Repositories;

namespace LogisticsBookingSystem.Core.Validators
{
    public class BookingCancellationValidator : AbstractValidator<Booking>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IBookingRepository bookingRepository;
        public BookingCancellationValidator(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            this.locationRepository = locationRepository;
            this.locationRepository = locationRepository;

            _ = RuleFor(booking => booking)
                .Must(CanBeCancelled)
                .WithMessage("Booking cannot be cancelled.");

        }
      
        private bool CanBeCancelled(Booking booking)
        {
            if (booking.State != BookingState.Arrived && booking.State != BookingState.Loading)
            {
                return false;
            }

            return true;
        }
       
    }
}
