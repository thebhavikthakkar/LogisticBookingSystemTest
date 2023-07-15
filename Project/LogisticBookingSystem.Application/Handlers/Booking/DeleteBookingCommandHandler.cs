using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingCommandHandler(ILocationRepository locationRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            LogisticsBookingSystem.Core.Entities.Booking booking = await _bookingRepository.GetByIdAsync(request.Id);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }

            await _bookingRepository.DeleteAsync(request.Id);
        }
    }
}
