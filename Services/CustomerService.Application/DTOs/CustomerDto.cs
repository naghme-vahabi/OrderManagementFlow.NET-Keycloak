using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
