using TaskManager.Models;

namespace TaskManager.ViewModels;

// Клас для відображення, створення та редагування завдання.
// Містить обчислюване поле IsOverdue.
public class TaskViewModel
{
    public int Id { get; }
    public int ProjectId { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    
    // Обчислюване поле: завдання є простроченим, якщо воно не виконане і дедлайн минув.
    public bool IsOverdue => !IsCompleted && DueDate < DateTime.Today;
    
    // Конструктор для створення ViewModel на основі моделі зберігання
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
    
    // Конструктор для ручного створення нового завдання
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
    
    // Короткий рядок для відображення в списку завдань
    public string ToListString()
    {
        string status = IsCompleted ? "✓" : (IsOverdue ? "!" : "○");
        return $"  [{status}] [{Id}] {Title} | Пріоритет: {Priority} | Дедлайн: {DueDate:dd.MM.yyyy}";
    }
    
    // Детальний рядок для відображення повної інформації по завданню
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
