using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Core.Types;

namespace YourProfExpert.Auxiliary;

public static class Helper
{
    /// <summary>
    /// Вспомогательный метод, который вытягивает из типа User имя и фамилию. 
    /// Если фамилия не указана, то возвращает просто имя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Имя и фамилию</returns>
    public static string GetFullNameUser(User user)
    {
        return string.IsNullOrWhiteSpace(user.LastName) ? user.FirstName : $"{user.FirstName} {user.LastName}";
    }

    /// <summary>
    /// Создает кнопку с указанным текстом
    /// </summary>
    /// <param name="text">Текст кнопки</param>
    /// <returns>Кнопка</returns>
    public static KeyboardButton CreateButton(string text)
    {
        return new KeyboardButton(text);
    }

    /// <summary>
    /// Создает из массивов кнопок цельную клавиатуру
    /// </summary>
    /// <param name="keyboardButtonsRows">Массивы кнопок</param>
    /// <returns>Клавиатура</returns>
    public static ReplyKeyboardMarkup CreateReplyMarkup(params KeyboardButton[][] keyboardButtonsRows) 
    {
        KeyboardButton[][] buttons = new KeyboardButton[keyboardButtonsRows.Length][];
        int currentIndex = -1;

        foreach (KeyboardButton[] buttonsRow in keyboardButtonsRows)
        {
            buttons[++currentIndex] = buttonsRow;
        }

        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true
        };
    }

    /// <summary>
    /// Создает inline клавиатуру из массивов inline кнопок
    /// </summary>
    /// <param name="keyboardButtonsRows">Inline кнопки</param>
    /// <returns>Inline клавиатура</returns>
    public static InlineKeyboardMarkup CreateInlineKeyboard(params InlineKeyboardButton[][] keyboardButtonsRows)
    {
        InlineKeyboardButton[][] buttons = new InlineKeyboardButton[keyboardButtonsRows.Length][];
        int currentIndex = -1;

        foreach (InlineKeyboardButton[] buttonsRow in keyboardButtonsRows)
        {
            buttons[++currentIndex] = buttonsRow;
        }

        return new InlineKeyboardMarkup(buttons);
    }

    /// <summary>
    /// Создает inline кнопку с заданным текстом и CallbackData
    /// </summary>
    /// <param name="text">Текст кнопки</param>
    /// <param name="command">Значение CallbackData</param>
    /// <returns>Inline кнопка</returns>
    public static InlineKeyboardButton CreateInlineButton(string text, string command)
    {
        return new InlineKeyboardButton(text) { CallbackData = command };
    }

    /// <summary>
    /// Вспомогательный метод, который создает клавиатуру с возможностью перемещения
    /// </summary>
    /// <param name="Professions">Список специальностей</param>
    /// <param name="withButtonNext"></param>
    /// <param name="withButtonBack"></param>
    /// <returns></returns>
    public static InlineKeyboardMarkup CreateInlineKeyboardWithProfessions
    (
        IEnumerable<Profession> Professions,
        bool withButtonNext = false,
        bool withButtonBack = false
    )
    {
        int count = -1;
        var buttons = new List<InlineKeyboardButton[]>( Professions.Count() );

        foreach (var Profession in Professions)
        {
            buttons.Add( new[] { CreateInlineButton(Profession.Name, $"/selectProfession {++count}") } );
        }

        if (withButtonNext) buttons.Add(new[] { CreateInlineButton("Следующая", "/nextProfessions") });
        if (withButtonBack) buttons.Add(new[] { CreateInlineButton("Предыдущая", "/backProfessions") });

        return new InlineKeyboardMarkup(buttons);
    }

    /// <summary>
    /// Вспомогательный метод, который создает клавиатуру с выбором ответа
    /// </summary>
    /// <param name="answers">Ответы на вопрос</param>
    /// <returns>Inline клавиатура</returns>
    public static InlineKeyboardMarkup CreateInlineKeyboardWithAnswers(IEnumerable<string> answers)
    {
        int count = -1;
        var buttons = new InlineKeyboardButton[answers.Count()][];

        foreach (var answer in answers)
        {
            buttons[++count] = new[] { CreateInlineButton(answer, $"/selectAnswer {count}") };
        }

        return new InlineKeyboardMarkup(buttons);
    }
}