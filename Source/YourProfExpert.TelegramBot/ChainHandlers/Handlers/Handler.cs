

using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers;

/// <summary>
/// Класс, описывающий обработчик цепи
/// </summary>
public abstract class Handler
{
    /// <summary>
    /// Следующий обработчик в цепи
    /// </summary>
    public Handler? Next;

    /// <summary>
    /// Реализует непосредственно бизнес-логику
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Возвращает флаг, указывающий возможность обработать запрос</returns>
    public abstract Task<bool> ProcessAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    /// <summary>
    /// Основной метод обработки запроса. Вызывает ProcessAsync и, если он вернул значение true,
    /// то НЕ передает его дальше по цепочке
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if ( ! await ProcessAsync(botClient, update, cancellationToken) && Next is not null )
        {
            await Next.HandleAsync( botClient, update, cancellationToken );
        }
    }
}