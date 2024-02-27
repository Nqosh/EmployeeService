using EmployeeAPI.Application.Mappings;
using EmployeeAPI.Application.Services;
using EmployeeAPI.Domain.Interfaces;
using EmployeeAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace EmployeeAPI.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmployeeConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabases(configuration)
                .AddServices()
                .AddRepositories()
                .AddMappingProfile()
                .AddLoggerServices();
        }

        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<EmployeeDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });          
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IRepository, EmployeeRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IEmployeeService, EmployeeService>();
        }

        public static IServiceCollection AddLoggerServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<EmployeeRepository>>();
            return services.AddSingleton(typeof(ILogger), logger);
        }

        public static IServiceCollection AddMappingProfile(this IServiceCollection services)
        {
            return services.AddAutoMapper(configuration =>
            {
                configuration
                  .AddProfile<EmployeeManagementSystemMapping>();
            });
        }
    }
}
