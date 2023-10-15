#nullable disable


namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Доступные результаты тестирования
/// </summary>
public class PossibleTestResult
{
    public int Id { get; set; }

    public Test Test { get; set; }

    public string Value { get; set; }

    public string Description { get; set; }
}