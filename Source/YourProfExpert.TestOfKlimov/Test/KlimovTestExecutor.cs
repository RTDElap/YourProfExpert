using YourProfExpert.Core.Tests;
using YourProfExpert.Core.Tests.Builders;

namespace YourProfExpert.TestOfKlimov;

public class KlimovTestExecutor : TestExecutor
{
    private readonly Random _random;

    private readonly string[] _questionNames = new string[]
    {
        "Чем вам больше нравится заниматься?",
        "Чтобы вы предпочли?"
    };

    private string _randomQuestionName
    {
        get => _questionNames[_random.Next(_questionNames.Length)];
    }
    
    private readonly int[] _scores = new int[KlimovTestData.RESULTS_COUNT];

    public KlimovTestExecutor(KlimovTest test, Random? random = null) : base(test)
    {
        _random = random ?? new Random(); 

        var questionsBuilder = new QuestionsBuilder( _questions );

        /// Ознакомиться с методичкой можно здесь: https://www.kurgancollege.ru/upload/docs/test.pdf
        
        // 1
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Ухаживать за животными", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Обслуживать машины и технические приборы", () => ++_scores[(int)TypeKlimov.Technique]);

        // 2
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Помогать больным людям, лечить их", () => ++_scores[(int)TypeKlimov.Man])
            .AddAnswer("Составлять таблицы и схемы", () => ++_scores[(int)TypeKlimov.SignSystem]);

        // 3
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Следить за качеством книжных иллюстраций", () => ++_scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Следить за развитием растениий", () => ++_scores[(int)TypeKlimov.Nature]);

        // 4
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Обрабатывать материалы", () => ++_scores[(int)TypeKlimov.Technique])
            .AddAnswer("Доводить товары до потребителя", () => ++_scores[(int)TypeKlimov.Man]);

        // 5
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Обсуждать научно-популярные книги", () => ++_scores[(int)TypeKlimov.SignSystem])
            .AddAnswer("Обсуждать художественные книги", () => ++_scores[(int)TypeKlimov.ArtisticImage]);

        // 6
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Выращивать молодняк", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Тренировать товарищей", () => ++_scores[(int)TypeKlimov.Man]);

        // 7
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Копировать рисунки и изображения", () => ++_scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Управлять каким-либо грузовым средством", () => ++_scores[(int)TypeKlimov.Technique]);

        // 8
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Сообщать людям нужные им сведения", () => ++_scores[(int)TypeKlimov.Man])
            .AddAnswer("Художественно оформлять выставки", () => ++_scores[(int)TypeKlimov.ArtisticImage]);

        // 9
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Выполнять вычисления", () => ++_scores[(int)TypeKlimov.Technique])
            .AddAnswer("Искать и исправлять ошибки в текстах", () => ++_scores[(int)TypeKlimov.SignSystem]);

        // 10
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Лечить животных", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Выполнять вычисления, расчеты", () => ++_scores[(int)TypeKlimov.SignSystem]);

        // 11
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Выводить новые сорта растений", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Проектировать новые виды пром. изделий", () => ++_scores[(int)TypeKlimov.Technique]);

        // 12
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Разбирать ссоры между людьми", () => ++_scores[(int)TypeKlimov.Man])
            .AddAnswer("Разбираться в чертежа", () => ++_scores[(int)TypeKlimov.SignSystem]);

        // 13
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Изучать работу кружков художественной самодеятельности", () => ++_scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Изучать жизнь микробов", () => ++_scores[(int)TypeKlimov.Nature]);

        // 14
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Налаживать медицинские приборы", () => ++_scores[(int)TypeKlimov.Technique])
            .AddAnswer("Оказывать людям медицинскую помощь", () => ++_scores[(int)TypeKlimov.Man]);

        // 15
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Составлять точные описания о наблюдаемых явлениях", () => ++_scores[(int)TypeKlimov.SignSystem])
            .AddAnswer("Художественно описывать, изображать события", () => ++_scores[(int)TypeKlimov.ArtisticImage]);

        // 16
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Делать лабораторные анализы в больнице", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Осматривать больных и назначать лечение", () => ++_scores[(int)TypeKlimov.Man]);

        // 17
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Красить или расписывать стены помещений", () => ++_scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Осуществлять монтаж зданий", () => ++_scores[(int)TypeKlimov.Technique]);

        // 18
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Организовывать сверстников в театры", () => ++_scores[(int)TypeKlimov.Man])
            .AddAnswer("Играть на сцене", () => ++_scores[(int)TypeKlimov.ArtisticImage]);

        // 19
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Изготовлять по чертежам детали", () => ++_scores[(int)TypeKlimov.Technique])
            .AddAnswer("Заниматься черчением", () => ++_scores[(int)TypeKlimov.SignSystem]);

        // 20
        questionsBuilder.CreateQuestion(_randomQuestionName)
            .AddAnswer("Вести борьбу с болезнями растений", () => ++_scores[(int)TypeKlimov.Nature])
            .AddAnswer("Работать на клавишных машинах", () => ++_scores[(int)TypeKlimov.SignSystem]);
    }

    public override int GetResult()
    {
        return Array.IndexOf(_scores, _scores.Max()) + 1;
    }
}

internal enum TypeKlimov
{
    Nature,
    Technique,
    SignSystem,
    ArtisticImage,
    Man
}