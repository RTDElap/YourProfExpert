#nullable disable

namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Описывает результат прохождения теста пользователем
/// </summary>
public class PassedTest
{
    public int Id { get; set; }

    public User User { get; set; }

    public AvailableTestResult Result { get; set; }
}