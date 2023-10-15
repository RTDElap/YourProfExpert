
using Microsoft.EntityFrameworkCore;

using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.KlimovTest;

public static class KlimovExtensions
{
    public static ITestService AddKlimovTest(this ITestService testService, Random? random = null)
    {
        testService.AddTest( new KlimovTest(random) );

        return testService;
    }

    /// <summary>
    /// Добавляет в базу данных данные теста, если это необходимо
    /// </summary>
    /// <param name="baseContext"></param>
    public static async Task AddKlimovTestToContextAsync(this BaseContext baseContext)
    {
        Test? test = await baseContext.Tests.SingleOrDefaultAsync(t => t.Title == KlimovTestData.KLIMOV_TITLE);

        // Нет теста Климова в таблице Tests
        if ( test is null )
        {
            test = new Test();

            test.Title = KlimovTestData.KLIMOV_TITLE;

            await baseContext.Tests.AddAsync( test );
        }

        var klimovData = new KlimovTestData();

        foreach ( var (availableResult, index) in Enumerable.Zip(klimovData.AvailableResults, Enumerable.Range(1, KlimovTestData.RESULTS_COUNT) ) )
        {
            var result = await baseContext
                .AvailableTestResults
                .SingleOrDefaultAsync( r => r.Name == availableResult.Name );
        
            if ( result is null )
            {
                await baseContext.AvailableTestResults.AddAsync
                (
                    new AvailableTestResult()
                    {
                        Test = test,
                        OrderId = index,
                        Name = availableResult.Name,
                        Description = availableResult.Description
                    }
                );
            }
        }

        await baseContext.SaveChangesAsync();
    }
}