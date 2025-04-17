using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    namespace Shared.Events
    {
        public class OrderCreatedEvent
        {
            public Guid OrderId { get; set; }
            public decimal TotalAmount { get; set; } 
        }
    }
}
