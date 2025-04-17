using MassTransit;
using MediatR;
using PaymentService.Application.Commands;
using PaymentService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //TODO: Payment Process
            await Task.Delay(500); // simulate payment

            await _publish.Publish(new OrderCreatedEvent
            {
                OrderId = request.OrderId
            });
        }
    }
}
