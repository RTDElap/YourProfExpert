

using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Core.Services;

public interface IUserService
{
    // Sync методы

    public User GetUser(long id);

    public bool UserExists(long id);

    public User CreateUser(long id);

    public User UpdateUser
    (
        long id,
        string? name = null,
        string? surname = null
    );

    // Async методы

    public Task<User> GetUserAsync(long id);

    public Task<bool> UserExistsAsync(long id);

    public Task<User> CreateUserAsync(long id);

    public Task<User> UpdateUserAsync
    (
        long userId, 
        string? name = null, 
        string? surname = null
    );
}