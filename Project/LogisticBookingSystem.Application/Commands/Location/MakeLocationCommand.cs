using MediatR;

namespace LogisticBookingSystem.Application.Commands
{
    public class MakeLocationCommand : IRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public TimeSpan? StartDate { get; set; }
        public TimeSpan? EndDate { get; set; }

    }
}
