#nullable disable

using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Auxiliary;
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
        TextMessages = new Dictionary<string, string>();
        InlineMarkups = new Dictionary<string, InlineKeyboardMarkup>();
        ReplyMarkups = new Dictionary<string, ReplyKeyboardMarkup>();
        
        TextMessages["main"] = "Привет, {0}!\nМеня зовут <b>ТвойПрофЭксперт</b>.\nЯ попытаюсь помочь тебе определить свои склонности и выбрать сферу будущей деятельности!";
    
        ReplyMarkups["main"] = Helper.CreateReplyMarkup
        (
            new[] { Helper.CreateButton("👤 О боте"), Helper.CreateButton("📄 Тесты") },
            new[] { Helper.CreateButton("📖 Результаты"), Helper.CreateButton("👩‍🚒 Профессии") }
        );
    }
}