using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RLH.Plume.Configuration;
using RLH.Plume.Context;
using RLH.Plume.Core.Entities;
using RLH.Plume.Core.Services;
using RLH.Plume.Services;

namespace RLH.Plume.ASPNETCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlume(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IMeasurementService,MeasurementService>();
            services.AddScoped<IReportService, ReportService>();


            var configSection = configuration.GetSection("PlumeConfig");
            if (configSection.Exists() == false)
            {
                throw new NullReferenceException("Missing 'PlumeConfig' section in appSettings.json file");
            }

            PlumeConfig config = configSection.Get<PlumeConfig>();

            services.AddDbContext<PlumeContext>(contextOptions =>
            {
                contextOptions.UseSqlServer(config.ConnectionString, serverOptions =>
                {
                    serverOptions.MigrationsAssembly("PLH.Plume");
                });

            });

            return services;
        }

       
    }
}
