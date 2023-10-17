#nullable disable


namespace YourProfExpert.Infrastructure.Models;

/// <summary>
/// Описывает доступные результаты прохождения теста
/// </summary>
public class AvailableTestResult
{
    public int Id { get; set; }

    /// <summary>
    /// Эта переменная отвечает за порядковый номер результата для одного конкретного теста 
    /// </summary>
    /// <value></value>
    public int OrderId { get; set; }

    public TestInformation Test { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}