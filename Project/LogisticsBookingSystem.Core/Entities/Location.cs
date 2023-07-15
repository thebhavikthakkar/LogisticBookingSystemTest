using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LogisticsBookingSystem.Core.Entities
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }

        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; }

        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }


        public Location()
        {
            Id = Guid.NewGuid();
            Bookings = new List<Booking>();
        }


    }
}
