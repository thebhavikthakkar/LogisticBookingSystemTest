using LogisticsBookingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsBookingSystem.Infrastructure.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(b => b.State)
                .HasConversion<int>();
        }
    }
}
