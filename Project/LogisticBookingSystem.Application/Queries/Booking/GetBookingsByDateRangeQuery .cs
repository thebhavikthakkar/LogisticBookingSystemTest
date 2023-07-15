using LogisticsBookingSystem.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBookingSystem.Application.Queries
{
    public class GetBookingsByDateRangeQuery : IRequest<List<Booking>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetBookingsByDateRangeQuery(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
