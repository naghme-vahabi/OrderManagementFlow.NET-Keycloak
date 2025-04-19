using CustomerService.Application.Interfaces;
using CustomerService.Domain.Interfaces;
using CustomerService.Infrastructure.ApplicationDbContext;
using CustomerService.Infrastructure.Persistence.Interceptors;
using CustomerService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CustomerService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICustomerDbContext>(provider => provider.GetRequiredService<CustomerDbContext>());
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<AuditableEntityInterceptor>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
