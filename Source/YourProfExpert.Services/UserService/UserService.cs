

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YourProfExpert.Core.Services;
using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IContextCreator _creator;

    public UserService(IContextCreator contextCreator, ILogger<UserService> logger)
    {
        _creator = contextCreator;

        _logger = logger;
    }

    public void CreateUser(long userId)
    {
        _logger.LogDebug($"Создание пользователя {userId}");

        using ( var context = _creator.CreateContext() )
        {
            context.Users.Add( new() { Id = userId } );

            context.SaveChanges();
        }
    }

    public async Task CreateUserAsync(long userId)
    {
        _logger.LogDebug($"Создание пользователя {userId}");

        using ( var context = _creator.CreateContext() )
        {
            await context.Users.AddAsync( new() { Id = userId } );

            await context.SaveChangesAsync();
        }
    }

    public bool IsUserExists(long userId)
    {
        _logger.LogDebug($"Проверка существования пользователя: {userId}");

        return _creator.CreateContext()
            .Users
            .Any( u => u.Id == userId );
    }

    public async Task<bool> IsUserExistsAsync(long userId)
    {
        _logger.LogDebug($"Проверка существования пользователя: {userId}");

        return await _creator.CreateContext()
            .Users
            .AnyAsync( u => u.Id == userId );
    }
}