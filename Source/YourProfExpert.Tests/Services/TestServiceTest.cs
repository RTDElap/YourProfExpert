

using Microsoft.EntityFrameworkCore;
using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;
using YourProfExpert.KlimovTest;

namespace YourProfExpert.Tests;

internal class ContextCreatorMockBuilder
{
    private List<User>? _users = new();
    private List<TestInformation>? _testInformations = new();
    private List<AvailableTestResult>? _availableTestResults = new();
    private List<PassedTest>? _passedTests = new();

    public ContextCreatorMockBuilder AddUser(int id)
    {
        if ( _users is null ) _users = new List<User>();

        _users.Add( new User() { Id = id } );

        return this;
    }

    public ContextCreatorMockBuilder AddTestInformation(int id, string title)
    {
        if ( _testInformations is null ) _testInformations = new List<TestInformation>();

        _testInformations.Add( new TestInformation() { Id = id, Title = title } );

        return this;
    }

    public ContextCreatorMockBuilder AddAvailableTestResult(int id, int testInformationId, int orderId , string name, string description)
    {
        if ( _testInformations is null ) throw new ArgumentNullException("");
        if ( _availableTestResults is null ) _availableTestResults = new List<AvailableTestResult>();

        _availableTestResults.Add
        (
            new AvailableTestResult()
            {
                Id = id,
                OrderId = orderId,
                Test =_testInformations.Single(t => t.Id == testInformationId),
                Name = name,
                Description = description
            }
        );

        return this;
    }

    public ContextCreatorMockBuilder AddPassedTest(int id, int userId, int availableTestResultId)
    {
        if ( _users is null ) throw new ArgumentNullException("");
        if ( _availableTestResults is null ) throw new ArgumentNullException("");
        if ( _passedTests is null ) _passedTests = new List<PassedTest>();

        _passedTests.Add
        (
            new PassedTest()
            {
                Id = id,
                Result = _availableTestResults.Single( a => a.Id == availableTestResultId ),
                User = _users.Single(u => u.Id == userId)
            }
        );

        return this;
    }

    public Mock<IContextCreator> Build()
    {
        var mockContext = new Mock<BaseContext>();

        if ( _users is not null )
        {
            {
                var userQueryable = _users.AsQueryable();
            
                var mockSet = new Mock<DbSet<User>>();
                mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userQueryable.Provider);
                mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userQueryable.Expression);
                mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userQueryable.ElementType);
                mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userQueryable.GetEnumerator());

                mockContext.Setup(x => x.Users).Returns(mockSet.Object);
            }
        }

        if ( _testInformations is not null )
        {
            {
                var testInformationQueryable = _testInformations.AsQueryable();
            
                var mockSet = new Mock<DbSet<TestInformation>>();
                mockSet.As<IQueryable<TestInformation>>().Setup(m => m.Provider).Returns(testInformationQueryable.Provider);
                mockSet.As<IQueryable<TestInformation>>().Setup(m => m.Expression).Returns(testInformationQueryable.Expression);
                mockSet.As<IQueryable<TestInformation>>().Setup(m => m.ElementType).Returns(testInformationQueryable.ElementType);
                mockSet.As<IQueryable<TestInformation>>().Setup(m => m.GetEnumerator()).Returns(() => testInformationQueryable.GetEnumerator());

                mockContext.Setup(x => x.Tests).Returns(mockSet.Object);
            }
        }

        if ( _passedTests is not null )
        {
            {
                var passedTestQueryable = _passedTests.AsQueryable();
            
                var mockSet = new Mock<DbSet<PassedTest>>();
                mockSet.As<IQueryable<PassedTest>>().Setup(m => m.Provider).Returns(passedTestQueryable.Provider);
                mockSet.As<IQueryable<PassedTest>>().Setup(m => m.Expression).Returns(passedTestQueryable.Expression);
                mockSet.As<IQueryable<PassedTest>>().Setup(m => m.ElementType).Returns(passedTestQueryable.ElementType);
                mockSet.As<IQueryable<PassedTest>>().Setup(m => m.GetEnumerator()).Returns(() => passedTestQueryable.GetEnumerator());

                mockContext.Setup(x => x.PassedTests).Returns(mockSet.Object);
            }
        }

        if ( _availableTestResults is not null )
        {
            {
                var availableTestResultsQueryable = _availableTestResults.AsQueryable();
            
                var mockSet = new Mock<DbSet<AvailableTestResult>>();
                mockSet.As<IQueryable<AvailableTestResult>>().Setup(m => m.Provider).Returns(availableTestResultsQueryable.Provider);
                mockSet.As<IQueryable<AvailableTestResult>>().Setup(m => m.Expression).Returns(availableTestResultsQueryable.Expression);
                mockSet.As<IQueryable<AvailableTestResult>>().Setup(m => m.ElementType).Returns(availableTestResultsQueryable.ElementType);
                mockSet.As<IQueryable<AvailableTestResult>>().Setup(m => m.GetEnumerator()).Returns(() => availableTestResultsQueryable.GetEnumerator());

                mockContext.Setup(x => x.AvailableTestResults).Returns(mockSet.Object);
            }
        }

        var contextCreatorMocked = new Mock<IContextCreator>();

        contextCreatorMocked.Setup( c => c.CreateContext() )
            .Returns( mockContext.Object );

        return contextCreatorMocked;
    }

    public static ContextCreatorMockBuilder CreateBuilder() =>
        new ContextCreatorMockBuilder();
}

