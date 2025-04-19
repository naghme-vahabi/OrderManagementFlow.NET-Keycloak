using MediatR;

namespace PaymentService.Application.Commands
{
    public class ProcessPaymentCommand : IRequest<bool>
    {
        public ProcessPaymentCommand(Guid orderId, string paymentMethod)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
        }

        public Guid OrderId { get; }
        public string PaymentMethod { get; }
    }
}
