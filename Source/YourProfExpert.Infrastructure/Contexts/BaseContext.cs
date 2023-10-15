#nullable disable

using Microsoft.EntityFrameworkCore;

using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Infrastructure.Contexts;

/// <summary>
/// Базовый контекст базы данных для всех видов БД
/// </summary>
public abstract class BaseContext : DbContext
{
    public DbSet<PassedTest> PassedTests { get; set; }
    public DbSet<PossibleTestResult> PossibleTestResults { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<User> Users { get; set; }

    public BaseContext(DbContextOptions options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}