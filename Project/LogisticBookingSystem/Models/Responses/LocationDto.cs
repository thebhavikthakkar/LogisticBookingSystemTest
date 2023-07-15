using LogisticsBookingSystem.Core.Entities;

namespace LogisticBookingSystem.Models.Responses
{
    public class LocationDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public IEnumerable<Booking>? Bookings { get; set; }
    }
}
