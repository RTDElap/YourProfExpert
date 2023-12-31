using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public partial class TestService : ITestService
{
    public PassedTest? GetUserPassedTestOrDefault(long userId, string testTitle)
    {
        using ( var context = _creator.CreateContext() )
        {
            _logger.LogDebug($"Попытка получить результат теста {testTitle} пользователя {userId}");

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
            _logger.LogDebug($"Попытка получить результат теста {testTitle} пользователя {userId}");

            return await context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefaultAsync
                ( 
                    p => 
                        p.Result.Test.Title == testTitle &&
                        p.User.Id == userId,
                    token
                );
        }
    }
}