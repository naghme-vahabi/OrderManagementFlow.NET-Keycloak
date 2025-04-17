using OrderService.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Interfaces;
using MassTransit;
using OrderService.Infrastructure.Repositories;
using OrderService.Application.Handlers;
using CustomerService.Domain.Interfaces;
using OrderService.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrderDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderDbContext>(provider =>
    provider.GetRequiredService<OrderDbContext>());

builder.Services.AddScoped<IOrderRepository>(provider =>
     new OrderRepository(provider.GetRequiredService<IOrderDbContext>(), CancellationToken.None));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateOrderHandler>());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
