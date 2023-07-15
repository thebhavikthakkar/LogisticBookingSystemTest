using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LogisticsBookingSystem.Infrastructure.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _dbContext;

        public LocationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Location GetById(Guid id)
        {
            return _dbContext.Locations.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Location> GetAll()
        {
            return _dbContext.Locations.ToList();
        }

        public async Task AddAsync(Location location)
        {
            _ = await _dbContext.Locations.AddAsync(location);
            _ = await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            var existingLocation = await _dbContext.Locations.FindAsync(location.Id);
            if (existingLocation != null)
            {
                _dbContext.Entry(existingLocation).State = EntityState.Detached;
            }

            _dbContext.Entry(location).State = EntityState.Modified;
            _ = await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Location location = GetById(id);
            if (location != null)
            {
                _ = _dbContext.Locations.Remove(location);
                _ = await _dbContext.SaveChangesAsync();
            }
        }

        async Task<Location> ILocationRepository.GetByIdAsync(Guid id)
        {
            return _dbContext.Locations.Include(x => x.Bookings).FirstOrDefault(l => l.Id == id);
        }

        async Task<IEnumerable<Location>> ILocationRepository.GetAllAsync()
        {
            return _dbContext.Locations.ToList();
        }
    }
}
