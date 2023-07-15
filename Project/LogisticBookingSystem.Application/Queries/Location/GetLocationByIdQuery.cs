using LogisticsBookingSystem.Core.Entities;
using MediatR;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetLocationByIdQuery : IRequest<List<Location>>
    {
        public Guid LocationId { get; set; }
    }
}
