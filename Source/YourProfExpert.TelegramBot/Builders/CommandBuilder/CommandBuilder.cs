

using Microsoft.Extensions.DependencyInjection;
using YourProfExpert.TelegramBot.Bot.Builders;
using YourProfExpert.TelegramBot.ChainHandlers.Handlers.Interfaces;

namespace YourProfExpert.TelegramBot.Commands;

/// <summary>
/// Билдер для создания декорируемых команд
/// </summary>
/// <typeparam name="T">Команда, для которой применяются декораторы</typeparam>
public class CommandBuilder<T> where T : IRunnable
{
    private readonly string[] _names;
    private readonly BotBuilder _botBuilder;

    private readonly Queue<Decorator> _decorators;

    public CommandBuilder(BotBuilder botBuilder, string[] names)
    {
        _botBuilder = botBuilder;
        _names = names;

        _decorators = new Queue<Decorator>();
    }

    /// <summary>
    /// Добавляет в конец очереди декоратор
    /// </summary>
    /// <typeparam name="DecoratorType">Декоратор для создания</typeparam>
    /// <returns></returns>
    public CommandBuilder<T> With<DecoratorType>() where DecoratorType : Decorator
    {
        _decorators.Enqueue( ActivatorUtilities.CreateInstance<DecoratorType>(_botBuilder.ServiceProvider) );

        return this;
    }

    /// <summary>
    /// Создает декорируемую команду и регистрирует ее в ICommandHandler
    /// </summary>
    public void Build()
    {
        var currentDecorator = _decorators.Dequeue();

        // Добавление первого добавленного декоратора, как точку в хода для заданных имен
        _botBuilder.ServiceProvider.GetService<ICommandHandler>()
            .AddCommand( currentDecorator, _names );
    
        // Последовательное создание цепочки декораторов
        while ( _decorators.TryDequeue(out Decorator decorator) )
        {
            currentDecorator.SetInnerCommand( decorator );

            currentDecorator = decorator;
        }

        // Добавление в последний декоратор декорируемую команду
        currentDecorator.SetInnerCommand
        (
            ActivatorUtilities.CreateInstance<T>(_botBuilder.ServiceProvider)
        );
    }
}