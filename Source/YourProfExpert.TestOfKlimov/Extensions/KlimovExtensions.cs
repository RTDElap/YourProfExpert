
using Microsoft.EntityFrameworkCore;

using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.TestOfKlimov;

public static partial class KlimovExtensions
{
    private static TestInformation AddTestInformationToDatabase(BaseContext context)
    {
        var test = new TestInformation()
        {
            Title = KlimovTestData.KLIMOV_TITLE
        };

        context.Tests.Add( test );

        return test;
    }

    private static bool IsAvailableResultExists(BaseContext context, string name)
    {
        return context
                .AvailableTestResults
                .SingleOrDefault( r => r.Name == name ) != null;
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
    public static void AddKlimovTest(this BaseContext context)
    {
        TestInformation? test = 
            context.Tests.SingleOrDefault
            (   
                t => t.Title == KlimovTestData.KLIMOV_TITLE
            );

        if ( test is null )
            test = AddTestInformationToDatabase(context);

        var klimovData = new KlimovTestData();
        int index = 0;

        foreach ( var availableResult in klimovData.AvailableResults )
        {
            if ( ! IsAvailableResultExists(context, availableResult.Name ) )
            {
                context.AvailableTestResults.Add
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

        context.SaveChanges();
    }
}