#nullable disable

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.KlimovTest;
using YourProfExpert.TelegramBot.ChainHandlers;
using YourProfExpert.TelegramBot.ChainHandlers.Builders;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    static async Task Main()
    {
        IServiceCollection services = new ServiceCollection();
        var root = GetConfigurationRoot();

        RegisterDatabaseAsync(services, root, "Sqlite");
        RegisterConfigs(services, root);
        RegisterServices(services);

        services.Configure<IContextCreator>( 
            async x => await x.CreateContext().AddKlimovTestToContextAsync() 
        );

        services.Configure<ITestService>( x => x.AddKlimovTest() );

        services.Configure<IJobsService>( x => x.SetJobs( root.GetValue<Job[]>("Jobs") ));

        IServiceProvider serviceProvider = services.BuildServiceProvider();
    
        var commands = CreateCommands(serviceProvider);

        var handlerChain = HandlerBuilder
            .Create()
            .AddCommandHandler( commands.MessageCommands, commands.callbackCommands )
            .Build();

        var updateHandler = UpdateHandler.Create( handlerChain );

        BotConfig? botConfig = serviceProvider.GetService<BotConfig>();
    
        if ( botConfig is null ) throw new ArgumentNullException("BotConfig is null");

        ITelegramBotClient botClient = new TelegramBotClient( botConfig.KeyApi );

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Запущен бот: {me.Username}");

        botClient.StartReceiving
        (
            updateHandler: updateHandler.HandleUpdateAsync,
            pollingErrorHandler: updateHandler.HandlePollingErrorAsync
        );

        Console.ReadLine();
    }
}