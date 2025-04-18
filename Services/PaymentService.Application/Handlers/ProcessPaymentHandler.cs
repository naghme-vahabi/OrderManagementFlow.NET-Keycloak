using MassTransit;
using MediatR;
using PaymentService.Application.Commands;
using Shared.Events.Shared.Events;

namespace PaymentService.Application.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand>
    {
        private readonly IPublishEndpoint _publish;

        public ProcessPaymentHandler(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public async Task Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            //TODO: simulate payment
            await Task.Delay(500);

            await _publish.Publish(new OrderCreatedEvent
            {
                OrderId = request.OrderId
            });
        }
    }
}
