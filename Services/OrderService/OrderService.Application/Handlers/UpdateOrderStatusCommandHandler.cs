using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new Exception($"سفارش با شناسه {request.OrderId} یافت نشد.");

            if (!IsValidStatusTransition(order.Status, request.NewStatus))
            {
                throw new InvalidOperationException($"تغییر وضعیت از {order.Status} به {request.NewStatus} مجاز نیست.");
            }

            order.Status = request.NewStatus;
            await _orderRepository.UpdateAsync(order, cancellationToken);

            return true;
        }
        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {

            bool isValid = (currentStatus, newStatus) switch
            {
                (OrderStatus.Pending, OrderStatus.Paid) => true,
                (OrderStatus.Paid, OrderStatus.Shipped) => true,
                _ => false
            };
            return isValid;
        }
    }
}
