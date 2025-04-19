using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Guid> AddAsync(Order order, CancellationToken cancellationToken);
        Task<List<Order>> GetByCustomerIdAsync(Guid customerId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task UpdateAsync(Order order, CancellationToken cancellationToken);
    }
}
