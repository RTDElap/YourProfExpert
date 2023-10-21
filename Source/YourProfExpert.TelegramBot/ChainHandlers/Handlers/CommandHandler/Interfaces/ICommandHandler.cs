

using Telegram.Bot;
using Telegram.Bot.Types;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;

public interface ICommandHandler
{
    /// <summary>
    /// Позволяет перенаправить выполнение из одной команды в другую
    /// </summary>
    /// <param name="commandName">Название команды, к которой нужно перейти</param>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task RedirectTo(string commandName, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    public ICommandHandler SetCallbackCommands(IDictionary<string, IRunnable> callbackCommands);
    public ICommandHandler SetMessageCommands(IDictionary<string, IRunnable> callbackCommands);
}