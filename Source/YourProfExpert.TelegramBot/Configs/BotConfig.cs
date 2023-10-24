#nullable disable

using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Auxiliary;
using YourProfExpert.Core.Types;

namespace YourProfExpert.TelegramBot.Configs;

public class BotConfig
{
    /// <summary>
    /// Api ключ для телеграмм бота
    /// </summary>
    /// <value></value>
    [JsonInclude]
    public string KeyApi { get; set; }

    /// <summary>
    /// Список специальностей с их характеристиками
    /// </summary>
    /// <value></value>
    [JsonInclude]
    public Profession[] Professions { get; set; }

    /// <summary>
    /// Определяет какую базу данных использовать.
    /// Название ConnectionString (в одноименной секции json-файла) должно обязательно совпадать с ним
    /// </summary>
    /// <value></value>
    [JsonInclude]
    public string DatabaseType { get; set; }

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
        TextMessages["about"] = "Этот бот предназначен для помощи учащимся и молодежи в поиске своей будущей профессии и индивидуальности.\n\nВо-первых, рекомендую вам пройти <b>тест Голланда</b>, разработанный по принципу учения американского ученого в сфере психологии <i> Джона Генри Холланда</i>.\n\nВо-вторых, обязательно пройдите <b>тест Климова</b>, за основу которого взята классификация профессий <i>Евгения Климова</i>.\n\nПотом проанализируйте результаты и подумайте, какая из предоставленных профессий подойдет для вас лучше всего! \n \n<b> Также расскажу о механике бота </b> \n \n - напишите /start, чтобы обновить бота (результаты будут сохранены).\n \n- если начинаете проходить тест, то проходите от начала до конца и не оставляйте все, потому что результат не будет сохранен! \n \n- о возникновении ошибок или вопросов сообщайте @elapst";
        TextMessages["tests"] = "Выберите тест для прохождения";

        ReplyMarkups["main"] = Helper.CreateReplyMarkup
        (
            new[] { Helper.CreateButton("👤 О боте"), Helper.CreateButton("📄 Тесты") },
            new[] { Helper.CreateButton("📖 Результаты"), Helper.CreateButton("👩‍🚒 Профессии") }
        );

        ReplyMarkups["about"] = Helper.CreateReplyMarkup
        (
            new[] { Helper.CreateButton("🏠 Главная"), Helper.CreateButton("📄 Тесты") },
            new[] { Helper.CreateButton("📖 Результаты"), Helper.CreateButton("👩‍🚒 Профессии") }
        );
    }
}