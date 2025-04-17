﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs
{
    public class OrderItemDto 
    {
        public string ProductId { get; set; } = ""; 
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
