

using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Core.Services;

/// <summary>
/// ITestService описывает взаимодействие с зарегистрированными тестами
/// 
/// Sync и Async методы записывают результат в базу данных
/// </summary>
public interface ITestService
{
    /// <summary>
    /// Представляет все зарегистрированные тесты
    /// </summary>
    /// <returns>Список всех зарегистрированных тестов</returns>
    public IEnumerable<FunctionalTest> GetAvailableTests();

    /// <summary>
    /// Возвращает тест по его названию или null, если не находит
    /// </summary>
    /// <param name="testTitle">Заголовок теста</param>
    /// <returns>Тест, если найден; null, если не найден</returns>
    public FunctionalTest? GetTestOrDefault(string testTitle);

    /// <summary>
    /// Добавляет тест 
    /// </summary>
    /// <param name="test">Тест для добавления</param>
    public void AddTest(FunctionalTest test);

    // Sync методы

    /// <summary>
    /// Устанавливает результат прохождения в базу данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="testTitle">Заголовок теста</param>
    /// <param name="orderId">Порядковый номер результата теста</param>
    public void SetUserPassedTest(long userId, string testTitle, int orderId);

    /// <summary>
    /// Возвращает результат пройденного теста из базы данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="testTitle">Заголовок теста</param>
    /// <returns>PassedTest, если пользователь проходил тест; null, если не проходил</returns>
    public PassedTest? GetUserPassedTestOrDefault(long userId, string testTitle);

    // Async методы

    /// <summary>
    /// Устанавливает результат прохождения в базу данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="testTitle">Заголовок теста</param>
    /// <param name="orderId">Порядковый номер результата теста</param>
    public Task SetUserPassedTestAsync(long userId, string testTitle, int orderId, CancellationToken token);

    /// <summary>
    /// Возвращает результат пройденного теста из базы данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="testTitle">Заголовок теста</param>
    /// <returns>PassedTest, если пользователь проходил тест; null, если не проходил</returns>
    public Task<PassedTest?> GetUserPassedTestOrDefaultAsync(long userId, string testTitle, CancellationToken token);
}