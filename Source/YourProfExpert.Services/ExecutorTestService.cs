using Microsoft.EntityFrameworkCore;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;

namespace YourProfExpert.Services;

public class ExecutorTestService : IExecutorTestService
{
    // long = TestExecutor, id пользователя = текущий экземпляр testExecutor
    private readonly IDictionary<long, TestExecutor> _userTests;

    private readonly ITestService _testService;

    public ExecutorTestService(ITestService testService)
    {
        _testService = testService;

        _userTests = new Dictionary<long, TestExecutor>();
    }

    public void CloseTest(long userId)
    {
        _userTests.Remove(userId);
    }

    public IEnumerable<string> GetAnswers(long userId)
    {
        return 
            _userTests[userId]
            .CurrentAnswers
            .Select(a => a.Name);
    }

    public int GetCurrentIndex(long userId)
    {
        return _userTests[userId].CurrentIndex;
    }

    public string GetQuestion(long userId)
    {
        return _userTests[userId].CurrentQuestion.Name;
    }

    public bool IsEnd(long userId)
    {
        return _userTests[userId].CanMoveNext();
    }

    public void SaveResult(long userId)
    {
        var orderId = _userTests[userId].GetResult();

        _testService.SetUserPassTest( userId, _userTests[userId].Test.Title, orderId );
    }

    public async void SaveResultAsync(long userId)
    {
        var orderId = _userTests[userId].GetResult();

        await _testService.SetUserPassTestAsync( userId, _userTests[userId].Test.Title, orderId );
    }

    public void StartTest(long userId, Test test)
    {
        _userTests[userId] = test.CreateExecutor();
    }
}