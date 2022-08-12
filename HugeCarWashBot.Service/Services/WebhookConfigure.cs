using HugeCarWashBot.Domain.Entities.Configurations;
using HugeCarWashBot.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace HugeCarWashBot.Service.Services
{
    public class WebhookConfigure : IWebhookConfigure
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppConfigurations _appConfigurations;
        private readonly BotConfigurations _botCofngiurations;

        public WebhookConfigure
        (
            IServiceProvider serviceProvider,
            AppConfigurations appConfigurations,
            BotConfigurations botConfiguration
        )
        {
            _serviceProvider = serviceProvider;
            _appConfigurations = appConfigurations;
            _botCofngiurations = botConfiguration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = _serviceProvider.GetService(typeof(ITelegramBotClient)) as ITelegramBotClient
                ?? throw new InvalidOperationException($"Service request failed for service {nameof(ITelegramBotClient)}");

            var webhookAddress = $"{_appConfigurations.ApiHostDomain}/bot/{_botCofngiurations.AuthToken}";

            await botClient.SetWebhookAsync
            (
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken
            );

            await botClient.SendTextMessageAsync
            (
                chatId: "1330988805",
                text: "Bot web hook have been set"
            );
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = _serviceProvider.GetService(typeof(ITelegramBotClient)) as ITelegramBotClient
                ?? throw new InvalidOperationException($"Service request failed for service {nameof(ITelegramBotClient)}");

            await botClient.DeleteWebhookAsync(cancellationToken);
            await botClient.SendTextMessageAsync
            (
                chatId: "1330988805",
                text: "Bot web hook have been removed"
            );
        }
    }
}
