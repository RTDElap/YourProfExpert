using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public partial class TestService : ITestService
{
    public bool TrySetUserPassedTest(long userId, string testTitle, int orderId)
    {
        using ( var context = _creator.CreateContext() )
        {
            _logger.LogDebug($"Попытка установить результат теста {testTitle} (результат с номером {orderId}) для пользователя {userId}");

            var user = context
                .Users
                .SingleOrDefault(u => u.Id == userId);

            if ( user is null )
            {
                _logger.LogDebug($"Пользователь {userId} не найден");
            
                return false;
            }    

            var availableResult = context
                .AvailableTestResults
                .Include(a => a.Test)
                .SingleOrDefault
                (
                    a => 
                        a.Test.Title == testTitle &&
                        a.OrderId == orderId
                );

            if ( availableResult is null )
            {
                _logger.LogDebug($"Не найден AvailableResult теста {testTitle} с порядковым номером {orderId}");

                return false;
            }

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
                _logger.LogDebug($"Добавление нового результата для пользователя {userId}");

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
                _logger.LogDebug($"Замена результата для пользователя {userId}");

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
            _logger.LogDebug($"Попытка установить результат теста {testTitle} (результат с номером {orderId}) для пользователя {userId}");

            var user = await context
                .Users
                .SingleOrDefaultAsync(u => u.Id == userId, token);

            if ( user is null )
            {
                _logger.LogDebug($"Пользователь {userId} не найден");

                return false;
            }

            var availableResult = await context
                .AvailableTestResults
                .Include(a => a.Test)
                .SingleOrDefaultAsync
                (
                    a => 
                        a.Test.Title == testTitle &&
                        a.OrderId == orderId,
                    token
                );

            if ( availableResult is null )
            {
                _logger.LogDebug($"Не найден AvailableResult теста {testTitle} с порядковым номером {orderId}");

                return false;
            }

            var passedTestOfUser = await context
                .PassedTests
                .Include(p => p.User)
                .Include(p => p.Result)
                .Include(p => p.Result.Test)
                .SingleOrDefaultAsync
                (
                    p =>
                        p.User.Id == userId &&
                        p.Result.Test.Title == testTitle,
                    token
                );

            // Если пользователь не проходил тест, добавить
            if ( passedTestOfUser is null )
            {
                _logger.LogDebug($"Добавление нового результата для пользователя {userId}");

                await context
                    .PassedTests
                    .AddAsync
                    (
                        new PassedTest()
                        {
                            User = user,
                            Result = availableResult
                        },
                        token
                    );
            }
            // Если пользователь существует, то обновить результат
            else
            {
                _logger.LogDebug($"Замена результата для пользователя {userId}");

                passedTestOfUser
                    .Result = availableResult;
            }

            await context.SaveChangesAsync(token);
        }

        return true;
    }
}
