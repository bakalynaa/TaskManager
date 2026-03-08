using TaskManager.Models;

namespace TaskManager.ViewModels;

// Клас для відображення, створення та редагування проєкту.
// Містить обчислюване поле Progress і зберігає список пов'язаних ViewModel завдань.

public class ProjectViewModel
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectType Type { get; set; }

    // Завдання проєкту — завантажуються окремо (lazy load)
    public List<TaskViewModel> Tasks { get; private set; } = new();

    // Чи були завантажені завдання для цього проєкту
    public bool TasksLoaded { get; private set; } = false;
    
    // Прогрес у відсотках: кількість завершених завдань / загальна кількість.
    // Обчислюване поле — не зберігається в БД.
    public double Progress =>
        Tasks.Count == 0 ? 0 : Math.Round((double)Tasks.Count(t => t.IsCompleted) / Tasks.Count * 100, 1);
    
    // Конструктор для створення ViewModel на основі моделі зберігання
    public ProjectViewModel(ProjectEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Description = entity.Description;
        Type = entity.Type;
    }
    
    // Конструктор для ручного створення нового проєкту (наприклад, із форми)
    public ProjectViewModel(int id, string name, string description, ProjectType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }
    
    // Завантажує завдання для проєкту. Повторний виклик не дублює завдання.
    public void LoadTasks(IEnumerable<TaskViewModel> tasks)
    {
        if (TasksLoaded) return;
        Tasks = tasks.ToList();
        TasksLoaded = true;
    }
    
    // Повертає короткий рядок для відображення в списку
    public string ToListString() =>
        $"[{Id}] {Name} ({Type}) — {(TasksLoaded ? $"{Progress}% виконано" : "завдання не завантажено")}";
    
    // Повертає детальний рядок для відображення на екрані проєкту
    public string ToDetailString() =>
        $"""
        ------------------------------
        Проєкт: {Name}
        Тип:    {Type}
        Опис:   {Description}
        Прогрес: {Progress}% ({Tasks.Count(t => t.IsCompleted)}/{Tasks.Count} завдань виконано)
        ------------------------------
        """;
}
