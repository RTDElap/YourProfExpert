



using Microsoft.EntityFrameworkCore;
using YourProfExpert.Core.Services;
using YourProfExpert.Core.Tests;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public class TestService : ITestService
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

    public PassedTest? GetUserPassedTestOrDefault(long userId, string testTitle)
    {
        using ( var context = _creator.CreateContext() )
        {
            return context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefault
                ( 
                    p => 
                        p.Result.Test.Title == testTitle &&
                        p.User.Id == userId
                );
        }
    }

    public async Task<PassedTest?> GetUserPassedTestOrDefaultAsync(long userId, string testTitle, CancellationToken token)
    {
        using ( var context = _creator.CreateContext() )
        {
            return await context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefaultAsync
                ( 
                    p => 
                        p.Result.Test.Title == testTitle &&
                        p.User.Id == userId
                );
        }
    }

    public bool TrySetUserPassedTest(long userId, string testTitle, int orderId)
    {
        using ( var context = _creator.CreateContext() )
        {
            var user = context
                .Users
                .SingleOrDefault(u => u.Id == userId);

            if ( user is null ) return false;    

            var availableResult = context
                .AvailableTestResults
                .Include(a => a.Test)
                .SingleOrDefault
                (
                    a => 
                        a.Test.Title == testTitle &&
                        a.OrderId == orderId
                );

            if ( availableResult is null ) return false;

            var passedTestOfUser = context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefault
                (
                    p =>
                        p.User.Id == userId &&
                        p.Result.Test.Title == testTitle
                );

            // Если пользователь не проходил тест, добавить
            if ( passedTestOfUser is null )
            {
                context
                    .PassedTests
                    .Add
                    (
                        new PassedTest()
                        {
                            User = user,
                            Result = availableResult
                        }
                    );
            }
            // Если пользователь существует, то обновить результат
            else
            {
                passedTestOfUser
                    .Result = availableResult;
            }

            context.SaveChanges();
        }

        return true;
    }

    public async Task<bool> TrySetUserPassedTestAsync(long userId, string testTitle, int orderId, CancellationToken token)
    {
        using ( var context = _creator.CreateContext() )
        {
            var user = await context
                .Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            if ( user is null ) return false;    

            var availableResult = await context
                .AvailableTestResults
                .Include(a => a.Test)
                .SingleOrDefaultAsync
                (
                    a => 
                        a.Test.Title == testTitle &&
                        a.OrderId == orderId
                );

            if ( availableResult is null ) return false;

            var passedTestOfUser = await context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefaultAsync
                (
                    p =>
                        p.User.Id == userId &&
                        p.Result.Test.Title == testTitle
                );

            // Если пользователь не проходил тест, добавить
            if ( passedTestOfUser is null )
            {
                await context
                    .PassedTests
                    .AddAsync
                    (
                        new PassedTest()
                        {
                            User = user,
                            Result = availableResult
                        }
                    );
            }
            // Если пользователь существует, то обновить результат
            else
            {
                passedTestOfUser
                    .Result = availableResult;
            }

            await context.SaveChangesAsync();
        }

        return true;
    }
}