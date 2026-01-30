
using Microsoft.Extensions.DependencyInjection;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Data.Repositories;

namespace Restaurant.System.Data.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        extension(IServiceCollection services)
        {
            
        }

        public static IServiceCollection AddDataDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IMemberService, MemberService>();
            

            return services;
        }
    }
}