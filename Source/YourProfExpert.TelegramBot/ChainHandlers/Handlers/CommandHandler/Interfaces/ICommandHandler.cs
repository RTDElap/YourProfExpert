

using Telegram.Bot;
using Telegram.Bot.Types;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;

public interface ICommandHandler
{
    /// <summary>
    /// Позволяет перенаправить выполнение из одной команды в другую.
    /// Перенаправляет на Callback-версию команды
    /// </summary>
    /// <param name="commandName">Название команды, к которой нужно перейти</param>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task RedirectTo(string commandName, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет команду в словарь команд
    /// </summary>
    /// <param name="command">Добавляемая команда</param>
    /// <param name="names">Имена команды с которыми она будет связана</param>
    /// <returns></returns>
    public ICommandHandler AddCommand(IRunnable command, string[] names);
}