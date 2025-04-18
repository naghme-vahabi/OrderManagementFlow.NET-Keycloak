using MassTransit;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;
using Shared.Events.Shared.Events;

namespace PaymentService.Infrastructure.Consumers
{
    public class PaymentSucceededConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IOrderRepository _repository;

        public PaymentSucceededConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var order = await _repository.GetByIdAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = OrderStatus.Paid;
                await _repository.UpdateAsync(order);
            }
        }
    }
}
