

using Microsoft.Extensions.DependencyInjection;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    /// <summary>
    /// Метод создает команды и заполняет их в MessageCommands и CallbackCommands
    /// </summary>
    public static 
    (
        IDictionary<string, IRunnable> MessageCommands, 
        IDictionary<string, IRunnable> CallbackCommands
    ) 
    CreateCommands(IServiceProvider serviceProvider)
    {
        IDictionary<string, IRunnable> messageCommands = new Dictionary<string, IRunnable>();
        IDictionary<string, IRunnable> callbackCommands = new Dictionary<string, IRunnable>();
    
        var commandStart = ActivatorUtilities.CreateInstance<CommandStart>(serviceProvider);

        messageCommands["/start"] = commandStart;
        callbackCommands["/start"] = commandStart;

        return (messageCommands, callbackCommands);
    }
}