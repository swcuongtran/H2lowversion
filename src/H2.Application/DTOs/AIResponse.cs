namespace H2.Application.DTOs
{
    public class DetectionResult
    {
        public string Label { get; set; } = string.Empty;
        public double Confidence { get; set; }
        public List<double> BoundingBox { get; set; } = new();
    }

    public class AIResponse
    {
        public Guid ImageId { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public string ModelVersion { get; set; } = string.Empty;
        public string DetectedImageUrl { get; set; } = string.Empty;
        public List<DetectionResult> Detections { get; set; } = new();
    }
}
