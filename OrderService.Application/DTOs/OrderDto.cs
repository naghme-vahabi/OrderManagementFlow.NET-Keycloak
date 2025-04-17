using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
