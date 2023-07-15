using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Core.Validators;
using LogisticsBookingSystem.Infrastructure.Data.Repositories;
using MediatR;
using System.Linq;

namespace LogisticBookingSystem.Application.Handlers
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IBookingRepository _bookingRepository;

        public UpdateBookingCommandHandler(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            Location location = await _locationRepository.GetByIdAsync(request.LocationId);

            if (location == null)
            {
                throw new NotFoundException("Location not found.");
            }

            Booking booking = new(request.Date, TimeSpan.Parse(request.Time), request.Goods, location.Id, request.Carrier,request.Id);

            await UpdateBooking(booking);
        }

        public async Task UpdateBooking(Booking booking)
        {
            // Validate the general booking details
            var bookingValidator = new BookingValidator(_locationRepository, _bookingRepository);
            var bookingValidationResult = await bookingValidator.ValidateAsync(booking);
            if (!bookingValidationResult.IsValid)
            {
                if (bookingValidationResult.Errors.Where(x => x.ErrorMessage == "Booking overlaps with existing bookings at the same location.").Count() != 1)
                {
                    throw new Exception("Booking is not valid.");
                }
            }

            // Validate the booking cancellation rule if applicable
            if (booking.State == BookingState.Arrived || booking.State == BookingState.Loading)
            {
                var bookingCancellationValidator = new BookingCancellationValidator(_locationRepository, _bookingRepository);
                var bookingCancellationValidationResult = await bookingCancellationValidator.ValidateAsync(booking);
                if (!bookingCancellationValidationResult.IsValid)
                {
                    throw new BookingProcessException("Booking can not be cancelled.");
                }
            }

            // Validate the booking completion rule if applicable
            if (booking.State == BookingState.Unloading)
            {
                var bookingCompletionValidator = new BookingCompletionValidator(_locationRepository, _bookingRepository);
                var bookingCompletionValidationResult = await bookingCompletionValidator.ValidateAsync(booking);
                if (!bookingCompletionValidationResult.IsValid)
                {
                    throw new BookingProcessException("Booking can not be completed.");
                }
            }

            // Proceed with the booking update
            await _bookingRepository.UpdateAsync(booking);
        }
    }
}
