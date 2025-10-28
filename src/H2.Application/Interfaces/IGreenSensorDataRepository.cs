using H2.Domain.Entities;

namespace H2.Application.Interfaces
{
    public interface IGreenSensorDataRepository
    {
        Task<GreenSensorData?> GetByIdAsync(Guid id);
        Task<IEnumerable<GreenSensorData>> GetByDeviceAsync(string deviceId, int page = 1, int pageSize = 50);
        Task AddAsync(GreenSensorData data);
        Task SaveChangesAsync();
    }
}
