using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Configurations;
using HugeCarWashBot.Domain.Entities.Users;
using HugeCarWashBot.Domain.Enums;
using HugeCarWashBot.Service.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HugeCarWashBot.Service.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly BotConfigurations _botConfiguration;
        private readonly IBotService _botService;

        public UpdateHandler
        (
            ITelegramBotClient botClient,
            IOptions<BotConfigurations> botConfiguration,
            IBotService botService
        )
        {
            _botClient = botClient;
            _botConfiguration = botConfiguration.Value;
            _botService = botService;
        }

        public async Task EchoAsync(Update update, string authToken)
        {
            if (update == null)
                throw new ArgumentException(nameof(Update));

            if (string.IsNullOrWhiteSpace(authToken) || authToken != _botConfiguration.AuthToken)
                throw new InvalidOperationException("Invalid token for bot");


            var handler = update.Type switch
            {
                UpdateType.Message => OnMessageReceived(update),
                UpdateType.CallbackQuery => OnCallbacQueryReceived(update),
                _ => OnUnknownDataReceived(update)
            };

            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        public async Task OnMessageReceived(Update update)
        {
            if (update.Message == null)
                throw new ArgumentException(nameof(Message));

            var telegramId = update.Message.Chat.Id.ToString();

            var message = update.Message;

            if (message.Text != null)
            {
                await _botService.TakeTextAsync(update);
            }
            else if (message.Contact != null)
            {
                await _botService.TakeContact(update);
            }
            else
            {
                await _botClient.SendTextMessageAsync(chatId: telegramId, text: "Please do not send anything!");
            }
            
            //var user = await _unitOfWork.Users.GetAsync(p => p.TelegramId == telegramId);

            //var userHelper = await _unitOfWork.UserHelpers.GetAsync(p => p.TelegramId == telegramId);
            

            //if (user == null)
            //{
            //    if (userHelper == null)
            //    {
            //        await _unitOfWork.UserHelpers.CreateAsync( new UserHelper() { TelegramId = telegramId, Step = Step.PhoneNumber });
            //        await _unitOfWork.SaveChangesAsync();
            //        KeyboardButton button = KeyboardButton.WithRequestContact("Send contact");

            //        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
            //        keyboard.ResizeKeyboard = true;

            //        await _botClient.SendTextMessageAsync(chatId: telegramId, text: "Please send contact", replyMarkup: keyboard);
            //    }
            //    else
            //    {
            //        var phoneNumber = update.Message.Contact.PhoneNumber;
            //        var u = await _unitOfWork.Users.GetAsync(p => p.PhoneNumber == phoneNumber);
            //        if (u != null)
            //        {
            //            await _botClient.SendTextMessageAsync(chatId: telegramId, text: "You are already registered!");
            //        }
            //        else if(u == null)
            //        {
            //            await _botClient.SendTextMessageAsync(chatId: telegramId, text: "Please check your number!");
            //        }
            //        else
            //        {
            //            var uu = new Domain.Entities.Users.User()
            //            {
            //                Id = u.Id,
            //                FirstName = u.FirstName,
            //                LastName = u.LastName,
            //                CarModel = u.CarModel,
            //                CarNumber = u.CarNumber,
            //                State = u.State,
            //                Step = Step.Message,
            //                TelegramId = telegramId,
            //                PhoneNumber = u.PhoneNumber
            //            };

            //            await _unitOfWork.Users.UpdateAsync(uu);
            //            await _unitOfWork.SaveChangesAsync();

            //            await _botClient.SendTextMessageAsync(chatId: telegramId, text: "You succesfully registered!");
            //        }
            //    }
            //}
            //else
            //{
            //    await _botClient.SendTextMessageAsync(chatId: telegramId, text: "Please do not write there!");
            //}

        }

        public async Task OnCallbacQueryReceived(Update update)
        {
            if (update.Message == null || update.CallbackQuery == null)
                throw new ArgumentException($"{nameof(Message)} {nameof(CallbackQuery)}");

            await _botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: "Callback query update received"
            );
        }

        public async Task OnUnknownDataReceived(Update update)
        {
            if (update == null || update.Message == null)
                throw new ArgumentException($"{nameof(Message)} {nameof(Update)}");

            await _botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: "Default update received"
            );
        }

        public void ExceptionHandler(Exception ex)
        {
            var exMessage = ex switch
            {
                ApiRequestException requestException => $"Telegram bot client reqeust exception",
                _ => ex.ToString()
            };

        }
    }
}
