using HugeCarWashBot.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HugeCarWashBotBot.Api.Controllers
{
    [ApiController]
    [Route("bot/{token}")]
    public class BotHookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post
        (
            [FromServices] IUpdateHandler handlerService,
            [FromRoute] string token,
            [FromBody] Update update
        )
        {
            await handlerService.EchoAsync(update, token);
            return Ok();
        }
    }
}