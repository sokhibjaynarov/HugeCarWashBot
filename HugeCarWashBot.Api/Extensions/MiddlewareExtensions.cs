using HugeCarWashBot.Domain.Entities.Configurations;
using HugeCarWashBot.Service.Interfaces;
using HugeCarWashBot.Service.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Bot.MiddlewareExtensions
{
    public static class MiddlewareExtensions
    {
        public static void AddAppConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppConfigurations>(configuration.GetSection(AppConfigurations.Position));
            services.Configure<BotConfigurations>(configuration.GetSection(BotConfigurations.Position));
        }

        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var botConfigurations = configuration.GetSection(BotConfigurations.Position).Get<BotConfigurations>();
            var appConfigurations = configuration.GetSection(AppConfigurations.Position).Get<AppConfigurations>();

            // Bot services
            services.AddHttpClient("TelegramBotClient").AddTypedClient<ITelegramBotClient>(client => new TelegramBotClient(botConfigurations.AuthToken, client));

            // Webhook services
            services.AddHostedService<IWebhookConfigure>(serviceProvider =>
                new WebhookConfigure
                    (
                        serviceProvider,
                        appConfigurations,
                        botConfigurations
                    )
                );
        }
    }
}
