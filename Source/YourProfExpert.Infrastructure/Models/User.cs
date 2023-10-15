

namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Пользователь телеграмма
/// </summary>
public class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }
}