using Microsoft.EntityFrameworkCore;
using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Storage;

/// <summary>
/// Контекст бази даних EF Core. Налаштовує таблиці і зв'язки між сутностями.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Каскадне видалення: при видаленні проєкту видаляються всі його завдання
        modelBuilder.Entity<TaskEntity>()
            .HasOne<ProjectEntity>()
            .WithMany()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
