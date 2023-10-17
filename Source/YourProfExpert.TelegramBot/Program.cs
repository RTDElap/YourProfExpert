#nullable disable

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.KlimovTest;

using YourProfExpert.Auxiliary;

using YourProfExpert.TelegramBot.ChainHandlers;
using YourProfExpert.TelegramBot.ChainHandlers.Builders;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    static IServiceProvider RegisterAndConfigureServices(IConfigurationRoot root)
    {
        IServiceCollection services = new ServiceCollection();

        RegisterDatabaseAsync(services, root, "Sqlite");
        RegisterConfigs(services, root);
        RegisterServices(services);

        services.Configure<IContextCreator>
        ( 
            async creator => 
            {
                await creator.CreateContext().AddKlimovTestToContextAsync();
            }
        );

        services.Configure<ITestService>
        ( 
            testService => 
            {
                testService.AddKlimovTest();
            }
        );

        services.Configure<IJobsService>
        ( 
            jobsService => 
            {
                jobsService.SetJobs( root.GetValue<Job[]>("Jobs") );
            }
        );

        return services.BuildServiceProvider();
    }

    static async void ConfigureAndStartBot(IServiceProvider serviceProvider)
    {
        var commands = CreateCommands(serviceProvider);

        var handlerChain = HandlerBuilder
            .Create()
            .AddCommandHandler( commands.MessageCommands, commands.callbackCommands )
            .Build();

        var updateHandler = UpdateHandler.Create( handlerChain );

        BotConfig botConfig = serviceProvider.GetService<BotConfig>();

        ITelegramBotClient botClient = new TelegramBotClient( botConfig.KeyApi );

        var bot = await botClient.GetMeAsync();

        Console.WriteLine
        (
            $"Запущен бот: @{bot.Username}\nНазвание: {Helper.GetFullNameUser(bot)}"
        );

        botClient
            .StartReceiving
            (
                updateHandler: updateHandler
            );
    }

    static void Main()
    {
        var root = GetConfigurationRoot();

        IServiceProvider serviceProvider = RegisterAndConfigureServices(root);
    
        ConfigureAndStartBot(serviceProvider);

        Console.ReadLine();
    }
}