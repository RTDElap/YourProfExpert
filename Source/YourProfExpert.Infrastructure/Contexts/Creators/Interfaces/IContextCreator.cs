

namespace YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

/// <summary>
/// Интерфейс фабричного метода создания контекста базы данных
/// </summary>
public interface IContextCreator
{
    public BaseContext CreateContext();
}