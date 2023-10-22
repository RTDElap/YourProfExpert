

using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourProfExpert.TelegramBot.Commands;

/// <summary>
/// Базовый класс для всех декораторов
/// </summary>
public abstract class Decorator : IRunnable
{
    protected IRunnable? _innerCommand;

    public Decorator()
    {

    }

    public void SetInnerCommand(IRunnable innerCommand)
    {
        _innerCommand = innerCommand;
    }

    public abstract Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args);

    public abstract Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args);
}