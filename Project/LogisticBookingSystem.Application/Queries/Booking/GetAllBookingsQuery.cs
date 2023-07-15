
using LogisticsBookingSystem.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetAllBookingsQuery : IRequest<List<Booking>>
    {
    }
}
