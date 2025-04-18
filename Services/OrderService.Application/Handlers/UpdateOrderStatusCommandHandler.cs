using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commands;
using OrderService.Application.Interfaces;
using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderDbContext _dbcontext;

        public UpdateOrderStatusCommandHandler(IOrderDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbcontext.Orders
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new Exception($"سفارش با شناسه {request.OrderId} یافت نشد.");
            }

            if (!IsValidStatusTransition(order.Status, request.NewStatus))
            {
                throw new InvalidOperationException($"تغییر وضعیت از {order.Status} به {request.NewStatus} مجاز نیست.");
            }

            order.Status = request.NewStatus;
            await _dbcontext.SaveChangesAsync(cancellationToken);

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
