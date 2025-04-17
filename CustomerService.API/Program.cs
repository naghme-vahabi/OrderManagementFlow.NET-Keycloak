using CustomerService.Application.Handlers;
using CustomerService.Application.Interfaces;
using CustomerService.Domain.Interfaces;
using CustomerService.Infrastructure.ApplicationDbContext;
using CustomerService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerDbContext>(provider=>
    provider.GetRequiredService<CustomerDbContext>());     

builder.Services.AddScoped<ICustomerRepository>(provider=>
     new CustomerRepository(provider.GetRequiredService<ICustomerDbContext>(),CancellationToken.None));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCustomerHandler>());


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
