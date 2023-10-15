

namespace YourProfExpert.Core.Tests;

/// <summary>
/// Описывает ответ на заданный вопрос.
/// Принимает лямбда-функцию, которая будет выполняться при выборе ответа
/// </summary>
public class Answer
{
    private Action action;

    public string Name { get; private set; }

    public void Execute() => action();

    public Answer(string name, Action action)
    {
        this.action = action;
        Name = name;
    }
}