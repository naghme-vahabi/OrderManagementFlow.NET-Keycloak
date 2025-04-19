namespace CustomerService.Application.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
