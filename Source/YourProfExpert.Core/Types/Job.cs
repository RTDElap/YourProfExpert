#nullable disable

namespace YourProfExpert.Core.Types;

/// <summary>
/// Представляет запись специальности
/// </summary>
public record Job
{
    public string Name { get; set; }

    public string Description { get; set; }
}