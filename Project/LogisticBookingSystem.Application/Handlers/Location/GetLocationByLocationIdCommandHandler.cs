using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class GetLocationByLocationIdCommandHandler : IRequestHandler<GetLocationByLocationIDQuery, Location>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationByLocationIdCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(GetLocationByLocationIDQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId);
            return location;
        }
    }
}
