

using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Models;

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

    public void SetUserPassedTest(long userId, string testTitle, int orderId);

    public PassedTest? GetUserPassedTest(long userId, string testTitle);

    // Async методы

    public Task SetUserPassedTestAsync(long userId, string testTitle, int orderId);

    public Task<PassedTest?> GetUserPassedTestAsync(long userId, string testTitle);
}