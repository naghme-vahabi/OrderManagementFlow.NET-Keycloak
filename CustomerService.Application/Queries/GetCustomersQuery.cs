using CustomerService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Queries
{
    public record GetCustomersQuery() : IRequest<List<CustomerDto>>;
}
