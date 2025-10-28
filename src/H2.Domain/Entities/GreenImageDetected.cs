namespace H2.Domain.Entities
{
    public class GreenImageDetected
    {
        public Guid Id { get; set; }
        public Guid OriginalImageId { get; set; }
        public string DeviceId { get; set; } = null!;
        public string DetectedImageUrl { get; set; } = null!;
        public string Labels { get; set; } = string.Empty;
        public DateTime ProcessedAt { get; set; }
    }
}
