using MediatR;

namespace CustomerService.Application.Commands
{
    public record CreateCustomerCommand(string FullName, string Email) : IRequest<Guid>;
}
