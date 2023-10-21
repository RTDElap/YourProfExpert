#nullable disable

using Telegram.Bot;
using Telegram.Bot.Types;
using YourProfExpert.TelegramBot.Configs;

namespace YourProfExpert.TelegramBot.Commands;

public class CommandStart : IRunnable
{
    private readonly BotConfig _botConfig;

    public CommandStart(BotConfig botConfig) =>
        _botConfig = botConfig;

    public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        throw new NotImplementedException();
    }

    public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
    {
        long chatId = update.Message.Chat.Id;
        
        await botClient.SendTextMessageAsync
        (
            chatId: chatId,
            text: _botConfig.TextMessages["main"],
            replyMarkup: _botConfig.ReplyMarkups["main"]
        );
    }
}