

using Microsoft.EntityFrameworkCore;

using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Infrastructure.Contexts.Creators;

/// <summary>
/// Фабричный метод создания Sqlite контекста
/// </summary>
public class SqliteContextCreator : IContextCreator
{
    private readonly DbContextOptions _options;

    public SqliteContextCreator(DbContextOptions options) =>
        _options = options;

    /// <summary>
    /// Конструктор контекста с конфигурированием через лямбду
    /// </summary>
    /// <param name="builderAction">Лямбда с настройкой контекста</param>
    public SqliteContextCreator( Action<DbContextOptionsBuilder> builderAction )
    {
        var builder = new DbContextOptionsBuilder();

        builderAction( builder );

        _options = builder.Options;
    }

    /// <summary>
    /// Конструктор контекста, подключаемый по connectionString
    /// </summary>
    /// <param name="connectionString"></param>
    public SqliteContextCreator( string connectionString )
    {
        var builder = new DbContextOptionsBuilder();

        builder.UseSqlite( connectionString );

        _options = builder.Options;
    }

    public BaseContext CreateContext()
    {
        return new SqliteContext( _options );
    }
}