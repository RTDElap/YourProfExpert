
using Microsoft.EntityFrameworkCore;

using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.TestOfKlimov;

public static partial class KlimovExtensions
{
    private static async Task<TestInformation> AddTestInformationToDatabaseAsync(BaseContext context)
    {
        var test = new TestInformation()
        {
            Title = KlimovTestData.KLIMOV_TITLE
        };

        await context.Tests.AddAsync( test );

        return test;
    }

    private static async Task<bool> IsAvailableResultExistsAsync(BaseContext context, string name)
    {
        return await context
                .AvailableTestResults
                .SingleOrDefaultAsync( r => r.Name == name ) != null;
    }

    /// <summary>
    /// Добавляет в базу данных данные теста, если это необходимо
    /// </summary>
    /// <param name="context"></param>
    public static async Task AddKlimovTestAsync(this BaseContext context)
    {
        TestInformation? test = 
            await context.Tests.SingleOrDefaultAsync
            (   
                t => t.Title == KlimovTestData.KLIMOV_TITLE
            );

        if ( test is null )
            test = await AddTestInformationToDatabaseAsync(context);

        var klimovData = new KlimovTestData();
        int index = 0;

        foreach ( var availableResult in klimovData.AvailableResults )
        {
            if ( ! await IsAvailableResultExistsAsync(context, availableResult.Name ) )
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

        await context.DisposeAsync();
    }
}