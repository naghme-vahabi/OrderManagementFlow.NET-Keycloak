using MediatR;
using OrderService.Application.DTOs;
using OrderService.Application.Queries;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Handlers
{
    public class GetOrdersByCustomerHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByCustomerHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(request.CustomerId);

            return orders.Select(o => new OrderDto
            {
                OrderId = o.Id,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount
            }).ToList();
        }
    }
}
