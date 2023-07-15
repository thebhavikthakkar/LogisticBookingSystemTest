using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Core.Validators;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class MakeBookingCommandHandler : IRequestHandler<MakeBookingCommand>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IBookingRepository _bookingRepository;

        public MakeBookingCommandHandler(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(MakeBookingCommand request, CancellationToken cancellationToken)
        {
            Location location = await _locationRepository.GetByIdAsync(request.LocationId);

            if (location == null)
            {
                throw new NotFoundException("Location not found.");
            }

            Booking booking = new(request.Date, TimeSpan.Parse(request.Time), request.Goods, request.LocationId, request.Carrier);
            booking.Location = location;
            await CreateBooking(booking);
        }

        public async Task CreateBooking(Booking booking)
        {
            // Validate the general booking details
            var bookingValidator = new BookingValidator(_locationRepository, _bookingRepository);
            var bookingValidationResult = await bookingValidator.ValidateAsync(booking);
            if (!bookingValidationResult.IsValid)
            {
                throw new Exception("Booking is not valid.");
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
            await _bookingRepository.AddAsync(booking);
        }

    }
}
