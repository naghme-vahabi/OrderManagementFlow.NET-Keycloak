using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Guid> AddAsync(Order order);
        Task<List<Order>> GetByCustomerIdAsync(Guid customerId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task UpdateAsync(Order order);
    }
}
