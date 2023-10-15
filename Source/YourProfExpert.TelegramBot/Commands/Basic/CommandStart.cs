#nullable disable

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.Auxiliary;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandStart : IRunnable
{
    private readonly BotConfig _botConfig;

    public CommandStart(BotConfig botConfig) =>
        _botConfig = botConfig;

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.CallbackQuery.Message.Chat.Id;
        int messageId = update.CallbackQuery.Message.MessageId;
        var user = update.CallbackQuery.From;

        try
        {
            await botClient.DeleteMessageAsync
            (
                chatId: chatId,
                messageId: messageId
            );
        }
        catch (ApiRequestException ex)
        {
            
        }
        catch (Exception ex)
        {
            
        }

        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: string.Format(_botConfig.TextMessages["main"], Helper.GetFullNameUser(update.Message.From)),
                replyMarkup: _botConfig.ReplyKeyboardMarkups["main"],
                parseMode: ParseMode.Html
            );
        }
        catch (ApiRequestException ex)
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }

    public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.Message.Chat.Id;

        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: string.Format(_botConfig.TextMessages["main"], Helper.GetFullNameUser(update.Message.From)),
                replyMarkup: _botConfig.ReplyKeyboardMarkups["main"],
                parseMode: ParseMode.Html
            );
        }
        catch (ApiRequestException ex)
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }
}