

using YourProfExpert.Core.Tests;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс, описывающий получение и установку значений прохождения теста,
/// в контексте базы данных
/// </summary>
public interface ITestService
{
    public IEnumerable<Test> GetAvailableTests();

    public Test GetTest(string testTitle);

    public void AddTest(Test test);

    // Sync методы

    public void SetUserPassTest(long userId, string testTitle, int orderId);

    public bool GetPassUserTest(long userId, string testTitle);

    // Async методы

    public Task SetUserPassTestAsync(long userId, string testTitle, int orderId);

    public Task<bool> GetPassUserTestAsync(long userId, string testTitle);
}