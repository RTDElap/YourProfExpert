

namespace YourProfExpert.Core.Services;

public interface IUserService
{
    public void RegisterUser(long userId);
    public bool UserExists(long userId);


    public Task RegisterUserAsync(long userId);
    public Task<bool> UserExistsAsync(long userId);
}