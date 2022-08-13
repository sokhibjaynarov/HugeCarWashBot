using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace HugeCarWashBot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITelegramBotClient _botClient;
        public MessageController(IUnitOfWork unitOfWork, ITelegramBotClient botClient)
        {
            _unitOfWork = unitOfWork;
            _botClient = botClient;
        }

        [HttpPost("{id}")]
        public async Task<string> Post([FromRoute] string id, string message)
        {
            var result = await _unitOfWork.Users.GetAsync(p => p.TelegramId == id);

            string success = null;

            if (result == null)
            {
                return "false";
            }

            try
            {
                await _botClient.SendTextMessageAsync(chatId: id, text: message);
            }
            catch(Exception ex)
            {
                success = "false => " + ex;
            }
            return success;
        }
    }
}
