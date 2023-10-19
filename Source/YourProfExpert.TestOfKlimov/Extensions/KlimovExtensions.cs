
using Microsoft.EntityFrameworkCore;

using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.TestOfKlimov;

public static class KlimovExtensions
{
    private static async Task<TestInformation> AddTestInformationToDatabase(BaseContext context)
    {
        var test = new TestInformation()
        {
            Title = KlimovTestData.KLIMOV_TITLE
        };

        await context.Tests.AddAsync( test );

        return test;
    }

    private static async Task<bool> IsAvailableResultExists(BaseContext context, string name)
    {
        return await context
                .AvailableTestResults
                .SingleOrDefaultAsync( r => r.Name == name ) != null;
    }

    /// <summary>
    /// Добавляет в ITestService тест Климова, что делает его доступным для прохождения
    /// </summary>
    /// <param name="testService">Любой объект типа TestService</param>
    /// <param name="random">Объект Random для случайного выбора вопроса</param>
    /// <returns>Объект, к которому был применен</returns>
    public static ITestService AddKlimovTest(this ITestService testService, Random? random = null)
    {
        testService.AddTest( new KlimovTest(random) );

        return testService;
    }

    /// <summary>
    /// Добавляет в базу данных данные теста, если это необходимо
    /// </summary>
    /// <param name="context"></param>
    public static async Task AddKlimovTest(this BaseContext context)
    {
        TestInformation? test = 
            await context.Tests.SingleOrDefaultAsync
            (   
                t => t.Title == KlimovTestData.KLIMOV_TITLE
            );

        if ( test is null )
            test = await AddTestInformationToDatabase(context);

        var klimovData = new KlimovTestData();
        int index = 0;

        foreach ( var availableResult in klimovData.AvailableResults )
        {
            if ( ! await IsAvailableResultExists(context, availableResult.Name ) )
            {
                await context.AvailableTestResults.AddAsync
                (
                    new AvailableTestResult()
                    {
                        Test = test,
                        OrderId = ++index,
                        Name = availableResult.Name,
                        Description = availableResult.Description
                    }
                );
            }
        }

        await context.SaveChangesAsync();
    }
}