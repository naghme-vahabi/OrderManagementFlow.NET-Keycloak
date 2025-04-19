using MassTransit;
using MassTransit.Mediator;
using PaymentService.Application.Commands;
using Shared.Events;


namespace PaymentService.Infrastructure.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IMediator _mediator;

        public OrderCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var command = new ProcessPaymentCommand(context.Message.OrderId, "CreditCard");

            await _mediator.Send(command);
        }
    }
}
