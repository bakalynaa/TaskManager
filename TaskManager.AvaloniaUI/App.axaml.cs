using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Repositories.Repositories;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI;

/// <summary>
/// Точка входу. Налаштовує IoC-контейнер з усіма залежностями.
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();

        // Рівень Repositories
        collection.AddSingleton<IProjectRepository, ProjectRepository>();
        collection.AddSingleton<ITaskRepository, TaskRepository>();

        // Рівень Services (залежать від інтерфейсів Repository — DI)
        collection.AddSingleton<IProjectService, ProjectService>();
        collection.AddSingleton<ITaskService, TaskService>();

        // UI
        collection.AddTransient<MainWindow>();

        Services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();

        base.OnFrameworkInitializationCompleted();
    }
}
