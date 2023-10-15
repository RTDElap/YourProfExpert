

using YourProfExpert.Core.Tests;

namespace YourProfExpert.Core.Services;

/// <summary>
/// Интерфейс описывает методы, необходимые для прохождения теста.
/// Имплементация интерфейса должна хранить состояние для каждого пользователя,
/// который решил пройти тест.
/// </summary>
public interface ITestService
{
    public IEnumerable<Test> GetAvailableTests();

    public Test GetTest(string testTitle);

    public TestExecutor RunTest(long userId, string testTitle);

    public TestExecutor GetTestExecutor(long userId);

    public bool IsUserRunTest(long userId);
}