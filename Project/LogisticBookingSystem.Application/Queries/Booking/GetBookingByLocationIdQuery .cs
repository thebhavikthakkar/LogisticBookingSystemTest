using LogisticsBookingSystem.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetBookingByLocationIdQuery : IRequest<List<Booking>>
    {
        public Guid LocationId
        {
            get; set;
        }

        public GetBookingByLocationIdQuery(Guid locationId)
        {
            LocationId = locationId;
        }   
    }
}
