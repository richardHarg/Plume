using Microsoft.Extensions.DependencyInjection;
using RLH.Plume.HighCharts.Services;

namespace RLH.Plume.HighCharts.ASPNETCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlumeHighCharts(this IServiceCollection services)
        {
            services.AddScoped<IHighChartService,HighChartService>();
            return services;
        }

       
    }
}
