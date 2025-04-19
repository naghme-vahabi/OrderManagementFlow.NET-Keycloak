using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken);
        Task<List<Customer>> GetAllAsync();
    }
}
