

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
    /// Создает команду с помощью IServiceProvider и добавляет ее в ICommandHandler
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <param name="aliases">Название команды, к которой она будет привязана</param>
    /// <typeparam name="T">Добавляемая команда</typeparam>
    /// <returns></returns>
    public static BotBuilder AddCommand<T>(this BotBuilder botBuilder, params string[] aliases) where T : IRunnable
    {
        botBuilder.ServiceProvider.GetService<ICommandHandler>()
            .AddCommand( ActivatorUtilities.CreateInstance<T>(botBuilder.ServiceProvider), aliases );

        return botBuilder;
    }

    /// <summary>
    /// Создает декорируемую команду с помощью IServiceProvider и добавляет ее в ICommandHandler
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <param name="aliases">Название команды, к которой она будет привязана</param>
    /// <typeparam name="T">Добавляемая команда</typeparam>
    /// <returns></returns>
    public static CommandBuilder<T> AddCustomCommand<T>(this BotBuilder botBuilder, params string[] aliases) where T : IRunnable
    {
        return new CommandBuilder<T>(botBuilder, aliases);
    }

    /// <summary>
    /// Добавляет декоратор к команде, который будет создавать пользователя в базе данных при первом обращении
    /// </summary>
    /// <param name="commandBuilder"></param>
    /// <typeparam name="T">Создаваемая команда</typeparam>
    /// <returns></returns>
    public static CommandBuilder<T> WithCreateUser<T>(this CommandBuilder<T> commandBuilder) where T : IRunnable
    {
        commandBuilder.With<CreateUserDecorator>();

        return commandBuilder;
    }  
}