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

public class CommandSelectAnswer : IRunnable
{
    private readonly ILogger<CommandSelectAnswer> _logger;
    private readonly IExecutorTestService _executorTestService;

    public CommandSelectAnswer(IExecutorTestService executorTestService, ILogger<CommandSelectAnswer> logger)
    {
        _logger = logger;
        _executorTestService = executorTestService;
    }

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.CallbackQuery.Message.Chat.Id;
        long userId = update.CallbackQuery.From.Id;
        int messageId = update.CallbackQuery.Message.MessageId;

        if ( args.Length < 2 )
        {
            _logger.LogError($"Неверный формат входных данных: {string.Join(' ', args)}");

            return;
        }

        if ( ! int.TryParse( args[1], out int selectedVariant ) )
        {
            _logger.LogError($"Не удалось распарсить индекс ответа: {args[1]}");

            return;
        }

        if ( ! _executorTestService.IsUserStartTest(userId) )
        {
            _logger.LogError($"Пользователь закончил тест");

            return;
        }

        _executorTestService.TrySelectAnswer( userId, selectedVariant);

        if ( _executorTestService.IsEnd(userId) )
        {
            if ( ! await _executorTestService.SaveResultAsync(userId, cancellationToken))
            {
                await SendErrorWithDatabaseSavingAsync(botClient, chatId, messageId);
            }
            else
            {
                await AddResultToMessageAsync(botClient, chatId, messageId);
            }
            
            _executorTestService.CloseTest(userId);

            return;
        }

        await UpdateAnswersToMessageAsync(botClient, userId, chatId, messageId);
    }

    public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        return Task.CompletedTask;
    }

    private async Task UpdateAnswersToMessageAsync(ITelegramBotClient botClient, long userId, long chatId, int messageId)
    {
        try
        {
            await botClient.EditMessageTextAsync
            (
                chatId: chatId,
                messageId: messageId,
                text: $"{_executorTestService.GetCurrentIndexOfQuestion(userId) + 1}. {_executorTestService.GetQuestion(userId)}:",
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

    private async Task SendErrorWithDatabaseSavingAsync(ITelegramBotClient botClient, long chatId, int messageId)
    {
        try
        {
            await botClient.EditMessageTextAsync
            (
                chatId: chatId,
                messageId: messageId,
                text: "Не удалось сохранить результат",
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

    private async Task AddResultToMessageAsync(ITelegramBotClient botClient, long chatId, int messageId)
    {
        try
        {
            await botClient.EditMessageTextAsync
            (
                chatId: chatId,
                messageId: messageId,
                text: "Вы прошли тест",
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
}