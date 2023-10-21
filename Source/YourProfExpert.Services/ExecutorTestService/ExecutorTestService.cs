using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;

namespace YourProfExpert.Services;

public class ExecutorTestService : IExecutorTestService
{
    // long = int, userId = текущий итератор теста
    private readonly IDictionary<long, TestExecutor> _usersTests;

    private readonly ITestService _testService;
    private readonly ILogger<ExecutorTestService> _logger;

    public ExecutorTestService(ITestService testService, ILogger<ExecutorTestService> logger)
    {
        _testService = testService;

        _logger = logger;

        _usersTests = new Dictionary<long, TestExecutor>();
    }

    public void CloseTest(long userId)
    {
        _logger.LogDebug($"{userId} закрыл тест");

        _usersTests.Remove(userId);
    }

    public IEnumerable<string> GetAnswers(long userId)
    {
        _logger.LogDebug($"{userId} получает списки вопросов для {_usersTests[userId].Test.Title}");

        return _usersTests[userId]
            .CurrentAnswers
            .Select( a => a.Name );
    }

    public int GetCurrentIndexOfQuestion(long userId)
    {
        _logger.LogDebug($"{userId} получает текущий индекс для {_usersTests[userId].Test.Title}");

        return _usersTests[userId]
            .CurrentIndex;
    }

    public string GetQuestion(long userId)
    {
        _logger.LogDebug($"{userId} получает вопрос для {_usersTests[userId].Test.Title}");

        return _usersTests[userId]
            .CurrentQuestion
            .Name;
    }

    public bool IsEnd(long userId)
    {
        _logger.LogDebug($"{userId} проверяет конец {_usersTests[userId].Test.Title}");

        return _usersTests[userId]
            .CanMoveNext();
    }

    public bool IsUserStartTest(long userId)
    {
        _logger.LogDebug($"{userId} проверяет наличие TestExecutor {_usersTests[userId].Test.Title}");

        return _usersTests.ContainsKey(userId);
    }

    public bool TrySelectAnswer(long userId, int index)
    {
        _logger.LogDebug($"{userId} выбирает ответ (индекс {index}) для {_usersTests[userId].Test.Title}");

        if ( ! _usersTests[userId].TrySelectAnswer(index) )
        {
            _logger.LogDebug($"{userId} выбрал вне диапазона в {_usersTests[userId].Test.Title}");

            return false;
        }

        _usersTests[userId]
            .MoveNext();

        return true;
    }

    public void StartTest(long userId, FunctionalTest test)
    {
        _logger.LogDebug($"{userId} начал {test.Title}");

        _usersTests[userId] = test.CreateExecutor();
    }

    public bool SaveResult(long userId)
    {
        var executor = _usersTests[userId];

        _logger.LogDebug($"{userId} сохраняет результат в базу данных {executor.Test.Title}");

        return
            _testService.TrySetUserPassedTest(userId, executor.Test.Title, executor.GetResult() );
    }

    public async Task<bool> SaveResultAsync(long userId, CancellationToken token)
    {
        var executor = _usersTests[userId];

        _logger.LogDebug($"{userId} сохраняет результат в базу данных {executor.Test.Title}");

        return
            await _testService.TrySetUserPassedTestAsync(userId, executor.Test.Title, executor.GetResult(), token );
    }
}