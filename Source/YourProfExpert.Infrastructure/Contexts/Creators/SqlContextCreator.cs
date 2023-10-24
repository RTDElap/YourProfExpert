

using Microsoft.EntityFrameworkCore;

using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Infrastructure.Contexts.Creators;

/// <summary>
/// Фабричный метод создания SqlServerContext контекста
/// </summary>
public class SqlServerContextCreator : IContextCreator
{
    private readonly DbContextOptions _options;

    public SqlServerContextCreator(DbContextOptions options) =>
        _options = options;

    /// <summary>
    /// Конструктор контекста с конфигурированием через лямбду
    /// </summary>
    /// <param name="builderAction">Лямбда с настройкой контекста</param>
    public SqlServerContextCreator( Action<DbContextOptionsBuilder> builderAction )
    {
        var builder = new DbContextOptionsBuilder();

        builderAction( builder );

        _options = builder.Options;
    }

    /// <summary>
    /// Конструктор контекста, подключаемый по connectionString
    /// </summary>
    /// <param name="connectionString"></param>
    public SqlServerContextCreator( string connectionString )
    {
        var builder = new DbContextOptionsBuilder();

        builder.UseSqlServer( connectionString );

        _options = builder.Options;
    }

    public BaseContext CreateContext()
    {
        return new SqlServerContext( _options );
    }
}