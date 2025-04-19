using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _orderDbContext;

        public OrderRepository(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Guid> AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _orderDbContext.Orders.AddAsync(order, cancellationToken);
            await _orderDbContext.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

        public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _orderDbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _orderDbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
