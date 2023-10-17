

namespace YourProfExpert.Core.Tests;

/// <summary>
/// Представляет данные о тесте и интерфейс для создания итератора по его прохождению (TestExecutor).
/// Приставка Functional указывает на то, что тест является функциональной единицей, доступной для прохождения,
/// когда TestInformation представляешь лишь запись о том, что какой-либо тест существует
/// </summary>
public abstract class FunctionalTest
{
    public string Title;

    public string Description;

    public FunctionalTest(string title, string description)
    {
        Title = title;
        Description = description;
    }

    /// <summary>
    /// Создает итератор, позволяющий пройти тест
    /// </summary>
    /// <returns>Итератор TestExecutor</returns>
    public abstract TestExecutor CreateExecutor();
}