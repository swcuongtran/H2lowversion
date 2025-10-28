namespace H2.Domain.Entities
{
    public class GreenSensorData
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public EnvironmentData Environment { get; set; } = new();
        public Co2Data Co2 { get; set; } = new();
        public SoilData Soil { get; set; } = new();
    }
}
