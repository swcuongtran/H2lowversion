using H2_Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using H2_Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using H2.Application.Interfaces;
using H2_Infrastructure.Services;
using Amazon.S3;
using H2_Infrastructure.Persistence.Repositories;
using H2.Infrastructure.Persistence.Repositories;

namespace H2_Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<H2DbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("H2Connection")));
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAwsS3Service, AwsS3Service>();
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var awsOptions = configuration.GetSection("AwsS3");

                return new AmazonS3Client(
                    awsOptions["AccessKey"],
                    awsOptions["SecretKey"],
                    Amazon.RegionEndpoint.GetBySystemName(awsOptions["Region"]));
            });
            services.AddScoped<IThermalImageRepository, ThermalImageRepository>();
            services.AddScoped<ISensorDataRepository, SensorDataRepository>();
            services.AddScoped<IGreenSensorDataRepository, GreenSensorDataRepository>();
            services.AddScoped<IGreenImageRepository, GreenImageRepository>();
            services.AddScoped<IGreenImageDetectedRepository, GreenImageDetectedRepository>();


            return services;
        }
    }
}
