

using YourProfExpert.TelegramBot.ChainHandlers.Handlers;

namespace YourProfExpert.TelegramBot.ChainHandlers.Builders;

public class HandlerBuilder
{
    private Handler? _rootHandler;
    private Handler? _currentHandler;

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

    public Handler Build()
    {
        if ( _rootHandler is null ) throw new ArgumentNullException("RootHandler is empty");

        return _rootHandler;
    }
}