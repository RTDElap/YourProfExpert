

using YourProfExpert.TelegramBot.ChainHandlers.Handlers;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.ChainHandlers.Builders;

public static class CommandHandlerExtensions
{
    public static HandlerBuilder AddCommandHandler
    (
        this HandlerBuilder handlerBuilder, 
        Dictionary<string, IRunnable> messageCommands, 
        Dictionary<string, IRunnable> callbackCommands
    )
    {
        handlerBuilder.AddHandler( new CommandHandler(messageCommands, callbackCommands) );

        return handlerBuilder;
    }
}