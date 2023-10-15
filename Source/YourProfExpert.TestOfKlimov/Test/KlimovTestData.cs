

namespace YourProfExpert.KlimovTest;

/// <summary>
/// Вспомогательный класс с описанием и кучей текста
/// </summary>
public class KlimovTestData
{
    public const int RESULTS_COUNT = 5;

    public const string KLIMOV_TITLE = "Климова";
    public const string KLIMOV_DESCRIPTION = 
        "Этот тест поможет вам определить группу профессий, " +
        "которая подойдет вам лучше всего.\n\n<b>Инструкция</b>" + 
        "\n«Допустим, вы можете выбрать любую из <b>двух деятельностей</b> " +
        "(в каждой из 20 пар), какую бы вы предпочли?»";

    public (string Name, string Description)[] AvailableResults;

    public KlimovTestData()
    {
        AvailableResults = new (string Name, string Description) [RESULTS_COUNT];

        AvailableResults[0] = 
        (
            Name: "Человек-природа",
            Description: "Сюда входят профессии, в которых человек имеет дело" + 
            "с различными явлениями неживой и живой природы, <b>например</b>:" +
            "\n  • биолог\n  • географ\n  • геолог\n  • физик\n  • химик и "+ 
            "другие профессии, относящиеся к разряду естественных наук."
        );

        AvailableResults[1] = 
        (
            Name: "Человек-техника",
            Description: "В эту группу профессий включены различные виды трудовой" + 
            "деятельности, в которых человек имеет дело с техникой, ее использованием" + 
            "или конструированием, <b>например, профессии</b>:\n  • инженера\n  • оператора" + 
            "\n  • машиниста\n  • механизатора\n  • сварщика."
        );

        AvailableResults[2] = 
        (
            Name: "Человек-знаковая система",
            Description: "Суть профессий типа «человек-знаковая система» состоит в том, чтобы" + 
            "накапливать определенную информацию, перерабатывать ее, хранить и донести до" + 
            "конечного потребителя, оказать помощь в обмене этой информацией. <b>Примеры профессий</b>" + 
            "типа «человек-знаковая система» по методике Климова:\n  • логист \n  • инженер-конструктор" + 
            "\n  • культуролог\n  • тестировщик ПО\n  • бизнес-аналитик\n  • бухгалтер\n  • архитектор."
        );

        AvailableResults[3] = 
        (
            Name: "Человек-искусство",
            Description: "Эта группа профессий является разнообразными видами художественно-творческого труда," + 
            "<b>например</b>: \n  • литература\n  • музыка\n  • театр\n  • изобразительное искусство."
        );

        AvailableResults[4] = 
        (
            Name: "Человек-человек",
            Description: "Сюда включены все виды профессий, предусматривающих взаимодействие людей" +  
            ", <b>например</b>: \n  • политика\n  • религия\n  • педагогика\n  • психология" + 
            "\n  • медицина\n  • торговля\n  • право."
        );
    }
}