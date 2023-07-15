using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class GetBookingsByDateRangeQueryHandler : IRequestHandler<GetBookingsByDateRangeQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByDateRangeQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> Handle(GetBookingsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingsByDateRangeAsync(request.StartDate, request.EndDate);
            return bookings.ToList();
        }
    }
}
