using H2.Application.Interfaces;
using H2.Domain.Entities;
using H2_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace H2.Infrastructure.Persistence.Repositories
{
    public class GreenImageRepository : IGreenImageRepository
    {
        private readonly H2DbContext _context;

        public GreenImageRepository(H2DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(GreenImage image)
        {
            await _context.greenImages.AddAsync(image);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.greenImages.FindAsync(id);
            if (entity != null)
            {
                _context.greenImages.Remove(entity);
            }
        }

        public async Task<IEnumerable<GreenImage>> GetByDeviceAsync(string deviceId, int page = 1, int pageSize = 20)
        {
            return await _context.greenImages
                .Where(x => x.DeviceId == deviceId)
                .OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<GreenImage?> GetByIdAsync(Guid id)
        {
            return await _context.greenImages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<GreenImage?> GetLatestByDeviceAsync(string deviceId)
        {
            return await _context.greenImages
                .Where(x => x.DeviceId == deviceId)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
        }
    }
}
