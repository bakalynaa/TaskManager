using TaskManager.Models;

namespace TaskManager.ViewModels;

/// <summary>
/// Клас для відображення, створення та редагування завдання.
/// Містить обчислюване поле IsOverdue.
/// </summary>
public class TaskViewModel
{
    public int Id { get; }
    public int ProjectId { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Обчислюване поле: завдання є простроченим, якщо воно не виконане і дедлайн минув.
    /// </summary>
    public bool IsOverdue => !IsCompleted && DueDate < DateTime.Today;

    /// <summary>
    /// Конструктор для створення ViewModel на основі моделі зберігання
    /// </summary>
    public TaskViewModel(TaskEntity entity)
    {
        Id = entity.Id;
        ProjectId = entity.ProjectId;
        Title = entity.Title;
        Description = entity.Description;
        Priority = entity.Priority;
        DueDate = entity.DueDate;
        IsCompleted = entity.IsCompleted;
    }

    /// <summary>
    /// Конструктор для ручного створення нового завдання
    /// </summary>
    public TaskViewModel(int id, int projectId, string title, string description,
        TaskPriority priority, DateTime dueDate, bool isCompleted = false)
    {
        Id = id;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        IsCompleted = isCompleted;
    }

    /// <summary>
    /// Короткий рядок для відображення в списку завдань
    /// </summary>
    public string ToListString()
    {
        string status = IsCompleted ? "✓" : (IsOverdue ? "!" : "○");
        return $"  [{status}] [{Id}] {Title} | Пріоритет: {Priority} | Дедлайн: {DueDate:dd.MM.yyyy}";
    }

    /// <summary>
    /// Детальний рядок для відображення повної інформації по завданню
    /// </summary>
    public string ToDetailString() =>
        $"""
        ────────────────────────────────
        Завдання: {Title}
        Статус:   {(IsCompleted ? "Виконано ✓" : (IsOverdue ? "Прострочено !" : "В роботі ○"))}
        Пріоритет: {Priority}
        Дедлайн:  {DueDate:dd.MM.yyyy}
        Опис:     {Description}
        ────────────────────────────────
        """;
}
