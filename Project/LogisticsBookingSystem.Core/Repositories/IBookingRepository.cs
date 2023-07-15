using LogisticsBookingSystem.Core.Entities;

namespace LogisticsBookingSystem.Core.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetBookingsByLocationIdAsync(Guid locationId);
        Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetOverlappingBookingsAsync(Guid locationId, DateTime startDateTime, DateTime endDateTime);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid id);
    }
}
