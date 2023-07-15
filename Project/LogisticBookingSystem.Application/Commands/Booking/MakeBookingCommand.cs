using MediatR;

namespace LogisticBookingSystem.Application.Commands
{
    public class MakeBookingCommand : IRequest
    {
        public Guid LocationId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string? Goods { get; set; }
        public string? Carrier { get; set; }
        public int State { get; set; }


    }
}
