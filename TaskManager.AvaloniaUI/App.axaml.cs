using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Repositories.Repositories;
using TaskManager.Repositories.Storage;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI;

/// <summary>
/// Точка входу застосунку. Ініціалізує БД і налаштовує IoC-контейнер.
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();

        // База даних SQLite
        var dbPath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TaskManager", "taskmanager.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbPath)!);

        collection.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Repositories
        collection.AddScoped<IProjectRepository, ProjectRepository>();
        collection.AddScoped<ITaskRepository, TaskRepository>();

        // Services
        collection.AddScoped<IProjectService, ProjectService>();
        collection.AddScoped<ITaskService, TaskService>();

        // UI
        collection.AddTransient<MainWindow>();

        Services = collection.BuildServiceProvider();

        // Ініціалізація БД і seed даних
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
        DatabaseSeeder.SeedAsync(db).GetAwaiter().GetResult();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();

        base.OnFrameworkInitializationCompleted();
    }
}
