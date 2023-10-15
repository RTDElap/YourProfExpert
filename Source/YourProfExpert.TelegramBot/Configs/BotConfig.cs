#nullable disable

using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace YourProfExpert.TelegramBot.Configs;

public class BotConfig
{
    public string KeyApi { get; set; }

    [JsonIgnore]
    public IDictionary<string, string> TextMessages;

    [JsonIgnore]
    public IDictionary<string, ReplyKeyboardMarkup> ReplyKeyboardMarkups;

    [JsonIgnore]
    public IDictionary<string, InlineKeyboardMarkup> InlineKeyboardMarkups;

    public BotConfig()
    {
        TextMessages = new Dictionary<string, string>();

        ReplyKeyboardMarkups = new Dictionary<string, ReplyKeyboardMarkup>();

        InlineKeyboardMarkups = new Dictionary<string, InlineKeyboardMarkup>();
    }
}