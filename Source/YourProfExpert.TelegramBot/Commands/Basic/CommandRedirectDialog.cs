

#nullable disable

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.Auxiliary;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandRedirectDialog : IRunnable
{
    private readonly ICommandHandler _commandHandler;
    private readonly ILogger<CommandRedirectDialog> _logger;

    public CommandRedirectDialog(ICommandHandler commandHandler, ILogger<CommandRedirectDialog> logger)
    {
        _commandHandler = commandHandler;
        _logger = logger;
    }

    public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        if ( args.Length < 2 )
        {
            _logger.LogError($"Недостаточное количество аргументов: {string.Join(' ', args)}");

            return;
        }

        // args.Skip(1) для пропуска самой команды (/dialog)
        string commandToRedirect = string.Join( ' ', args.Skip(1) );

        await _commandHandler.RedirectTo(commandToRedirect, botClient, update, cancellationToken);
    }

    public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        throw new NotSupportedException();
    }
}