#nullable disable


namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Доступные результаты тестирования
/// </summary>
public class PossibleTestResult
{
    public int Id { get; set; }

    /// <summary>
    /// Эта переменная отвечает за порядковый номер для одного конкретного теста 
    /// </summary>
    /// <value></value>
    public int OrderId { get; set; }

    public Test Test { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}