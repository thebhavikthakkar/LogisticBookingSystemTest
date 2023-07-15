using LogisticsBookingSystem.Core.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsBookingSystem.Core.Entities
{
    [Table("Booking")]
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Goods { get; set; }
        public Guid LocationId { get; set; }

        public string Carrier { get; set; }
        public BookingState State { get; set; }

        public Location Location { get; set; }
        public Booking()
        {
            Id = Guid.NewGuid();
        }

        public Booking(DateTime date, TimeSpan time, string goods, Guid locationId, string carrier,Guid bookingId)
        {
            Date = date;
            Time = time;
            Goods = goods;
            Carrier = carrier;
            LocationId = locationId;
            Id = bookingId;
        }
        public Booking(DateTime date, TimeSpan time, string goods, Guid locationId, string carrier)
        {
            Date = date;
            Time = time;
            Goods = goods;
            Carrier = carrier;
            LocationId = locationId;
        }
    }
}
