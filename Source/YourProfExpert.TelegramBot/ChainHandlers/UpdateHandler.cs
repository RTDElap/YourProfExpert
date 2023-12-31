using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

using YourProfExpert.TelegramBot.ChainHandlers.Handlers;

namespace YourProfExpert.TelegramBot.ChainHandlers;

/// <summary>
/// Класс для обработки Update'ов
/// </summary>
public class UpdateHandler : IUpdateHandler
{
    private readonly Handler _handler;

    public UpdateHandler(Handler handler)
    {
        _handler = handler;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception? exception, CancellationToken cancellationToken)
    {
        
        while ( exception is not null )
        {
            Console.WriteLine($"{exception.Message}");

            exception = exception.InnerException;
        }

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await _handler.HandleAsync(botClient, update, cancellationToken);
    }

    public static UpdateHandler Create(Handler handler) => new UpdateHandler(handler);
}