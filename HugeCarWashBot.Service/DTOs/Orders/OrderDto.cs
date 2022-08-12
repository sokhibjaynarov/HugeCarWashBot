using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HugeCarWashBot.Service.DTOs.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CarNumber { get; set; }
        public string EmployeeName { get; set; }
        public decimal Price { get; set; }
    }
}