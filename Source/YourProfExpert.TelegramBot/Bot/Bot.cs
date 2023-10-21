using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;

using YourProfExpert.Auxiliary;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Bot;

/// <summary>
/// Инкапсулирует всю работу с ботом
/// </summary>
public class Bot
{
    private readonly CancellationTokenSource _source;

    public CancellationToken CancellationToken { get => _source.Token; }

    public readonly BotConfig BotConfig;
    public readonly IServiceProvider ServiceProvider;
    public readonly ITelegramBotClient BotClient;
    public readonly IUpdateHandler UpdateHandler;

    public Bot(BotConfig config, IUpdateHandler updateHandler, IServiceProvider serviceProvider)
    {
        _source = new();

        UpdateHandler = updateHandler;

        ServiceProvider = serviceProvider;

        BotConfig = config;

        BotClient = new TelegramBotClient(config.KeyApi);
    }

    /// <summary>
    /// Начинает прослушивание updates от телеграмма в режиме LongPolling.
    /// НЕ блокирующая операция
    /// </summary>
    public void StartAsLongPolling()
    {
        var user = BotClient.GetMeAsync().Result;

        ServiceProvider.GetService<ILogger<Bot>>()
            .LogInformation
            (
                $"Запуск бота: {Helper.GetFullNameUser(user)}"
            );
            

        BotClient.StartReceiving
        (
            updateHandler: UpdateHandler,
            cancellationToken: CancellationToken
        );
    }
}