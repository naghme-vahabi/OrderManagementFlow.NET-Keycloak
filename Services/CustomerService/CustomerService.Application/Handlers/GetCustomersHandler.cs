using CustomerService.Application.DTOs;
using CustomerService.Application.Queries;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomersHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email
            }).ToList();
        }
    }
}
