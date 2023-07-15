using LogisticsBookingSystem.Core.Enum;

namespace LogisticBookingSystem.Models.Requests
{
    public class BookingModel
    {
        public DateTime Date { get; set; }
        public string? Goods { get; set; }
        public Guid LocationId { get; set; }
        public string? Carrier { get; set; }
        public int State { get; set; }
        public string Duration { get; set; }
        public string Time { get; set; }
    }
}
