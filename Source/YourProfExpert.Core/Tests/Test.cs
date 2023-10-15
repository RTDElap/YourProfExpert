

namespace YourProfExpert.Core.Tests;

/// <summary>
/// Представляет данные для выполнения теста пользователем
/// </summary>
public abstract class Test
{
    public string Title;

    public string Description;

    public IList<Question> Questions;

    public Test(string title, string description)
    {
        Title = title;
        Description = description;

        Questions = new List<Question>();
    }

    public abstract TestExecutor CreateExecutor();
}