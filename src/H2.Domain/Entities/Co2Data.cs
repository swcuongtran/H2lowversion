namespace H2.Domain.Entities
{
    public class Co2Data
    {
        public Guid Id { get; set; }
        public Guid GreenSensorDataId { get; set; }    // FK
        public double Ppm { get; set; }
    }
}
