namespace H2.Domain.Entities
{
    public class SoilData
    {
        public Guid Id { get; set; }
        public Guid GreenSensorDataId { get; set; }    // FK
        public double TemperatureC { get; set; }
        public double HumidityPct { get; set; }
        public double EcUSCm { get; set; }
        public double Ph { get; set; }
        public double N { get; set; }
        public double P { get; set; }
        public double K { get; set; }
        public double SaltMgL { get; set; }
    }
}
