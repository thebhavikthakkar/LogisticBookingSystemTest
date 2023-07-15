using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            LogisticsBookingSystem.Core.Entities.Location location = await _locationRepository.GetByIdAsync(request.Id);

            if (location == null)
            {
                throw new NotFoundException("Location not found.");
            }

            await _locationRepository.DeleteAsync(request.Id);
        }
    }
}
