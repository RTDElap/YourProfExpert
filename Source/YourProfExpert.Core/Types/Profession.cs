#nullable disable

namespace YourProfExpert.Core.Types;

/// <summary>
/// Представляет запись профессии
/// </summary>
public record Profession
{
    public string Name { get; set; }

    public string Description { get; set; }
}