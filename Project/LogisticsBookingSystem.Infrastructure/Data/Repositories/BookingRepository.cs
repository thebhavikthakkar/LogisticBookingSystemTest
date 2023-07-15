using Azure.Core;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace LogisticsBookingSystem.Infrastructure.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public BookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Booking GetById(Guid id)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Booking> GetBookingsByLocationId(Guid locationId)
        {
            return _dbContext.Bookings.Where(b => b.Location.Id == locationId).ToList();
        }

        public async Task AddAsync(Booking booking)
        {
            _ = await _dbContext.Bookings.AddAsync(booking);
            _ = await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            var existingBooking = await _dbContext.Bookings.FindAsync(booking.Id);
            if (existingBooking != null)
            {
                _dbContext.Entry(existingBooking).State = EntityState.Detached;
            }

            _dbContext.Entry(booking).State = EntityState.Modified;
            _ = await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Booking booking = GetById(id);
            if (booking != null)
            {
                _ = _dbContext.Bookings.Remove(booking);
                _ = await _dbContext.SaveChangesAsync();
            }
        }

        async Task<Booking> IBookingRepository.GetByIdAsync(Guid id)
        {
            return _dbContext.Bookings.Include(x=>x.Location).FirstOrDefault(b => b.Id == id);
        }

        async Task<IEnumerable<Booking>> IBookingRepository.GetBookingsByLocationIdAsync(Guid locationId)
        {
            return _dbContext.Bookings.Where(b => b.Location.Id == locationId).Include(x => x.Location).ToList();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Bookings.Where(b => b.Date >= startDate && b.Date <= endDate).Include(x => x.Location).ToList();
        }

        public async Task<List<Booking>> GetOverlappingBookingsAsync(Guid locationId, DateTime startDateTime, DateTime endDateTime)
        {
            var overlappingBookings = await _dbContext.Bookings.Where(b => b.LocationId == locationId && b.Date>= startDateTime).Include(x => x.Location).ToListAsync();

            var conflictingBookings = overlappingBookings
                .Where(b => (b.Date <= startDateTime && startDateTime < b.Date.Add(b.Time)) ||
                            (b.Date >= startDateTime && b.Date < endDateTime) ||
                            (b.Date >= startDateTime && b.Date < endDateTime && endDateTime < b.Date.Add(b.Time)))
                .ToList();

            return conflictingBookings;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return _dbContext.Bookings.Include(x=>x.Location).ToList();
        }
    }
}
