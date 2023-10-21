

using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandStart : IRunnable
{
    public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        throw new NotImplementedException();
    }

    public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        throw new NotImplementedException();
    }
}