using H2.Domain.Entities;

namespace H2.Application.Interfaces
{
    public interface IGreenImageDetectedRepository
    {
        Task AddAsync(GreenImageDetected entity);
        Task<IEnumerable<GreenImageDetected>> GetByDeviceAsync(string deviceId, int page, int pageSize);
        Task<GreenImageDetected?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
