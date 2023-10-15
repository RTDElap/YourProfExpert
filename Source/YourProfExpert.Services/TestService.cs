using Microsoft.EntityFrameworkCore;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public class TestService : ITestService
{
    private readonly IContextCreator _creator;

    private readonly IList<Test> _tests;

    public TestService(IContextCreator creator)
    {
        _creator = creator;

        _tests = new List<Test>();
    }

    public void AddTest(Test test)
    {
        _tests.Add( test );
    }

    public IEnumerable<Test> GetAvailableTests()
    {
        return _tests;
    }

    public Test GetTest(string testTitle)
    {
        return _tests.Single( t => t.Title == testTitle );
    }

    public PassedTest? GetUserPassedTest(long userId, string testTitle)
    {
        using ( var context = _creator.CreateContext() )
        {
            var passedTest = context.PassedTests
                .Include(ps => ps.User)
                .Include(ps => ps.Result)
                .Include(ps => ps.Result.Test)
                .SingleOrDefault( ps => ps.Result.Test.Title == testTitle && ps.User.Id == userId );

            return passedTest;
        }
    }

    public async Task<PassedTest?> GetUserPassedTestAsync(long userId, string testTitle)
    {
        using ( var context = _creator.CreateContext() )
        {
            var passedTest = await context.PassedTests
                .Include(ps => ps.User)
                .Include(ps => ps.Result)
                .Include(ps => ps.Result.Test)
                .SingleOrDefaultAsync( ps => ps.Result.Test.Title == testTitle && ps.User.Id == userId );

            return passedTest;
        }
    }

    public void SetUserPassedTest(long userId, string testTitle, int orderId)
    {
        using ( var context = _creator.CreateContext() )
        {
            var passedTest = context.PassedTests
                .Include(ps => ps.User)
                .Include(ps => ps.Result)
                .Include(ps => ps.Result.Test)
                .SingleOrDefault( ps => ps.Result.Test.Title == testTitle && ps.User.Id == userId );

            var availableResult = context
                .AvailableTestResults
                .Include(r => r.Test)
                .Single( r => r.OrderId == orderId && r.Test.Title == testTitle );

            if ( passedTest is not null )
            {
                passedTest.Result = availableResult;
            }
            else
            {
                var user = context
                    .Users
                    .Single(u => u.Id == userId);

                context.PassedTests
                .Add
                (
                    new ()
                    {
                        User = user,
                        Result = availableResult
                    }
                );
            }

            context.SaveChanges();
        }
    }

    public async Task SetUserPassedTestAsync(long userId, string testTitle, int orderId)
    {
        using ( var context = _creator.CreateContext() )
        {
            var passedTest = await context.PassedTests
                .Include(ps => ps.User)
                .Include(ps => ps.Result)
                .Include(ps => ps.Result.Test)
                .SingleOrDefaultAsync( ps => ps.Result.Test.Title == testTitle && ps.User.Id == userId );

            var availableResult = await context
                .AvailableTestResults
                .Include(r => r.Test)
                .SingleAsync( r => r.OrderId == orderId && r.Test.Title == testTitle );

            if ( passedTest is not null )
            {
                passedTest.Result = availableResult;
            }
            else
            {
                var user = await context
                    .Users
                    .SingleAsync(u => u.Id == userId);

                await context.PassedTests
                .AddAsync
                (
                    new ()
                    {
                        User = user,
                        Result = availableResult
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}