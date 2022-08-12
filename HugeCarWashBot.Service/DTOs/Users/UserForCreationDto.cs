using HugeCarWashBot.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HugeCarWashBot.Service.DTOs.Users
{
    public class UserForCreationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CarNumber { get; set; }
        [Required]
        public string CarModel { get; set; }
        public string? TelegramId { get; set; }
        public Step Step { get; set; }
    }
}
