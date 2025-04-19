using CustomerService.Application.DTOs;
using MediatR;

namespace CustomerService.Application.Queries
{
    public record GetCustomersQuery() : IRequest<List<CustomerDto>>;
}
