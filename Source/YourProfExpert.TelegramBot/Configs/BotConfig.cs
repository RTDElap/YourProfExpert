#nullable disable

using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Auxiliary;

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

        InitTextMessages();

        ReplyKeyboardMarkups = new Dictionary<string, ReplyKeyboardMarkup>();

        InitReplyKeyboardMarkups();

        InlineKeyboardMarkups = new Dictionary<string, InlineKeyboardMarkup>();

        InitInlineKeyboardMarkups();
    }

    private void InitTextMessages()
    {
        TextMessages["main"] = 
            "Привет, {0}!\nМеня зовут <b>ТвойПрофЭксперт</b>.\nЯ попытаюсь помочь тебе определить свои склонности и выбрать сферу будущей деятельности!";
    }

    private void InitReplyKeyboardMarkups()
    {
        ReplyKeyboardMarkups["main"] = Helper.CreateReplyMarkup
        (
            new[] { Helper.CreateButton("👤 О боте"), Helper.CreateButton("📄 Тесты") },
            new[] { Helper.CreateButton("📖 Результаты"), Helper.CreateButton("👩‍🚒 Профессии") }
        );
    }

    private void InitInlineKeyboardMarkups()
    {
        
    }
}