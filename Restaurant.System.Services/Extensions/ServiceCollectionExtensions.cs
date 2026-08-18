
using Microsoft.Extensions.DependencyInjection;
using Restaurant.System.Data;
using Restaurant.System.Data.Repositories;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Services.Interfaces.Auth;
using Restaurant.System.Services.Interfaces.Maintenance;
using Restaurant.System.Services.Services;
using Restaurant.System.Services.Services.Auth;
using Restaurant.System.Services.Services.Maintenance;

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

            #region Auth
            services.AddScoped<IAuthService, AuthService>();
            #endregion

            #region Maintenance
            services.AddScoped<IStaffMaintenanceService, StaffMaintenanceService>();
            services.AddScoped<IDropdownMaintenanceService, DropdownMaintenanceService>();
            #endregion

            #region General Services
            services.AddScoped<IDropdownSelectionService, DropdownSelectionService>();
            #endregion

            return services;
        }
    }
}