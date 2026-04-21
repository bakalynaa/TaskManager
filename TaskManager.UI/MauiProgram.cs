using Microsoft.Extensions.Logging;
using TaskManager.Services;

namespace TaskManager.UI;

/// <summary>
/// Точка входу MAUI застосунку.
/// Тут реєструються сервіси через IoC-контейнер (Dependency Injection).
/// </summary>
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Реєстрація сервісів через DI-контейнер
        // ITaskRepository -> TaskRepository: UI залежить від інтерфейсу, а не від реалізації
        builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

        // Реєстрація сторінок для DI
        builder.Services.AddTransient<ProjectsPage>();
        builder.Services.AddTransient<ProjectDetailPage>();
        builder.Services.AddTransient<TaskDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
