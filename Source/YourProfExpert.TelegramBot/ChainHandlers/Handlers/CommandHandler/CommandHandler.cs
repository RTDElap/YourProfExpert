

using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers;

/// <summary>
/// Реализует маршрутизатор функциональных (с бизнес-логикой) команд бота
/// </summary>
public class CommandHandler : Handler, ICommandHandler
{
    private readonly ILogger<CommandHandler> _logger;

    private IDictionary<string, IRunnable> _commands;

    public CommandHandler(ILogger<CommandHandler> logger)
    { 
        _logger = logger;

        _commands = new Dictionary<string, IRunnable>();
    }

    public override async Task<bool> ProcessAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch ( update.Type )
        {
            case UpdateType.Message:
                return await HandleMessageAsync(botClient, update, cancellationToken);

            case UpdateType.CallbackQuery:
                return await HandleCallbackQueryAsync(botClient, update, cancellationToken);
        }

        return false;
    }

    public async Task RedirectTo(string commandName, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var args = GetArguments(commandName);

        await HandleUpdateAsync
        (
            botClient,
            update,
            cancellationToken,
            args[0],
            args,
            _commands,
            cmd => cmd.RunFromCallbackAsync(botClient, update, cancellationToken, args) 
        );
    }

    public ICommandHandler AddCommand(IRunnable command, string[] names)
    {
        foreach ( var name in names)
            _commands.Add(name, command);

        return this;
    }

    private async Task<bool> HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Сообщение: {update.Message?.Text ?? update.Message?.Caption}");

        string? message = update.Message?.Text ?? update.Message?.Caption;
        
        var args = GetArguments(message);

        return await HandleUpdateAsync
        (
            botClient,
            update,
            cancellationToken,
            message,
            args,
            _commands,
            command => command.RunFromMessageAsync(botClient, update, cancellationToken, args) 
        );
    }

    private async Task<bool> HandleCallbackQueryAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Сообщение: {update.CallbackQuery?.Data}");

        string? message = update.CallbackQuery?.Data; 

        var args = GetArguments(message);

        return await HandleUpdateAsync
        (
            botClient,
            update,
            cancellationToken,
            args[0],
            args,
            _commands,
            command => command.RunFromCallbackAsync(botClient, update, cancellationToken, args) 
        );
    }

    private string[] GetArguments(string message)
    {
        return message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    private async Task<bool> HandleUpdateAsync
    (
        ITelegramBotClient botClient, 
        Update update, 
        CancellationToken cancellationToken,
        string? commandName,
        string[] commandArgs,
        IDictionary<string, IRunnable> commandDictionary,
        Func<IRunnable, Task> runCommand
    )
    {
        if ( commandName is null )
        {
            _logger.LogDebug($"Команда равен null");

            return false;
        }

        if ( commandDictionary.TryGetValue(commandName, out IRunnable? command) && command is not null )
        {
            await runCommand(command);

            return true;
        }

        return false;
    }
}