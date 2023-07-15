using MediatR;

namespace LogisticBookingSystem.Application.Commands
{
    public class DeleteLocationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
