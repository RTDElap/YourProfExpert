

namespace YourProfExpert.Core.Tests.Builders;

public class AnswerBuilder
{
    private readonly Question question;

    public AnswerBuilder(Question question)
    {
        this.question = question;
    }

    public AnswerBuilder AddAnswer(string name, Action action)
    {
        question.Answers.Add
        (
            new Answer(name, action)
        );

        return this;
    }
}
