namespace YourProfExpert.Core.Tests;

/// <summary>
/// Представляет интерфейс итератора теста в виде отдельной функциональной единицы
/// </summary>
public abstract class TestExecutor
{
    /// <summary>
    /// Текущий список вопросов в прохождении
    /// </summary>
    /// <value>Список вопросов</value>
    protected virtual IList<Question> _questions { get; set; }

    /// <summary>
    /// Текущий вопрос в прохождении
    /// </summary>
    /// <value>Текущий вопрос</value>
    public Question CurrentQuestion { get => _questions[CurrentIndex]; }

    /// <summary>
    /// Список ответов на текущий вопрос
    /// </summary>
    /// <value>Ответы на текущий вопрос</value>
    public IEnumerable<Answer> CurrentAnswers { get => CurrentQuestion.Answers; }

    /// <summary>
    /// Порядковый номер вопроса
    /// </summary>
    /// <value>Порядковый номер вопроса</value>
    public int CurrentIndex { get; protected set;} = 0;

    /// <summary>
    /// Тест, для которого Executor и предназначается
    /// </summary>
    public readonly FunctionalTest Test;

    public TestExecutor(FunctionalTest test)
    {
        Test = test;

        _questions = new List<Question>();
    }
    
    /// <summary>
    /// Возвращает возможность перехода дальше и переходит
    /// </summary>
    /// <returns>Флаг, указывающий на возможность дальнейшего перехода</returns>
    public virtual bool MoveNext()
    {
        return ++CurrentIndex <= _questions.Count;
    }

    /// <summary>
    /// Возвращает возможность перехода дальше
    /// </summary>
    /// <returns>Флаг, указывающий на возможность дальнейшего перехода</returns>
    public virtual bool CanMoveNext()
    {
        return CurrentIndex < _questions.Count;
    }

    /// <summary>
    /// Выбирает ответ на вопрос с помощью индекса
    /// </summary>
    /// <param name="index">Порядковый номер ответа на вопрос</param>
    public virtual void SelectAnswer(int index)
    {
        CurrentQuestion.SelectAnswer(index);
    }

    /// <summary>
    /// Выбирает ответ на вопрос с помощью индекса,
    /// с проверкой на допустимый диапазон 
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Флаг, указывающий на нахождение индекса в допустимом диапазоне</returns>
    public virtual bool TrySelectAnswer(int index)
    {
        if ( index < 0 || index >= CurrentAnswers.Count() ) return false;

        CurrentQuestion.SelectAnswer(index);

        return true;
    }

    /// <summary>
    /// Возвращает orderId - порядковый номер результата на тест
    /// </summary>
    /// <returns>Порядковый номер результата</returns>
    public abstract int GetResult();
}