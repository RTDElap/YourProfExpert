

using Microsoft.EntityFrameworkCore;
using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Infrastructure.Contexts;

/// <summary>
/// Контекст базы данных для Microsoft SQL Server
/// </summary>
public class SqlServerContext : BaseContext
{
    public SqlServerContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ef Core автоматически добавляет к Primary Key свойство Identity,
        // которое, в случае с MS SQL Server, не позволяет указать Id самостоятельно
        // без специальной команды Transact SQL (см. https://learn.microsoft.com/ru-ru/sql/t-sql/statements/set-identity-insert-transact-sql?view=sql-server-ver16)
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedNever();

        base.OnModelCreating(modelBuilder);
    }
}