
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;

namespace LogisticBookingSystem.Models.Responses
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Goods { get; set; }
        public Location Location { get; set; }
        public string Carrier { get; set; }
        public BookingState State { get; set; }
    }
}
