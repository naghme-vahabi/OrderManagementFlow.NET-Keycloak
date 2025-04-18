using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Infrastructure.Persistence;
using CustomerService.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerService.Infrastructure.ApplicationDbContext
{
    public class CustomerDbContext : DbContext, ICustomerDbContext
    {
        private readonly AuditableEntityInterceptor _auditableEntityInterceptor;
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor) : base(options) 
        {
            _auditableEntityInterceptor = auditableEntityInterceptor;
        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
