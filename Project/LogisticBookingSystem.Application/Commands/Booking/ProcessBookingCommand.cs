using MediatR;

namespace LogisticBookingSystem.Application.Commands
{
    public class ProcessBookingCommand : IRequest
    {
        public Guid BookingId { get; set; }
    }
}
