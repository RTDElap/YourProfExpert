#nullable disable

using YourProfExpert.TelegramBot.Bot.Builders;
using Microsoft.Extensions.DependencyInjection;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;
using YourProfExpert.TelegramBot.Extensions;
using YourProfExpert.Core.Services;
using YourProfExpert.TestOfKlimov;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using Microsoft.Extensions.Logging;

namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    static async Task Main()
    {
        var botBuilder = new BotBuilder();

#region Регистрация сервисов
        botBuilder
            .ReadConfigFrom( Path.Combine( Environment.CurrentDirectory, "appsettings.json" ) )
            .AddBotConfig();

        botBuilder.Services
            .AddLogging
            (
                l => l
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace)
            );

        botBuilder
            .AddSqliteCreator("Sqlite")
            .AddTestService()
            .AddExecutorTestService()
            .AddProfessionsService()
            .AddCommandHandler();
#endregion

#region Создание цепочки обработчиков
        botBuilder
            .UseCommandHandler();
#endregion

#region Настройка зарегистрированных сервисов
        var commands = CreateCommands( botBuilder.ServiceProvider );

        await botBuilder.ServiceProvider.GetService<IContextCreator>()
            .CreateContext()
            .AddKlimovTestAsync();

        botBuilder.ServiceProvider.GetService<ITestService>()
            .AddKlimovTest();

        botBuilder.ServiceProvider.GetService<ICommandHandler>()
            .SetMessageCommands(commands.MessageCommands)
            .SetCallbackCommands(commands.CallbackCommands);

        botBuilder.ServiceProvider.GetService<IProfessionsService>()
            .SetProfessions( botBuilder.BotConfig.Professions );
#endregion

        var bot = botBuilder.Build();

        bot.StartAsLongPolling();

        Console.ReadLine();
    }
}