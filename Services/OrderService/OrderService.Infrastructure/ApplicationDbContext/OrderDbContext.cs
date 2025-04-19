using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence.Interceptors;
using System.Reflection;


namespace OrderService.Infrastructure.ApplicationDbContext
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        private readonly AuditableEntityInterceptor _auditableEntityInterceptor;
        public OrderDbContext(DbContextOptions<OrderDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor) : base(options)
        {
            _auditableEntityInterceptor = auditableEntityInterceptor;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
