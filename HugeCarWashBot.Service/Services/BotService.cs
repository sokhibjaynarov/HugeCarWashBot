using HugeCarWashBot.Data.IRepositories;
using HugeCarWashBot.Domain.Entities.Configurations;
using HugeCarWashBot.Domain.Entities.Users;
using HugeCarWashBot.Domain.Enums;
using HugeCarWashBot.Service.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = HugeCarWashBot.Domain.Entities.Users.User;

namespace HugeCarWashBot.Service.Services
{
    public class BotService : IBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly BotConfigurations _botConfiguration;
        private readonly IUnitOfWork _unitOfWork;

        public BotService
        (
            ITelegramBotClient botClient,
            IOptions<BotConfigurations> botConfiguration,
            IUnitOfWork unitOfWork
        )
        {
            _botClient = botClient;
            _botConfiguration = botConfiguration.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task TakeTextAsync(Update update)
        {
            var message = update.Message.Text;

            var id = update.Message.Chat.Id;

            if(message == null)
                return;
            else if(message == "/start")
            {
                //check user exist
                var existUser = await _unitOfWork.Users.GetAsync(p => p.TelegramId == id.ToString());
                if(existUser != null)
                    await _botClient.SendTextMessageAsync(chatId: id, text: "Please do not send anything!");
                else
                {
                    //check exist user helper
                    var existUserHelper = await _unitOfWork.UserHelpers.GetAsync(p => p.TelegramId == id.ToString());
                    if( existUserHelper != null)
                    {
                        KeyboardButton button = KeyboardButton.WithRequestContact("Send contact");
                        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
                        keyboard.ResizeKeyboard = true;

                        await _botClient.SendTextMessageAsync(chatId: id, text: "Please send contact!", replyMarkup: keyboard);
                    }
                    else
                    {
                        var userHelper = new UserHelper()
                        {
                            TelegramId = id.ToString(),
                            Step = Step.PhoneNumber
                        };

                        await _unitOfWork.UserHelpers.CreateAsync(userHelper);
                        await _unitOfWork.SaveChangesAsync();

                        KeyboardButton button = KeyboardButton.WithRequestContact("Send contact");
                        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
                        keyboard.ResizeKeyboard = true;

                        await _botClient.SendTextMessageAsync(chatId: id, text: "Please send contact!", replyMarkup: keyboard);
                    }
                }
            }
            else
                await _botClient.SendTextMessageAsync(chatId: id, text: "Please do not send anything!");
        }

        public async Task TakeContact(Update update)
        {
            var phoneNumber = update.Message.Contact.PhoneNumber;
            if (!phoneNumber.StartsWith('+'))
                phoneNumber = '+' + phoneNumber;

            var id = update.Message.Chat.Id;

            //check exist user
            var existUser = await _unitOfWork.Users.GetAsync(p => p.PhoneNumber == phoneNumber);
            //check user helper
            var existUserHelper = await _unitOfWork.UserHelpers.GetAsync(p => p.TelegramId == id.ToString());

            if (existUser == null)
                await _botClient.SendTextMessageAsync(chatId: id, text: "Please contact with admin! Your data is not in our database!");
            else if (existUser.Step == Step.Message)
                await _botClient.SendTextMessageAsync(chatId: id, text: "Please do not send anything!");
            else if (existUserHelper == null)
            {
                var userHelper = new UserHelper()
                {
                    TelegramId = id.ToString(),
                    Step = Step.PhoneNumber
                };

                await _unitOfWork.UserHelpers.CreateAsync(userHelper);
                await _unitOfWork.SaveChangesAsync();

                KeyboardButton button = KeyboardButton.WithRequestContact("Send contact");
                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
                keyboard.ResizeKeyboard = true;

                await _botClient.SendTextMessageAsync(chatId: id, text: "Please send contact!");
            }
            else if (existUserHelper.Step == Step.PhoneNumber)
            {

                existUser.TelegramId = id.ToString();
                existUser.Orders = existUser.Orders;
                existUser.Step = Step.Message;
                existUser.Update();

                try
                {

                    await _unitOfWork.Users.UpdateAsync(existUser);
                }
                catch(Exception ex)
                {
                    await _botClient.SendTextMessageAsync(chatId: id, text: ex.Message);
                }
                await _unitOfWork.SaveChangesAsync();

                await _botClient.SendTextMessageAsync(chatId: id, text: "You are successfully registered!", replyMarkup: new ReplyKeyboardRemove());
            }

        }
    }
}
