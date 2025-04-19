using CustomerService.Application.Commands;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email
            };

            await _customerRepository.AddAsync(customer, cancellationToken);

            return customer.Id;
        }
    }
}
