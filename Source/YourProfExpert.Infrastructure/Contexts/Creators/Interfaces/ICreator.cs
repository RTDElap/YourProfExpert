

namespace YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

/// <summary>
/// Интерфейс фабричного метода базы данных
/// </summary>
public interface ICreator
{
    public BaseContext CreateContext();
}