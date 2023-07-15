using LogisticBookingSystem.Application.Commands;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class MakeLocationCommandHandler : IRequestHandler<MakeLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public MakeLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task Handle(MakeLocationCommand request, CancellationToken cancellationToken)
        {
            Location location = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                Capacity = request.Capacity
            };

            await _locationRepository.AddAsync(location);
        }
    }
}
