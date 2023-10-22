

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
    
        AddCommandStart(serviceProvider, messageCommands, callbackCommands);
        AddCommandAbout(serviceProvider, messageCommands, callbackCommands);
        AddCommandDialog(serviceProvider, messageCommands, callbackCommands);

        AddCommandTests(serviceProvider, messageCommands, callbackCommands);

        return (messageCommands, callbackCommands);
    }

    internal static void AddCommandStart
    (
        IServiceProvider serviceProvider, 
        IDictionary<string, IRunnable> messageCommands, 
        IDictionary<string, IRunnable> callbackCommands
    )
    {
        var commandStart = ActivatorUtilities.CreateInstance<CommandStart>(serviceProvider);

        callbackCommands["/start"] = commandStart;
        messageCommands["/start"] = commandStart;
        messageCommands["🏠 Главная"] = commandStart;
        messageCommands["🏠 В главное меню"] = commandStart;
    }

    internal static void AddCommandAbout
    (
        IServiceProvider serviceProvider, 
        IDictionary<string, IRunnable> messageCommands, 
        IDictionary<string, IRunnable> callbackCommands
    )
    {
        var commandAbout = ActivatorUtilities.CreateInstance<CommandAbout>(serviceProvider);

        messageCommands["👤 О боте"] = commandAbout;
    }

    internal static void AddCommandTests
    (
        IServiceProvider serviceProvider, 
        IDictionary<string, IRunnable> messageCommands, 
        IDictionary<string, IRunnable> callbackCommands
    )
    {
        var commandTests = ActivatorUtilities.CreateInstance<CommandTests>(serviceProvider);

        messageCommands["📄 Тесты"] = commandTests;
    }

    internal static void AddCommandDialog
    (
        IServiceProvider serviceProvider, 
        IDictionary<string, IRunnable> messageCommands, 
        IDictionary<string, IRunnable> callbackCommands
    )
    {
        var commandRedirectDialog = ActivatorUtilities.CreateInstance<CommandRedirectDialog>(serviceProvider);

        callbackCommands["/dialog"] = commandRedirectDialog;
    }
}