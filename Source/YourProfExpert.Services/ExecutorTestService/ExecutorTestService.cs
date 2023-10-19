using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;

namespace YourProfExpert.Services;

public class ExecutorTestService : IExecutorTestService
{
    // long = int, userId = текущий итератор теста
    private readonly IDictionary<long, TestExecutor> _usersTests;

    private readonly ITestService _testService;

    public ExecutorTestService(ITestService testService)
    {
        _testService = testService;

        _usersTests = new Dictionary<long, TestExecutor>();
    }

    public void CloseTest(long userId)
    {
        _usersTests.Remove(userId);
    }

    public IEnumerable<string> GetAnswers(long userId)
    {
        return _usersTests[userId]
            .CurrentAnswers
            .Select( a => a.Name );
    }

    public int GetCurrentIndexOfQuestion(long userId)
    {
        return _usersTests[userId]
            .CurrentIndex;
    }

    public string GetQuestion(long userId)
    {
        return _usersTests[userId]
            .CurrentQuestion
            .Name;
    }

    public bool IsEnd(long userId)
    {
        return _usersTests[userId]
            .CanMoveNext();
    }

    public bool IsUserStartTest(long userId)
    {
        return _usersTests.ContainsKey(userId);
    }

    public bool TrySelectAnswer(long userId, int index)
    {
        if ( ! _usersTests[userId].TrySelectAnswer(index) )
            return false;

        _usersTests[userId]
            .MoveNext();

        return true;
    }

    public void StartTest(long userId, FunctionalTest test)
    {
        _usersTests[userId] = test.CreateExecutor();
    }

    public bool SaveResult(long userId)
    {
        var executor = _usersTests[userId];

        return
            _testService.TrySetUserPassedTest(userId, executor.Test.Title, executor.GetResult() );
    }

    public async Task<bool> SaveResultAsync(long userId, CancellationToken token)
    {
        var executor = _usersTests[userId];

        return
            await _testService.TrySetUserPassedTestAsync(userId, executor.Test.Title, executor.GetResult(), token );
    }
}