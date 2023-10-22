#nullable disable

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.Auxiliary;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandAbout : IRunnable
{
    private readonly BotConfig _botConfig;
    private readonly ILogger<CommandAbout> _logger;

    public CommandAbout(BotConfig botConfig, ILogger<CommandAbout> logger) =>
        (_botConfig, _logger) = (botConfig, logger);

    public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        throw new NotSupportedException();
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
                text: _botConfig.TextMessages["about"],
                replyMarkup: _botConfig.ReplyMarkups["about"],
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