using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _orderDbContext;
        private readonly CancellationToken _cancellationToken;

        public OrderRepository(IOrderDbContext orderDbContext,CancellationToken cancellationToken)
        {
            _orderDbContext = orderDbContext;
            _cancellationToken = cancellationToken;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync(_cancellationToken);
            return order.Id;
        }

        public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _orderDbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public  async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _orderDbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateAsync(Order order)
        {
            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync(_cancellationToken);
        }
    }
}
