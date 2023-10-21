

using Telegram.Bot;
using Telegram.Bot.Polling;
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
    public readonly ITelegramBotClient BotClient;
    public readonly IUpdateHandler UpdateHandler;

    public Bot(BotConfig config, IUpdateHandler updateHandler)
    {
        _source = new();

        UpdateHandler = updateHandler;

        BotConfig = config;

        BotClient = new TelegramBotClient(config.KeyApi);
    }

    /// <summary>
    /// Начинает прослушивание updates от телеграмма в режиме LongPolling.
    /// НЕ блокирующая операция
    /// </summary>
    public void StartAsLongPolling()
    {
        BotClient.StartReceiving
        (
            updateHandler: UpdateHandler,
            cancellationToken: CancellationToken
        );
    }
}