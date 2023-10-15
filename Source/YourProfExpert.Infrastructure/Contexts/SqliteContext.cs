

using Microsoft.EntityFrameworkCore;

namespace YourProfExpert.Infrastructure.Contexts;

public class SqliteContext : BaseContext
{
    public SqliteContext(DbContextOptions options) : base(options)
    {
        
    }
}