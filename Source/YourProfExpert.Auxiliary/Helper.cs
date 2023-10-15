

using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YourProfExpert.Core.Types;

namespace YourProfExpert.Auxiliary;

public static class Helper
{
    public static string GetFullNameUser(User user)
    {
        return string.IsNullOrWhiteSpace(user.LastName) ? user.FirstName : $"{user.FirstName} {user.LastName}";
    }

    public static KeyboardButton CreateButton(string text)
    {
        return new KeyboardButton(text);
    }

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

    public static InlineKeyboardButton CreateInlineButton(string text, string command)
    {
        return new InlineKeyboardButton(text) { CallbackData = command };
    }

    public static InlineKeyboardMarkup CreateInlineKeyboardWithJobs
    (
        IEnumerable<Job> jobs,
        int startIndex = 0,
        bool withButtonNext = false,
        bool withButtonBack = false
    )
    {
        int count = -1;
        var buttons = new List<InlineKeyboardButton[]>( jobs.Count() );

        foreach (var job in jobs)
        {
            buttons.Add( new[] { CreateInlineButton(job.Name, $"/selectJob {startIndex + ++count}") } );
        }

        if (withButtonNext) buttons.Add(new[] { CreateInlineButton("Следующая", "/nextJobs") });
        if (withButtonBack) buttons.Add(new[] { CreateInlineButton("Предыдущая", "/backJobs") });

        return new InlineKeyboardMarkup(buttons);
    }

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