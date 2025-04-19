using MassTransit;
using MediatR;
using OrderService.Application.Commands;
using Shared.Events;

namespace OrderService.Infrastructure.Consumers
{
    public class OrderPaidConsumer : IConsumer<OrderPaidEvent>
    {
        private readonly IMediator _mediator;

        public OrderPaidConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderPaidEvent> context)
        {
            var command = new UpdateOrderStatusCommand
            {
                OrderId = context.Message.OrderId,
                NewStatus = Domain.Enums.OrderStatus.Paid
            };

            await _mediator.Send(command);
        }
    }
}
