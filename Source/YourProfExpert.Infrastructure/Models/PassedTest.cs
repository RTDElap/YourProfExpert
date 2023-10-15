#nullable disable

namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Пройденный пользователем тест
/// </summary>
public class PassedTest
{
    public int Id { get; set; }

    public User User { get; set; }

    public Test Test { get; set; }

    public PossibleTestResult Result { get; set; }
}