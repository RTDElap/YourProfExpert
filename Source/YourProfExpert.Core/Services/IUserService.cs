

namespace YourProfExpert.Core.Services;

public interface IUserService
{
    // Sync методы

    /// <summary>
    /// Проверяет наличие записи о пользователе в базе данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг, указывающий на существование пользователя</returns>
    public bool IsUserExists(long userId);

    /// <summary>
    /// Создает запись в базе данных
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public void CreateUser(long userId);

    // Async методы

    /// <summary>
    /// Проверяет наличие записи о пользователе в базе данных
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <returns>Флаг, указывающий на существование пользователя</returns>
    public Task<bool> IsUserExistsAsync(long userId);

    /// <summary>
    /// Создает запись в базе данных
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public Task CreateUserAsync(long userId);
}