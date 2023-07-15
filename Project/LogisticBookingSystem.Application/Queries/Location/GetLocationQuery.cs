using LogisticsBookingSystem.Core.Entities;
using MediatR;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetLocationQuery : IRequest<List<Location>>
    {
        public Guid LocationId { get; set; }
    }
}
