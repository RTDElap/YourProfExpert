using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Types;
using YourProfExpert.Infrastructure.Contexts.Creators;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;
using YourProfExpert.KlimovTest;
using YourProfExpert.Services;
using YourProfExpert.TelegramBot.Commands;
using YourProfExpert.TelegramBot.Configs;


namespace YourProfExpert.TelegramBot;

internal static partial class Program
{
    internal static Job[]? _jobs;

    internal static IConfigurationRoot GetConfigurationRoot()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();

        builder
            .SetBasePath( Environment.CurrentDirectory )
            .AddJsonFile("appsettings.json");

        return builder.Build();
    }

    internal static void RegisterDatabaseAsync(IServiceCollection container, IConfigurationRoot root, string name)
    {
        string? connectionString = root.GetConnectionString(name);

        if ( connectionString is null ) throw new ArgumentNullException("BotConfig is null");

        IContextCreator contextCreator = new SqliteCreator
            (opt => opt.UseSqlite( connectionString ));

        container.AddSingleton(contextCreator);
    }

    internal static void RegisterConfigs(IServiceCollection container, IConfigurationRoot root)
    {
        BotConfig? botConfig = root.GetSection(nameof(BotConfig)).Get<BotConfig>();
        _jobs = root.GetSection("Jobs").Get<Job[]>();

        if ( botConfig is null ) throw new ArgumentNullException("BotConfig is null");
        if ( _jobs is null ) throw new ArgumentNullException("Jobs is null");

        container.AddSingleton(botConfig);
    }

    internal static void RegisterServices(IServiceCollection container)
    {
        IJobsService jobsService = new JobsService(_jobs);

        container.AddSingleton<IJobsService>(jobsService);

        container.AddSingleton<ITestService, TestService>();
        container.AddSingleton<IExecutorTestService, ExecutorTestService>();
    }

    internal static (Dictionary<string, IRunnable> MessageCommands, Dictionary<string, IRunnable> callbackCommands) CreateCommands(IServiceProvider container)
    {
        Dictionary<string, IRunnable> messageCommands = new();
        Dictionary<string, IRunnable> callbackCommands = new();

        var start = ActivatorUtilities.CreateInstance<CommandStart>(container);

        messageCommands["/start"] = start;
        messageCommands["🏠 Главная"] = start;

        callbackCommands["/start"] = start;
        
        return ( messageCommands, callbackCommands );
    }
}