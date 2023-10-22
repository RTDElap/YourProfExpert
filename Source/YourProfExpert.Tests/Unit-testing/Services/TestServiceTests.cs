using YourProfExpert.TestOfKlimov;

namespace YourProfExpert.Tests;


public class TestService_Tests
{
    [Fact]
    public void GetAvailableTests_with_klimov_test()
    {
        ITestService testService = new TestService( ContextBuilder.CreateBuilder().Build().Object, Logger.CreateMock<TestService>() );

        testService.AddKlimovTest();

        Assert.True
        (
            testService.GetAvailableTests().Count() == 1
        );
    }

    [Fact]
    public void GetTestOrDefault_correct()
    {
        ITestService testService = new TestService( ContextBuilder.CreateBuilder().Build().Object, Logger.CreateMock<TestService>() );

        testService.AddKlimovTest();

        var test = testService.GetTestOrDefault(KlimovTestData.KLIMOV_TITLE);

        Assert.Equal( KlimovTestData.KLIMOV_TITLE, test?.Title );
        Assert.Equal( KlimovTestData.KLIMOV_DESCRIPTION, test?.Description );
    }

    [Fact]
    public void GetTestOrDefault_incorrect()
    {
        ITestService testService = new TestService( ContextBuilder.CreateBuilder().Build().Object, Logger.CreateMock<TestService>() );

        testService.AddKlimovTest();

        var test = testService.GetTestOrDefault("INCORRECT");

        Assert.True( test is null );
    }

    [Fact]
    public void GetUserPassedTestOrDefault_correct()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        var passedTest = testService.GetUserPassedTestOrDefault(1, "Тест");

        Assert.NotNull(passedTest);
        Assert.Equal(1, passedTest.User.Id);
        Assert.Equal("Прошел", passedTest.Result.Name);
        Assert.Equal("Тест", passedTest.Result.Test.Title);
    }

    [Fact]
    public void GetUserPassedTestOrDefault_incorrect_user_id()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        var passedTest = testService.GetUserPassedTestOrDefault(2, "Тест");

        Assert.Null(passedTest);
    }

    [Fact]
    public void GetUserPassedTestOrDefault_incorrect_title()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        var passedTest = testService.GetUserPassedTestOrDefault(2, "IncorrectTestName");

        Assert.Null(passedTest);
    }

    [Fact]
    public void TrySetUserPassedTest_correct_and_new_passed_test()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        bool result = testService.TrySetUserPassedTest(1, "Тест", 1);

        Assert.True( result );
    }

    [Fact]
    public void TrySetUserPassedTest_correct_and_replace_passed_test()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddAvailableTestResult(2, 1, 2, "Прошел2", "Прошел2")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        bool result = testService.TrySetUserPassedTest(1, "Тест", 2);

        Assert.True( result );
    }

    [Fact]
    public void TrySetUserPassedTest_incorrect_title()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        bool result = testService.TrySetUserPassedTest(1, "Тест1", 1);

        Assert.False( result );
    }

    [Fact]
    public void TrySetUserPassedTest_incorrect_order_id()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        bool result = testService.TrySetUserPassedTest(1, "Тест1", 2);

        Assert.False( result );
    }

    [Fact]
    public void TrySetUserPassedTest_incorrect_user_id()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, "Тест")
            .AddAvailableTestResult(1, 1, 1, "Прошел", "Прошел")
            .AddPassedTest(1, 1, 1)
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>());

        bool result = testService.TrySetUserPassedTest(15, "Тест1", 2);

        Assert.False( result );
    }
}