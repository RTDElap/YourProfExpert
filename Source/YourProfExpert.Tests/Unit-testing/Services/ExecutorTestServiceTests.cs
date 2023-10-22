using YourProfExpert.TestOfKlimov;

namespace YourProfExpert.Tests;


public class ExecutorTestService_Tests
{
    [Fact]
    public void GetCurrentIndexOfQuestion_index_is_3()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .Build();

        creatorMock.Object.CreateContext()
           .AddKlimovTest();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>())
            .AddKlimovTest();

        IExecutorTestService executorTestService = new ExecutorTestService(testService, Logger.CreateMock<ExecutorTestService>());

        var test = testService.GetTestOrDefault( KlimovTestData.KLIMOV_TITLE );
        
        Assert.NotNull(test);

        executorTestService.StartTest( 1, test);

        for ( int i = 0; i < 3; ++i ) 
        {
            Assert.True( executorTestService.TrySelectAnswer(1, 1) );
        }

        Assert.Equal( 3, executorTestService.GetCurrentIndexOfQuestion(1) );
    }

    [Fact]
    public void SaveResult()
    {
        var creatorMock = ContextBuilder.CreateBuilder()
            .AddUser(1)
            .AddTestInformation(1, KlimovTestData.KLIMOV_TITLE)
            .AddAvailableTestResult(1, 1, 1, "Природа", "")
            .AddAvailableTestResult(2, 1, 2, "Техника", "")
            .AddAvailableTestResult(3, 1, 3, "Знак", "")
            .AddAvailableTestResult(4, 1, 4, "Человек", "")
            .Build();

        ITestService testService = new TestService(creatorMock.Object, Logger.CreateMock<TestService>())
            .AddKlimovTest();

        IExecutorTestService executorTestService = new ExecutorTestService(testService, Logger.CreateMock<ExecutorTestService>());

        var test = testService.GetTestOrDefault( KlimovTestData.KLIMOV_TITLE );
        
        Assert.NotNull(test);

        executorTestService.StartTest( 1, test);

        while ( executorTestService.IsEnd(1) )
        {
            executorTestService.TrySelectAnswer(1, 1);
        }

        bool result = executorTestService.SaveResult(1);

        Assert.Equal( 20, executorTestService.GetCurrentIndexOfQuestion(1) );
        Assert.True(result);
    }
}