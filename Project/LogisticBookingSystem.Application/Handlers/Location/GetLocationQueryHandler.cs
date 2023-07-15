using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery ,List<Location>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<Location>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllAsync();
            return locations.ToList();
        }
    }
}
