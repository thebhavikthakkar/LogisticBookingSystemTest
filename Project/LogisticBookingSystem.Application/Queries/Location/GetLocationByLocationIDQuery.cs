using LogisticsBookingSystem.Core.Entities;
using MediatR;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetLocationByLocationIDQuery : IRequest<Location>
    {
        public Guid LocationId { get; set; }
    }
}
