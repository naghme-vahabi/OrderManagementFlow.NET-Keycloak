using MassTransit;
using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Shared.Events;

namespace OrderService.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publish;

        public CreateOrderHandler(IOrderRepository orderRepository, IPublishEndpoint publish)
        {
            _orderRepository = orderRepository;
            _publish = publish;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                Items = request.Items.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
            order.TotalAmount = order.Items.Sum(x => x.Price * x.Quantity);

            await _orderRepository.AddAsync(order, cancellationToken);

            await _publish.Publish(new OrderCreatedEvent
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount
            });
            return order.Id;
        }
    }
}
