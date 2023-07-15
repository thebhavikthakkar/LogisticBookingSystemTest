using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Exceptions;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            Location location = await _locationRepository.GetByIdAsync(request.Id);

            if(location == null)
            {
                throw new NotFoundException("Location not found.");
            }   

            var updatedLocation = new Location
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                Capacity = request.Capacity,
                EndDate = request.EndDate,
                StartDate = request.StartDate
            };

            await _locationRepository.UpdateAsync(updatedLocation);
        }
    }
}
