using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
