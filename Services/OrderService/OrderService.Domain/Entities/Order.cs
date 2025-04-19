using OrderService.Domain.Common;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
    }


}
