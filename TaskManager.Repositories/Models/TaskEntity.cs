namespace TaskManager.Repositories.Models;

/// <summary>
/// DB Model завдання — зберігає лише дані, без обчислюваних полів.
/// Зв'язок з проєктом через ProjectId.
/// </summary>
public class TaskEntity
{
    public int Id { get; }
    public int ProjectId { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

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
