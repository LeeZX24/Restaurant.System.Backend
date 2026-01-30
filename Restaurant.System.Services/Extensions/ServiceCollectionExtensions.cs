
using Microsoft.Extensions.DependencyInjection;
using Restaurant.System.Data;
using Restaurant.System.Data.Repositories;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Services.Services;

namespace Restaurant.System.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        extension(IServiceCollection services)
        {
            
        }

        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();   

            return services;
        }
    }
}