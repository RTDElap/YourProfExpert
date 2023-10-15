using YourProfExpert.Core.Tests;
using YourProfExpert.Core.Tests.Builders;

namespace YourProfExpert.KlimovTest;

public class KlimovTest : Test
{
    private readonly Random _random;

    private readonly string[] questionNames = new string[]
    {
        "Чем вам больше нравится заниматься?",
        "Чтобы вы предпочли?"
    };

    private string randomQuestionName
    {
        get => questionNames[_random.Next(questionNames.Length)];
    }
    
    public readonly int[] Scores = new int[KlimovTestData.RESULTS_COUNT];

    public KlimovTest(Random? random = null) : base(KlimovTestData.KLIMOV_TITLE, KlimovTestData.KLIMOV_DESCRIPTION)
    {
        _random = random ?? new Random();
    }

    public KlimovTest(IList<Question> questions, Random? random = null) : base(KlimovTestData.KLIMOV_TITLE, KlimovTestData.KLIMOV_DESCRIPTION)
    {
        _random = random ?? new Random();

        Questions = questions;
    }

    public override TestExecutor CreateExecutor()
    {
        List<Question> questions = new List<Question>(20);

        var questionsBuilder = new QuestionsBuilder( questions );

        /// Ознакомиться с методичкой можно здесь: https://www.kurgancollege.ru/upload/docs/test.pdf
        
        // 1
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Ухаживать за животными", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Обслуживать машины и технические приборы", () => ++Scores[(int)TypeKlimov.Technique]);

        // 2
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Помогать больным людям, лечить их", () => ++Scores[(int)TypeKlimov.Man])
            .AddAnswer("Составлять таблицы и схемы", () => ++Scores[(int)TypeKlimov.SignSystem]);

        // 3
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Следить за качеством книжных иллюстраций", () => ++Scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Следить за развитием растениий", () => ++Scores[(int)TypeKlimov.Nature]);

        // 4
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Обрабатывать материалы", () => ++Scores[(int)TypeKlimov.Technique])
            .AddAnswer("Доводить товары до потребителя", () => ++Scores[(int)TypeKlimov.Man]);

        // 5
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Обсуждать научно-популярные книги", () => ++Scores[(int)TypeKlimov.SignSystem])
            .AddAnswer("Обсуждать художественные книги", () => ++Scores[(int)TypeKlimov.ArtisticImage]);

        // 6
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Выращивать молодняк", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Тренировать товарищей", () => ++Scores[(int)TypeKlimov.Man]);

        // 7
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Копировать рисунки и изображения", () => ++Scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Управлять каким-либо грузовым средством", () => ++Scores[(int)TypeKlimov.Technique]);

        // 8
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Сообщать людям нужные им сведения", () => ++Scores[(int)TypeKlimov.Man])
            .AddAnswer("Художественно оформлять выставки", () => ++Scores[(int)TypeKlimov.ArtisticImage]);

        // 9
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Выполнять вычисления", () => ++Scores[(int)TypeKlimov.Technique])
            .AddAnswer("Искать и исправлять ошибки в текстах", () => ++Scores[(int)TypeKlimov.SignSystem]);

        // 10
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Лечить животных", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Выполнять вычисления, расчеты", () => ++Scores[(int)TypeKlimov.SignSystem]);

        // 11
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Выводить новые сорта растений", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Проектировать новые виды пром. изделий", () => ++Scores[(int)TypeKlimov.Technique]);

        // 12
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Разбирать ссоры между людьми", () => ++Scores[(int)TypeKlimov.Man])
            .AddAnswer("Разбираться в чертежа", () => ++Scores[(int)TypeKlimov.SignSystem]);

        // 13
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Изучать работу кружков художественной самодеятельности", () => ++Scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Изучать жизнь микробов", () => ++Scores[(int)TypeKlimov.Nature]);

        // 14
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Налаживать медицинские приборы", () => ++Scores[(int)TypeKlimov.Technique])
            .AddAnswer("Оказывать людям медицинскую помощь", () => ++Scores[(int)TypeKlimov.Man]);

        // 15
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Составлять точные описания о наблюдаемых явлениях", () => ++Scores[(int)TypeKlimov.SignSystem])
            .AddAnswer("Художественно описывать, изображать события", () => ++Scores[(int)TypeKlimov.ArtisticImage]);

        // 16
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Делать лабораторные анализы в больнице", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Осматривать больных и назначать лечение", () => ++Scores[(int)TypeKlimov.Man]);

        // 17
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Красить или расписывать стены помещений", () => ++Scores[(int)TypeKlimov.ArtisticImage])
            .AddAnswer("Осуществлять монтаж зданий", () => ++Scores[(int)TypeKlimov.Technique]);

        // 18
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Организовывать сверстников в театры", () => ++Scores[(int)TypeKlimov.Man])
            .AddAnswer("Играть на сцене", () => ++Scores[(int)TypeKlimov.ArtisticImage]);

        // 19
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Изготовлять по чертежам детали", () => ++Scores[(int)TypeKlimov.Technique])
            .AddAnswer("Заниматься черчением", () => ++Scores[(int)TypeKlimov.SignSystem]);

        // 20
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Вести борьбу с болезнями растений", () => ++Scores[(int)TypeKlimov.Nature])
            .AddAnswer("Работать на клавишных машинах", () => ++Scores[(int)TypeKlimov.SignSystem]);

        return new KlimovTestExecutor( new KlimovTest(questions, _random) );
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