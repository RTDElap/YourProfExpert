

namespace YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

/// <summary>
/// Интерфейс фабричного метода базы данных
/// </summary>
public interface IContextCreator
{
    public BaseContext CreateContext();
}