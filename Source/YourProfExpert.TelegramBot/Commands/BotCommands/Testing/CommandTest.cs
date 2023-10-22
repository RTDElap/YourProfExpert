#nullable disable

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.Auxiliary;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandTest : IRunnable
{
    private readonly ILogger<CommandTest> _logger;
    private readonly ITestService _testService;
    private readonly IExecutorTestService _executorTestService;

    public CommandTest(ITestService testService, IExecutorTestService executorTestService, ILogger<CommandTest> logger)
    {
        _logger = logger;
        _testService = testService;
        _executorTestService = executorTestService;
    }

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.CallbackQuery.Message.Chat.Id;
        long userId = update.CallbackQuery.From.Id;
        int messageId = update.CallbackQuery.Message.MessageId;

        // args.Skip(1) для пропуска самой комадны (/test)
        string testTitle = string.Join(' ', args.Skip(1));

        if ( _executorTestService.IsUserStartTest(userId) )
        {
            _executorTestService.CloseTest(userId);
        }

        FunctionalTest? test = _testService.GetTestOrDefault(testTitle);

        if ( test is null )
        {
            await SendErrorWithTestAsync(botClient, chatId);

            return;
        }

        _executorTestService.StartTest(userId, test);

        await AddTestAnswersToMessageAsync(botClient, userId, chatId, messageId, test);
    }

    public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.Message.Chat.Id;
        long userId = update.Message.From.Id;

        _logger.LogDebug($"Сообщение от: userId={userId}, chatId={chatId}");

        // args.Skip(1) для пропуска эмодзи (см. CommandTests.cs GenerateKeyboard)
        string testTitle = string.Join(' ', args.Skip(1));

        if ( _testService.GetUserPassedTestOrDefault(userId, testTitle) is not null )
        {
            await Helper.SendMessageWithDialog
            (
                botClient,
                chatId: chatId,
                message: $"Вы прошли этот тест!\n\nХотите начать заново?",
                commandOnYes: $"/test {testTitle}",
                commandOnNo: "/tests"
            );
        }

        if ( _executorTestService.IsUserStartTest(userId) )
        {
            await Helper.SendMessageWithDialog
            (
                botClient,
                chatId: chatId,
                message: $"Вы проходите <b>\"{_executorTestService.GetTestTitleOfUser(userId)}\"</b>\n\nХотите начать этот тест?",
                commandOnYes: $"/test {testTitle}",
                commandOnNo: "/tests"
            );

            return;
        }

        FunctionalTest? test = _testService.GetTestOrDefault(testTitle);

        if ( test is null )
        {
            await SendErrorWithTestAsync(botClient, chatId);

            return;
        }

        _executorTestService.StartTest( userId, test );

        await SendTestAnswers(botClient, userId, chatId, test);
    }

    private async Task SendTestAnswers(ITelegramBotClient botClient, long userId, long chatId, FunctionalTest test)
    {
        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: $"<b>{test.Title}</b>\n\n{test.Description}",
                replyMarkup: Helper.CreateInlineKeyboardWithAnswers( _executorTestService.GetAnswers(userId) ),
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

    private async Task AddTestAnswersToMessageAsync(ITelegramBotClient botClient, long userId, long chatId , int messageId, FunctionalTest test)
    {
        try
        {
            await botClient.EditMessageTextAsync
            (
                chatId: chatId,
                messageId: messageId,
                text: $"<b>{test.Title}</b>\n\n{test.Description}",
                replyMarkup: Helper.CreateInlineKeyboardWithAnswers( _executorTestService.GetAnswers(userId) ),
                parseMode: ParseMode.Html
            );
        }
        catch( ApiRequestException ex )
        {
            _logger.LogError($"Не удалось изменить сообщение: {ex.Message}");
        }
        catch ( Exception ex )
        {
            _logger.LogCritical($"Не удалось изменить сообщение: {ex.Message}");
        }
    }

    

    private async Task SendErrorWithTestAsync(ITelegramBotClient botClient, long chatId)
    {
        try
        {
            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: "Тест не найден",
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