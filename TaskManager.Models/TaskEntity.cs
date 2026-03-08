namespace TaskManager.Models;

// Клас для зберігання даних завдання.
// Не містить обчислюваних полів (IsOverdue визначається у ViewModel).
// Зберігає ProjectId для зв'язку з проєктом, але не зберігає посилання на об'єкт проєкту.

public class TaskEntity
{
    // Унікальний ідентифікатор завдання (лише читання після створення)
    public int Id { get; }

    // Ідентифікатор проєкту, якому належить завдання
    public int ProjectId { get; }

    // Назва завдання
    public string Title { get; set; }

    // Опис або технічні характеристики завдання
    public string Description { get; set; }

    // Пріоритет завдання
    public TaskPriority Priority { get; set; }

    // Дата, до якої завдання повинно бути виконаним
    public DateTime DueDate { get; set; }

    // Позначка, чи виконано завдання
    public bool IsCompleted { get; set; }
    
    // Основний конструктор
    public TaskEntity(int id, int projectId, string title, string description,
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
}
