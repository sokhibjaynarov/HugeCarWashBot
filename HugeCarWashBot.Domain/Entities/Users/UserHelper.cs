using HugeCarWashBot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeCarWashBot.Domain.Entities.Users
{
    public class UserHelper
    {
        public Guid Id { get; set; }
        public string? TelegramId { get; set; }
        public Step Step { get; set; }
    }
}