public class TestServiceTest
{
    [Fact]
    public void GetAvailableTests()
    {
        ITestService testService = new TestService( ContextCreatorMockBuilder.CreateBuilder().Build().Object );

        testService.AddKlimovTest();

        Assert.True
        (
            testService.GetAvailableTests().Count() == 1
        );
    }

    [Fact]
    public void GetTestOrDefault_Correct()
    {
        ITestService testService = new TestService( ContextCreatorMockBuilder.CreateBuilder().Build().Object );

        testService.AddKlimovTest();

        var test = testService.GetTestOrDefault(KlimovTestData.KLIMOV_TITLE);

        Assert.Equal( KlimovTestData.KLIMOV_TITLE, test?.Title );
        Assert.Equal( KlimovTestData.KLIMOV_DESCRIPTION, test?.Description );
    }

    [Fact]
    public void GetTestOrDefault_Incorrect()
    {
        ITestService testService = new TestService( ContextCreatorMockBuilder.CreateBuilder().Build().Object );

        testService.AddKlimovTest();

        var test = testService.GetTestOrDefault("INCORRECT");

        Assert.True( test is null );
    }

    [Fact]
    public void GetUserPassedTestOrDefault_Correct()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        var passedTest = testService.GetUserPassedTestOrDefault(1, "Тест");

        Assert.NotNull(passedTest);
        Assert.Equal(1, passedTest.User.Id);
        Assert.Equal("Прошел", passedTest.Result.Name);
        Assert.Equal("Тест", passedTest.Result.Test.Title);
    }

    [Fact]
    public void GetUserPassedTestOrDefault_IncorrectUserId()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        var passedTest = testService.GetUserPassedTestOrDefault(2, "Тест");

        Assert.Null(passedTest);
    }

    [Fact]
    public void GetUserPassedTestOrDefault_IncorrectTitle()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        var passedTest = testService.GetUserPassedTestOrDefault(2, "IncorrectTestName");

        Assert.Null(passedTest);
    }

    [Fact]
    public void TrySetUserPassedTest_Correct_and_NewPassedTest()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        bool result = testService.TrySetUserPassedTest(1, "Тест", 1);

        Assert.True( result );
    }

    [Fact]
    public void TrySetUserPassedTest_Correct_and_ReplacePassedTest()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddAvailableTestResult(2, 1, 2, "Прошел2", "Прошел2")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        bool result = testService.TrySetUserPassedTest(1, "Тест", 2);

        Assert.True( result );
    }

    [Fact]
    public void TrySetUserPassedTest_Incorrect_Title()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        bool result = testService.TrySetUserPassedTest(1, "Тест1", 1);

        Assert.False( result );
    }

    [Fact]
    public void TrySetUserPassedTest_Incorrect_OrderId()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        bool result = testService.TrySetUserPassedTest(1, "Тест1", 2);

        Assert.False( result );
    }

    [Fact]
    public void TrySetUserPassedTest_Incorrect_UserId()
    {
        var creatorMock = ContextCreatorMockBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object);

        bool result = testService.TrySetUserPassedTest(15, "Тест1", 2);

        Assert.False( result );
    }
}