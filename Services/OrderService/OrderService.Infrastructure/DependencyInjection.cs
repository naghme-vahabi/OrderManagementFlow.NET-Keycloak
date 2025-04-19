using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.ApplicationDbContext;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.Persistence.Interceptors;
using OrderService.Infrastructure.Repositories;


namespace OrderService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderDbContext>(provider => provider.GetRequiredService<OrderDbContext>());
            services.AddScoped<AuditableEntityInterceptor>();

            services.AddScoped<IOrderRepository, OrderRepository>();


            services.AddHttpContextAccessor();

            // تنظیمات MassTransit و RabbitMQ
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderPaidConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                    });

                    cfg.ReceiveEndpoint("order-paid-queue", e =>
                    {
                        e.ConfigureConsumer<OrderPaidConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(5,TimeSpan.FromSeconds(5)));
                        e.UseDelayedRedelivery(r => r.Interval(2, TimeSpan.FromSeconds(10)));

                    });
                });
            });

            return services;
        }
    }
}
