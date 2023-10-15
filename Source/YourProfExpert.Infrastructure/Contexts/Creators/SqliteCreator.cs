

using Microsoft.EntityFrameworkCore;

using YourProfExpert.Infrastructure.Contexts.Creators.Interfaces;

namespace YourProfExpert.Infrastructure.Contexts.Creators;

/// <summary>
/// Фабричный метод создания Sqlite контекста
/// </summary>
public class SqliteCreator : IContextCreator
{
    private readonly DbContextOptions _options;

    public SqliteCreator(DbContextOptions options) =>
        _options = options;

    public SqliteCreator( Action<DbContextOptionsBuilder> builderAction )
    {
        var builder = new DbContextOptionsBuilder();

        builderAction( builder );

        _options = builder.Options;
    }

    public SqliteCreator( string connectionString )
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