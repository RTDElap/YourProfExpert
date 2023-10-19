using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Services;

public partial class TestService : ITestService
{
    private readonly IContextCreator _creator;
    private readonly IList<FunctionalTest> _tests;

    public TestService(IContextCreator creator)
    {
        _creator = creator;
        
        _tests = new List<FunctionalTest>();
    }

    public void AddTest(FunctionalTest test)
    {
        _tests.Add(test);
    }

    public IEnumerable<FunctionalTest> GetAvailableTests()
    {
        return _tests;
    }

    public FunctionalTest? GetTestOrDefault(string testTitle)
    {
        return _tests.SingleOrDefault( t => t.Title == testTitle );
    }
}