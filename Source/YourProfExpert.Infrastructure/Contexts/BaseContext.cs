#nullable disable

using Microsoft.EntityFrameworkCore;

using YourProfExpert.Infrastructure.Models;

namespace YourProfExpert.Infrastructure.Contexts;

/// <summary>
/// Базовый контекст базы данных для всех видов БД
/// </summary>
public abstract class BaseContext : DbContext
{
    public virtual DbSet<PassedTest> PassedTests { get; set; }
    public virtual DbSet<AvailableTestResult> AvailableTestResults { get; set; }
    public virtual DbSet<TestInformation> Tests { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public BaseContext()
    { }

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