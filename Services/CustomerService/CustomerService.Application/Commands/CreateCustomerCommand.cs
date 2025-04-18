using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Commands
{
    public record CreateCustomerCommand(string FullName, string Email) : IRequest<Guid>;
}
