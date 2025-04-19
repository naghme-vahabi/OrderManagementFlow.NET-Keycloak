using CustomerService.Domain.Common;

namespace CustomerService.Domain.Entities
{
    public class Customer : AuditableEntity
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
