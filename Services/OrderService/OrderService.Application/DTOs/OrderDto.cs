namespace OrderService.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
