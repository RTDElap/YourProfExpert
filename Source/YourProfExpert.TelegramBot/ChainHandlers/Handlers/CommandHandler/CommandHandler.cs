

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Handlers;

public class CommandHandler : Handler
{
    private readonly Dictionary<string, IRunnable> _messageCommands;
    private readonly Dictionary<string, IRunnable> _callbackCommands;

    public CommandHandler(Dictionary<string, IRunnable> messageCommands, Dictionary<string, IRunnable> callbackCommands)
    {
        _messageCommands = messageCommands;
        _callbackCommands = callbackCommands;
    }

    private string[] GetArguments(string message)
    {
        return message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    private async Task<bool> HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        string? message = update.Message?.Text ?? update.Message?.Caption;

        if ( message is null ) return false;

        if ( _messageCommands.TryGetValue( message, out IRunnable? runnable ) && runnable is not null )
        {
            await runnable.RunFromMessageAsync(botClient, update, cancellationToken, GetArguments(message) );

            return true;
        }

        return false;
    }

    private async Task<bool> HandleCallbackQueryAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        string? message = update.CallbackQuery?.Data; 

        if ( message is null ) return false;

        string[] args = GetArguments(message);

        if ( _messageCommands.TryGetValue( args[0], out IRunnable? runnable ) && runnable is not null )
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
}