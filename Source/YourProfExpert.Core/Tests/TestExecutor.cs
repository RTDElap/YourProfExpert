

namespace YourProfExpert.Core.Tests;

public abstract class TestExecutor
{
    protected virtual IList<Question> _questions { get; set; }

    public Question CurrentQuestion { get => _questions[CurrentIndex]; }

    public IList<Answer> CurrentAnswers { get => CurrentQuestion.Answers; }

    public int CurrentIndex { get; protected set;} = 0;

    public readonly Test Test;

    public TestExecutor(Test test)
    {
        Test = test;

        _questions = new List<Question>();
    }

    public virtual bool MoveNext()
    {
        return ++CurrentIndex <= _questions.Count;
    }

    public virtual bool CanMoveNext()
    {
        return CurrentIndex <= _questions.Count;
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