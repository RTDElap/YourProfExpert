

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts.Creators;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Services;
using YourProfExpert.TelegramBot.Bot.Builders;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;
using YourProfExpert.TelegramBot.Commands;

namespace YourProfExpert.TelegramBot.Extensions;

public static partial class BotBuilderExtensions
{
    /// <summary>
    /// Регистрирует реализацию IContextCreator в виде SqliteCreator 
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <param name="nameOfConnectionString">Название строки подключения в конфиге</param>
    /// <returns></returns>
    public static BotBuilder AddSqliteCreator(this BotBuilder botBuilder, string nameOfConnectionString)
    {
        botBuilder
            .Services
            .AddSingleton<IContextCreator>
            (
                x => new SqliteContextCreator( botBuilder.ConfigurationRoot.GetConnectionString(nameOfConnectionString) )
            );

        return botBuilder;
    }

    /// <summary>
    /// Регистрирует реализацию IExecutorTestService
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder AddExecutorTestService(this BotBuilder botBuilder)
    {
        botBuilder
            .Services
            .AddSingleton<IExecutorTestService, ExecutorTestService>();

        return botBuilder;
    }

    /// <summary>
    /// Регистрирует реализацию IProfessionsService
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder AddProfessionsService(this BotBuilder botBuilder)
    {
        botBuilder
            .Services
            .AddSingleton<IProfessionsService, ProfessionsService>(); 

        return botBuilder;
    }

    /// <summary>
    /// Регистрирует реализацию ITestService
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder AddTestService(this BotBuilder botBuilder)
    {
        botBuilder
            .Services
            .AddSingleton<ITestService, TestService>();

        return botBuilder;
    }

    /// <summary>
    /// Регистрирует реализацию ICommandHandler
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder AddCommandHandler(this BotBuilder botBuilder)
    {
        botBuilder
            .Services
            .AddSingleton<ICommandHandler, CommandHandler>();

        return botBuilder;
    }

    /// <summary>
    /// Регистрирует реализацию IUserService
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder AddUserService(this BotBuilder botBuilder)
    {
        botBuilder
            .Services
            .AddSingleton<IUserService, UserService>();

        return botBuilder;
    }
}