using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly CancellationToken _cancellationToken;

        public CustomerRepository(ICustomerDbContext customerDbContext,CancellationToken cancellationToken)
        {
            _customerDbContext = customerDbContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task AddAsync(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync(_cancellationToken);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerDbContext.Customers.ToListAsync(_cancellationToken);
        }
    }
}
