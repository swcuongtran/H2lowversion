using H2.Application.Interfaces;
using H2.Domain.Entities;
using H2_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace H2.Infrastructure.Persistence.Repositories
{
    public class GreenImageDetectedRepository : IGreenImageDetectedRepository
    {

        private readonly H2DbContext _context;
        public GreenImageDetectedRepository(H2DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GreenImageDetected entity)
        {
            await _context.GreenImageDetected.AddAsync(entity);
        }

        public async Task<IEnumerable<GreenImageDetected>> GetByDeviceAsync(string deviceId, int page, int pageSize)
        {
            return await _context.GreenImageDetected
                .Where(x => x.DeviceId == deviceId)
                .OrderByDescending(x => x.ProcessedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<GreenImageDetected?> GetByIdAsync(Guid id)
        {
            return await _context.GreenImageDetected
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
