using System.ComponentModel.DataAnnotations;

namespace HugeCarWashBot.Service.DTOs.Orders
{
    public class OrderForCreationDto
    {
        [Required]
        public string CarNnumber { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
