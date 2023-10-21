#nullable disable

using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CommandStart> _logger;

    public CommandStart(BotConfig botConfig, ILogger<CommandStart> logger) =>
        (_botConfig, _logger) = (botConfig, logger);

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.CallbackQuery.Message.Chat.Id;
        long userId = update.CallbackQuery.Message.From.Id;
        
        int messageId = update.CallbackQuery.Message.MessageId;

        _logger.LogDebug($"Сообщение от: userId={userId}, chatId={chatId}, messageId={messageId}");

        string formattedString = 
            string.Format
            ( 
                _botConfig.TextMessages["main"], Helper.GetFullNameUser(update.CallbackQuery.From)
            );
        
        try
        {
            await botClient.DeleteMessageAsync
            (
                chatId: chatId,
                messageId: messageId
            );
        }
        catch( ApiRequestException ex )
        {
            _logger.LogError($"Не удалось удалить сообщение: {ex.Message}");
        }
        catch ( Exception ex )
        {
            _logger.LogCritical($"Не удалось удалить сообщение: {ex.Message}");
        }

        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: formattedString,
                replyMarkup: _botConfig.ReplyMarkups["main"],
                parseMode: ParseMode.Html
            );
        }
        catch( ApiRequestException ex )
        {
            _logger.LogError($"Не удалось отправить сообщение: {ex.Message}");
        }
        catch ( Exception ex )
        {
            _logger.LogCritical($"Не удалось отправить сообщение: {ex.Message}");
        }
    }

    public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.Message.Chat.Id;
        long userId = update.Message.From.Id;

        _logger.LogDebug($"Сообщение от: userId={userId}, chatId={chatId}");
        
        string formattedString = 
            string.Format
            ( 
                _botConfig.TextMessages["main"], Helper.GetFullNameUser(update.Message.From)
            );

        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: formattedString,
                replyMarkup: _botConfig.ReplyMarkups["main"],
                parseMode: ParseMode.Html
            );
        }
        catch( ApiRequestException ex )
        {
            _logger.LogError($"Не удалось отправить сообщение: {ex.Message}");
        }
        catch ( Exception ex )
        {
            _logger.LogCritical($"Не удалось отправить сообщение: {ex.Message}");
        }
    }
}