

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers;

public class CommandHandler : Handler
{
    private Dictionary<string, IRunnable> _messageCommands;
    private Dictionary<string, IRunnable> _callbackCommands;

    public CommandHandler()
    { }

    private string[] GetArguments(string message)
    {
        return message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    private async Task<bool> HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string? commandName = null)
    {
        string? message = update.Message?.Text ?? update.Message?.Caption;

        if ( message is null ) return false;

        if ( _messageCommands.TryGetValue( commandName ?? message, out IRunnable? runnable ) && runnable is not null )
        {
            await runnable.RunFromMessageAsync(botClient, update, cancellationToken, GetArguments(message) );

            return true;
        }

        return false;
    }

    private async Task<bool> HandleCallbackQueryAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string? commandName = null)
    {
        string? message = update.CallbackQuery?.Data; 

        if ( message is null ) return false;

        string[] args = GetArguments(message);

        if ( _callbackCommands.TryGetValue( commandName ?? args[0], out IRunnable? runnable ) && runnable is not null )
        {
            await runnable.RunFromCallbackAsync(botClient, update, cancellationToken, args );

            return true;
        }

        return false;
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
        switch ( update.Type )
        {
            case UpdateType.Message:
                await HandleMessageAsync(botClient, update, cancellationToken, commandName);
            break;

            case UpdateType.CallbackQuery:
                await HandleCallbackQueryAsync(botClient, update, cancellationToken, commandName);
            break;
        }
    }
}