using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Tests.Mocks;

public partial class ContextBuilder
{
    private readonly List<User> _users = new();
    private readonly List<TestInformation> _testInformations = new();
    private readonly List<AvailableTestResult> _availableTestResults = new();
    private readonly List<PassedTest> _passedTests = new();

    public Mock<IContextCreator> Build()
    {

#region Мокинг контекста базы данных
        var mockContext = new Mock<BaseContext>();

        mockContext
            .Setup(c => c.Users)
            .Returns( CreateMockSet<User>(_users) );
        
        mockContext
            .Setup(c => c.Tests)
            .Returns( CreateMockSet<TestInformation>(_testInformations) );

        mockContext
            .Setup(c => c.AvailableTestResults)
            .Returns( CreateMockSet<AvailableTestResult>(_availableTestResults) );

        mockContext
            .Setup(c => c.PassedTests)
            .Returns( CreateMockSet<PassedTest>(_passedTests) );
#endregion

        var contextCreatorMocked = new Mock<IContextCreator>();

        contextCreatorMocked.Setup( c => c.CreateContext() )
            .Returns( mockContext.Object );

        return contextCreatorMocked;
    }

    public static ContextBuilder CreateBuilder() =>
        new ContextBuilder();
}