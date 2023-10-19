

namespace YourProfExpert.Core.Tests.Builders;

/// <summary>
/// Вспомогательный класс для построения списка вопросов
/// </summary>
public class QuestionsBuilder
{
    private readonly IList<Question> questions;

    private Question? currentQuestion;

    public QuestionsBuilder(IList<Question>? questions = null)
    {
        this.questions = questions ?? new List<Question>();

        currentQuestion = null;
    }

    public AnswerBuilder CreateQuestion(string value)
    {
        currentQuestion = new Question(value);

        questions.Add(currentQuestion);

        return new AnswerBuilder(currentQuestion);
    }
}
