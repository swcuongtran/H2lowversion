using H2.Application.Interfaces;
using H2.Domain.Entities;
using H2_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace H2.Infrastructure.Persistence.Repositories
{
    public class GreenSensorDataRepository : IGreenSensorDataRepository
    {
        private readonly H2DbContext _context;
        public GreenSensorDataRepository(H2DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(GreenSensorData data)
        {
            await _context.greenSensorDatas.AddAsync(data);
        }

        public async Task<IEnumerable<GreenSensorData>> GetByDeviceAsync(string deviceId, int page = 1, int pageSize = 50)
        {
            return await _context.greenSensorDatas
                .Where(x => x.DeviceId == deviceId)
                .OrderByDescending(x => x.Timestamp)
                .Include(x => x.Environment)
                .Include(x => x.Co2)
                .Include(x => x.Soil)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<GreenSensorData?> GetByIdAsync(Guid id)
        {
            return await _context.greenSensorDatas
                .Include(x => x.Environment)
                .Include(x => x.Co2)
                .Include(x => x.Soil)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
