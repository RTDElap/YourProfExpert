using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Services;

public partial class TestService : ITestService
{
    private readonly IContextCreator _creator;
    private readonly IList<FunctionalTest> _tests;
    private readonly ILogger<TestService> _logger;

    public TestService(IContextCreator creator, ILogger<TestService> logger)
    {
        _creator = creator;
        
        _logger = logger;

        _tests = new List<FunctionalTest>();
    }

    public void AddTest(FunctionalTest test)
    {
        _tests.Add(test);
    }

    public IEnumerable<FunctionalTest> GetAvailableTests()
    {
        _logger.LogDebug("Возвращает список всех тестов");

        return _tests;
    }

    public FunctionalTest? GetTestOrDefault(string testTitle)
    {
        _logger.LogDebug($"Попытка получить тест {testTitle}");

        return _tests.SingleOrDefault( t => t.Title == testTitle );
    }
}