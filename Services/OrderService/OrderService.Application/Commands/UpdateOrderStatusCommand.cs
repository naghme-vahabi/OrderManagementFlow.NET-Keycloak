using MediatR;
using OrderService.Domain.Enums;

namespace OrderService.Application.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
