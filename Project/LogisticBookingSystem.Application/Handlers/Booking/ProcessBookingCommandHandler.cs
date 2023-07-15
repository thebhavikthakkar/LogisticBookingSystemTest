using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class ProcessBookingCommandHandler : IRequestHandler<ProcessBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILocationRepository _locationRepository;

        public ProcessBookingCommandHandler(IBookingRepository bookingRepository, ILocationRepository locationRepository)
        {
            _bookingRepository = bookingRepository;
            _locationRepository = locationRepository;
        }

        public async Task Handle(ProcessBookingCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the booking from the repository
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            // Retrieve the location from the repository
            var locationId = booking.Location.Id == null ? booking.LocationId : booking.Location.Id;
            var location = await _locationRepository.GetByIdAsync(locationId);

            if (location == null)
            {
                throw new NotFoundException("Location not found.");
            }

            // Update the booking state based on the location status
            BookingState newBookingState = GetNextBookingStatus(booking.State);
            booking.State = newBookingState;

            // Save the changes to the booking
            await _bookingRepository.UpdateAsync(booking);
        }

        public BookingState GetNextBookingStatus(BookingState currentStatus)
        {
            switch (currentStatus)
            {
                case BookingState.Arrived:
                    return BookingState.Loading;
                case BookingState.Loading:
                    return BookingState.Unloading;
                case BookingState.Unloading:
                    return BookingState.Completed;
                default:
                    return currentStatus;
            }
        }
    }
}
