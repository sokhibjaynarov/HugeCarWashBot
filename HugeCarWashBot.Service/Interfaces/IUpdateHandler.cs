using Telegram.Bot.Types;

namespace HugeCarWashBot.Service.Interfaces
{
    public interface IUpdateHandler
    {
        Task EchoAsync(Update update, string authToken);

        Task OnMessageReceived(Update update);

        Task OnCallbacQueryReceived(Update update);

        Task OnUnknownDataReceived(Update update);

        void ExceptionHandler(Exception ex);
    }
}
