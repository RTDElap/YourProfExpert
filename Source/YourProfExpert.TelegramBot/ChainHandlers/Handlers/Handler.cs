

using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers;

public abstract class Handler
{
    /// <summary>
    /// Следующий обработчик в цепи
    /// </summary>
    public Handler? Next;

    /// <summary>
    /// Бизнес-логика
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<bool> ProcessAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    public virtual async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if ( ! await ProcessAsync(botClient, update, cancellationToken) && Next is not null )
        {
            await Next.HandleAsync( botClient, update, cancellationToken );
        }
    }
}