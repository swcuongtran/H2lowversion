using H2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2_Infrastructure.Persistence
{
    public class H2DbContext : DbContext
    {
        public H2DbContext(DbContextOptions<H2DbContext> options) : base(options)
        {
        }

        public DbSet<SensorData> sensorDatas { get; set; } = null!;
        public DbSet<ThermalImage> thermalImages { get; set; } = null!;
        
        public DbSet<GreenSensorData> greenSensorDatas { get; set; } = null!;
        public DbSet<Co2Data> co2Datas { get; set; } = null!;
        public DbSet<SoilData> soilDatas { get; set; } = null!;
        public DbSet<EnvironmentData> environmentDatas { get; set; } = null!;
        public DbSet<GreenImage> greenImages { get; set; } = null!;
        public DbSet<GreenImageDetected> GreenImageDetected { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.HydrogenPpm).IsRequired();
                entity.Property(e => e.AlertLevel)
                    .HasConversion<string>()
                    .IsRequired();
            });

            modelBuilder.Entity<ThermalImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.ImageType)
                    .HasConversion<string>()            
                    .IsRequired();
            });
            modelBuilder.Entity<GreenSensorData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();

                entity.HasOne(e => e.Environment)
                      .WithOne()
                      .HasForeignKey<EnvironmentData>(x => x.GreenSensorDataId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Co2)
                      .WithOne()
                      .HasForeignKey<Co2Data>(x => x.GreenSensorDataId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Soil)
                      .WithOne()
                      .HasForeignKey<SoilData>(x => x.GreenSensorDataId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EnvironmentData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TemperatureC).HasPrecision(6, 2);
                entity.Property(e => e.HumidityPct).HasPrecision(6, 2);
            });
            modelBuilder.Entity<Co2Data>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ppm).IsRequired();
            });
            modelBuilder.Entity<SoilData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ph).HasPrecision(4, 2);
            });
            modelBuilder.Entity<GreenImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });
            modelBuilder.Entity<GreenImageDetected>(entity =>
            {
                entity.ToTable("GreenImageDetected");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .IsRequired();

                entity.Property(e => e.OriginalImageId)
                      .IsRequired();

                entity.Property(e => e.DeviceId)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.DetectedImageUrl)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.Labels)
                      .HasMaxLength(500);

                entity.Property(e => e.ProcessedAt)
                      .IsRequired();
            });
        }
        
    }
}
