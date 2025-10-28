using H2.Domain.Enums;

namespace H2.Domain.Entities
{
    public class GreenImage
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime Timestamp { get; set; }
    }
}
