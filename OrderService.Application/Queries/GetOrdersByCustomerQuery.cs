using MediatR;
using OrderService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IRequest<List<OrderDto>>;

}
