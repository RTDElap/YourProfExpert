

namespace YourProfExpert.Core.Tests;

public abstract class TestExecutor
{
    protected virtual IList<Question> Questions { get => Test.Questions; }

    public Question CurrentQuestion { get => Questions[_currentIndex]; }

    public IList<Answer> CurrentAnswers { get => CurrentQuestion.Answers; }

    protected int _currentIndex = 0;

    public readonly Test Test;

    public TestExecutor(Test test)
    {
        Test = test;
    }

    public virtual void SelectAnswer(int index)
    {
        CurrentQuestion.SelectAnswer(index);
    }

    /// <summary>
    /// Возвращает orderId
    /// </summary>
    /// <returns></returns>
    public abstract int GetResult();
}