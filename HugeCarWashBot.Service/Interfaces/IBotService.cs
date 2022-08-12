using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HugeCarWashBot.Service.Interfaces
{
    public interface IBotService
    {
        Task TakeContact(Update update);
        Task TakeTextAsync(Update update);
    }
}
