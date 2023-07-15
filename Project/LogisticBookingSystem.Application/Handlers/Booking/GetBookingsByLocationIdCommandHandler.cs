using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using MediatR;

namespace LogisticBookingSystem.Application.Handlers
{
    public class GetBookingsByLocationIdCommandHandler : IRequestHandler<GetBookingByLocationIdQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByLocationIdCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> Handle(GetBookingByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingsByLocationIdAsync(request.LocationId);
            return bookings.ToList();
        }
    }
}
