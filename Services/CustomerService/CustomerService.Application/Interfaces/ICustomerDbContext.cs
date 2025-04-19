using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Application.Interfaces
{
    public interface ICustomerDbContext
    {
        DbSet<Customer> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
