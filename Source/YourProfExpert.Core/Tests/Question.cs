

namespace YourProfExpert.Core.Tests;

/// <summary>
/// Описывает вопрос и возможные ответы для него
/// </summary>
public class Question
{
    public string Name { get; private set; }

    public readonly List<Answer> Answers;

    public Question(string name)
    {
        Name = name;
        Answers = new List<Answer>();
    }

    public void SelectAnswer(int index) => Answers[index].Execute();
}