using FluentValidation;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Models;
using LogisticsBookingSystem.Core.Repositories;

namespace LogisticsBookingSystem.Core.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IBookingRepository bookingRepository;
        public BookingValidator(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            this.locationRepository = locationRepository;
            this.bookingRepository = bookingRepository;


            _ = RuleFor(booking => booking.LocationId)
                .NotNull()
                .WithMessage("Booking must have a valid location.");

            _ = RuleFor(booking => booking)
                .MustAsync(HaveValidCapacity)
                .WithMessage("Number of bookings at the location exceeds its capacity.");

            _ = RuleFor(booking => booking)
                .MustAsync(BeWithinTimeWindow)
                .WithMessage("Booking is outside the allowed time window for the location.");

            _ = RuleFor(booking => booking)
                .Must(FollowStateTransitionOrder)
                .WithMessage("Invalid state transition for the booking.");

            _ = RuleFor(booking => booking)
                .MustAsync(NotOverlapWithExistingBookings)
                .WithMessage("Booking overlaps with existing bookings at the same location.");
        }

        private async Task<bool> HaveValidCapacity(Booking booking, CancellationToken cancellationToken)
        {
            // Implement logic to check if the number of bookings at the location exceeds its capacity
            if (booking.LocationId == null) return false;
            Location location = await locationRepository.GetByIdAsync(booking.LocationId);

            if (location == null)
            {
                return false;
            }

            // Retrieve the number of existing bookings at the location
            IEnumerable<Booking> existingBookings = await bookingRepository.GetBookingsByLocationIdAsync(booking.LocationId);

            //skip current booking request
            if (booking.Id != null)
                existingBookings.Except(new List<Booking> { booking });

            // Check if the number of bookings exceeds the location's capacity
            return existingBookings.Count() < location.Capacity;
        }

        private async Task<bool> BeWithinTimeWindow(Booking booking, CancellationToken cancellationToken)
        {
            // Implement logic to check if the booking is within the allowed time window for the location
            _ = await locationRepository.GetByIdAsync(booking.LocationId);

            // Get the booking time window for the location from the repository
            TimeWindow timeWindow = await GetBookingTimeWindow(booking.LocationId);

            // Perform the validation based on the time window
            DateTime bookingStartTime = booking.Date;
            DateTime bookingEndTime = booking.Date.Add(booking.Time);

            bool isValid = bookingStartTime.TimeOfDay >= timeWindow.StartTime && bookingEndTime.TimeOfDay <= timeWindow.EndTime;

            return isValid;
        }

        private bool FollowStateTransitionOrder(Booking booking)
        {
            // Implement logic to check if the booking follows the predefined state transition order
            return true;
        }

        private async Task<bool> NotOverlapWithExistingBookings(Booking booking, CancellationToken cancellationToken)
        {
            // Perform the validation based on the time window
            DateTime bookingStartTime = booking.Date;
            DateTime bookingEndTime = booking.Date.Add(booking.Time);

            var overlappingBookings = await bookingRepository.GetOverlappingBookingsAsync(booking.LocationId, bookingStartTime, bookingEndTime);

            //skip current booking request
            if (booking.Id != null)
                overlappingBookings.Except(new List<Booking> { booking });

            return !overlappingBookings.Any();
        }


        public async Task<TimeWindow> GetBookingTimeWindow(Guid locationId)
        {
            Location location = await locationRepository.GetByIdAsync(locationId);

            return new TimeWindow
            {
                StartTime = location.StartDate,
                EndTime = location.EndDate
            };
        }
    }
}
