using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Infrastructure.Consumers;

namespace PaymentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                    });

                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(ctx);
                    });
                });
            });
            return services;
        }
    }
}
