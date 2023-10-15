

using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;

namespace YourProfExpert.Services;

public class TestService : ITestService
{
    // long = TestExecutor, id пользователя = текущий экземпляр для прохождения теста
    private readonly IDictionary<long, TestExecutor> _userTests;

    private readonly IList<Test> _tests;

    public TestService()
    {
        _tests = new List<Test>();

        _userTests = new Dictionary<long, TestExecutor>();
    }

    public void AddTest(Test test)
    {
        _tests.Add( test );
    }

    public IEnumerable<Test> GetAvailableTests()
    {
        return _tests;
    }

    public Test GetTest(string testTitle)
    {
        return _tests.Single( t => t.Title == testTitle );
    }

    public TestExecutor GetTestExecutor(long userId)
    {
        return _userTests[userId];
    }

    public bool IsUserRunTest(long userId)
    {
        return _userTests.ContainsKey( userId );
    }

    public TestExecutor RunTest(long userId, string testTitle)
    {
        TestExecutor executor = _tests
            .Single(t => t.Title == testTitle)
            .CreateExecutor();

        _userTests[userId] = executor;

        return executor;
    }
}