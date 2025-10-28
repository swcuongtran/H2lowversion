namespace H2.Domain.Entities
{
    public class EnvironmentData
    {
        public Guid Id { get; set; }
        public Guid GreenSensorDataId { get; set; }    // FK
        public double TemperatureC { get; set; }
        public double HumidityPct { get; set; }
        public double PressureHpa { get; set; }
        public double Lux { get; set; }
        public double UvMwCm2 { get; set; }
        public double AltitudeM { get; set; }
    }
}
