#nullable disable

using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Core.Types;

namespace YourProfExpert.TelegramBot.Configs;

public class BotConfig
{
    [JsonInclude]
    public string KeyApi { get; set; }

    [JsonInclude]
    public Profession[] Professions { get; set; }

    [JsonIgnore]
    public IDictionary<string, string> TextMessages { get; set; }

    [JsonIgnore]
    public IDictionary<string, InlineKeyboardMarkup> InlineMarkups { get; set; }

    [JsonIgnore]
    public IDictionary<string, ReplyKeyboardMarkup> ReplyMarkups { get; set; }

    public BotConfig()
    {
        TextMessages = new Dictionary<string, string>()
        {
            { "main", "бот" }
        };

        
    }
}