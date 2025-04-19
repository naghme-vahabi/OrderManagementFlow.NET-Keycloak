using MassTransit;
using MediatR;
using PaymentService.Application.Commands;
using Shared.Events;

namespace PaymentService.Application.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, bool>
    {
        private readonly IPublishEndpoint _publish;

        public ProcessPaymentHandler(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public async Task<bool> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            //TODO: simulate payment

            await Task.Delay(500, cancellationToken);

            await _publish.Publish(new OrderPaidEvent
            {
                OrderId = request.OrderId
            }, cancellationToken);

            return true;
        }
    }
}
