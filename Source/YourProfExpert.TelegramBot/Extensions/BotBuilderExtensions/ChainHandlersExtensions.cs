

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
    /// Добавляет зарегистрированный ICommandHandler в цепочку обработчиков.
    /// Этот метод инициализирует ServiceProvider
    /// </summary>
    /// <param name="botBuilder"></param>
    /// <returns></returns>
    public static BotBuilder UseCommandHandler(this BotBuilder botBuilder)
    {
        var commandHandler = botBuilder.ServiceProvider.GetService<ICommandHandler>() as Handler;

        botBuilder.HandlerBuilder
            .AddHandler(commandHandler);

        return botBuilder;
    }
}