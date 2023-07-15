using MediatR;

namespace LogisticBookingSystem.Application.Commands
{
    public class DeleteBookingCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
