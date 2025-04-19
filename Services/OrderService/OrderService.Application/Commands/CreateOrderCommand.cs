using MediatR;
using OrderService.Application.DTOs;

namespace OrderService.Application.Commands
{
    public record CreateOrderCommand(Guid CustomerId, List<OrderItemDto> Items) : IRequest<Guid>;

}
