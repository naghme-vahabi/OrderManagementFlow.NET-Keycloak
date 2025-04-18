using OrderService.Domain.Common;

namespace OrderService.Domain.Entities
{
    public class OrderItem: AuditableEntity
    {
        public Guid Id { get; set; }
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }


}
