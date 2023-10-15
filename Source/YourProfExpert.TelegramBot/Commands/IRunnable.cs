
using Telegram.Bot;
using Telegram.Bot.Types;

namespace YourProfExpert.TelegramBot.Commands;

public interface IRunnable
{
    public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args);
    public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args);
}