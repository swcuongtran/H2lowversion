using H2.Domain.Entities;

namespace H2.Application.Interfaces
{
    public interface IGreenImageRepository
    {
        Task<GreenImage?> GetByIdAsync(Guid id);
        Task<IEnumerable<GreenImage>> GetByDeviceAsync(string deviceId, int page = 1, int pageSize = 20);
        Task AddAsync(GreenImage image);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
        Task<GreenImage?> GetLatestByDeviceAsync(string deviceId);
    }
}
