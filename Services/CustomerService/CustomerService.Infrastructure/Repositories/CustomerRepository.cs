using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDbContext _customerDbContext;

        public CustomerRepository(ICustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _customerDbContext.Customers.AddAsync(customer, cancellationToken);
            await _customerDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerDbContext.Customers.ToListAsync();
        }
    }
}
