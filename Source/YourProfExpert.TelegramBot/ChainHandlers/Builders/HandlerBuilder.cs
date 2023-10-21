

using YourProfExpert.TelegramBot.ChainHandlers.Handlers;

namespace YourProfExpert.TelegramBot.ChainHandlers.Builders;

/// <summary>
/// Класс, для создания цепочки обработчиков
/// </summary>
public class HandlerBuilder
{
    private Handler? _rootHandler;
    private Handler? _currentHandler;

    /// <summary>
    /// Добавляет в цепочку обработчик
    /// </summary>
    /// <param name="handler">Обработчик</param>
    /// <returns></returns>
    public HandlerBuilder AddHandler(Handler handler)
    {
        if ( _rootHandler is null || _currentHandler is null )
        {
            _rootHandler = handler;
            _currentHandler = _rootHandler;
        
            return this;
        }

        _currentHandler.Next = handler;

        return this;
    }

    /// <summary>
    /// Возвращает корень цепи.
    /// Вызывает исключение, если ни один обработчик не был добавлен
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public Handler Build()
    {
        if ( _rootHandler is null ) throw new ArgumentNullException("RootHandler is empty");

        return _rootHandler;
    }

    public static HandlerBuilder Create() => new HandlerBuilder();
}