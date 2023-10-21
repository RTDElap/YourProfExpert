using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;
using YourProfExpert.TelegramBot.ChainHandlers;
using YourProfExpert.TelegramBot.ChainHandlers.Builders;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Bot.Builders;

/// <summary>
/// Фасад создания бота через билдер 
/// </summary>
public class BotBuilder
{
    private IServiceProvider? _serviceProvider;

    public IConfigurationRoot? ConfigurationRoot { get; private set; }
    public BotConfig? BotConfig { get; private set; }

    public readonly HandlerBuilder HandlerBuilder;
    public readonly IServiceCollection Services;

    /// <summary>
    /// Инициализируется при первом обращении, используя Services
    /// </summary>
    /// <value>Зарегистрированные сервисы</value>
    public IServiceProvider ServiceProvider
    {
        get
        {
            if ( _serviceProvider is null ) 
                _serviceProvider = Services.BuildServiceProvider();

            return _serviceProvider;
        }
    }

    public BotBuilder()
    {
        Services = new ServiceCollection();
        HandlerBuilder = new();
    }

    /// <summary>
    /// Создает IConfigurationRoot из json файла
    /// </summary>
    /// <param name="pathToFile">Путь к json файлу</param>
    /// <returns></returns>
    public BotBuilder ReadConfigFrom(string pathToFile)
    {
        ConfigurationRoot = new ConfigurationBuilder()
            .AddJsonFile( pathToFile )
            .Build();
        
        return this;
    }

    /// <summary>
    /// Инициализирует BotConfig из json конфига
    /// Создает исключение, есть корня конфигурации или секции "BotConfig" нету 
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public BotBuilder AddBotConfig()
    {
        if ( ConfigurationRoot is null )
            throw new ArgumentNullException("ConfigurationRoot is null");

        BotConfig = ConfigurationRoot.GetSection(nameof(BotConfig)).Get<BotConfig>();

        if ( BotConfig is null )
            throw new ArgumentNullException("BotConfig is null");

        Services.AddSingleton<BotConfig>
        (
            BotConfig
        );

        return this;
    }

    /// <summary>
    /// Строит объект типа Bot, используя HandlerBuilder для создания UpdateHandler.
    /// Если BotConfig равен null, то будет вызвано исключение.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public Bot Build()
    {
        if ( BotConfig is null )
            throw new ArgumentNullException("BotConfig is null");

        IUpdateHandler updateHandler = new UpdateHandler( HandlerBuilder.Build() );

        return new Bot(BotConfig, updateHandler);
    }
}