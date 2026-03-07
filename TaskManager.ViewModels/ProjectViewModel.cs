using TaskManager.Models;

namespace TaskManager.ViewModels;

/// <summary>
/// Клас для відображення, створення та редагування проєкту.
/// Містить обчислюване поле Progress і зберігає список пов'язаних ViewModel завдань.
/// </summary>
public class ProjectViewModel
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectType Type { get; set; }

    /// <summary>Завдання проєкту — завантажуються окремо (lazy load)</summary>
    public List<TaskViewModel> Tasks { get; private set; } = new();

    /// <summary>Чи були завантажені завдання для цього проєкту</summary>
    public bool TasksLoaded { get; private set; } = false;

    /// <summary>
    /// Прогрес у відсотках: кількість завершених завдань / загальна кількість.
    /// Обчислюване поле — не зберігається в БД.
    /// </summary>
    public double Progress =>
        Tasks.Count == 0 ? 0 : Math.Round((double)Tasks.Count(t => t.IsCompleted) / Tasks.Count * 100, 1);

    /// <summary>
    /// Конструктор для створення ViewModel на основі моделі зберігання
    /// </summary>
    public ProjectViewModel(ProjectEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Description = entity.Description;
        Type = entity.Type;
    }

    /// <summary>
    /// Конструктор для ручного створення нового проєкту (наприклад, із форми)
    /// </summary>
    public ProjectViewModel(int id, string name, string description, ProjectType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// Завантажує завдання для проєкту. Повторний виклик не дублює завдання.
    /// </summary>
    public void LoadTasks(IEnumerable<TaskViewModel> tasks)
    {
        if (TasksLoaded) return;
        Tasks = tasks.ToList();
        TasksLoaded = true;
    }

    /// <summary>
    /// Повертає короткий рядок для відображення в списку
    /// </summary>
    public string ToListString() =>
        $"[{Id}] {Name} ({Type}) — {(TasksLoaded ? $"{Progress}% виконано" : "завдання не завантажено")}";

    /// <summary>
    /// Повертає детальний рядок для відображення на екрані проєкту
    /// </summary>
    public string ToDetailString() =>
        $"""
        ════════════════════════════════
        Проєкт: {Name}
        Тип:    {Type}
        Опис:   {Description}
        Прогрес: {Progress}% ({Tasks.Count(t => t.IsCompleted)}/{Tasks.Count} завдань виконано)
        ════════════════════════════════
        """;
}
