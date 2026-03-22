namespace TaskManager.Services.DTOs;

/// <summary>
/// DTO для відображення завдання в списку — лише необхідні поля.
/// </summary>
public class TaskListDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public string DueDateText { get; init; } = string.Empty;
    public string StatusIcon { get; init; } = string.Empty;
    public string PriorityColor { get; init; } = "#95A5A6";
}

/// <summary>
/// DTO для детального відображення завдання — всі поля включно з обчислюваними.
/// </summary>
public class TaskDetailDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public string DueDate { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
    public bool IsOverdue { get; init; }
    public string StatusText { get; init; } = string.Empty;
    public string StatusColor { get; init; } = "#3498DB";
}
