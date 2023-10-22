#nullable disable

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Auxiliary;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandTests : IRunnable
{
    private readonly BotConfig _botConfig;
    private readonly ILogger<CommandTests> _logger;
    private readonly ITestService _testService;

    public CommandTests(BotConfig botConfig, ITestService testService, ILogger<CommandTests> logger) =>
        (_botConfig, _testService, _logger) = (botConfig, testService, logger);

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.CallbackQuery.Message.Chat.Id;
        long userId = update.CallbackQuery.From.Id;
        int messageId = update.CallbackQuery.Message.MessageId;

        _logger.LogDebug($"Сообщение от: userId={userId}, chatId={chatId}, messageId={messageId}");

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
                replyMarkup: GenerateKeyboard( _testService.GetAvailableTests() ),
                text: _botConfig.TextMessages["tests"],
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

        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _botConfig.TextMessages["tests"],
                replyMarkup: GenerateKeyboard( _testService.GetAvailableTests() ),
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

    private ReplyKeyboardMarkup GenerateKeyboard(IEnumerable<FunctionalTest> tests)
    {
        // + 1 для клавиши "В главное меню"
        List<List<KeyboardButton>> keyboard = new List<List<KeyboardButton>>(tests.Count() + 1);
    
        foreach ( var test in tests )
        {
            keyboard.Add( new List<KeyboardButton>() { Helper.CreateButton( "📄 " + test.Title ) } );
        }

        keyboard.Add( new List<KeyboardButton>() { Helper.CreateButton("🏠 В главное меню") } );

        return new ReplyKeyboardMarkup(keyboard)
        {
            ResizeKeyboard = true
        };
    }
}