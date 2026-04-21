namespace TaskManager.Services.DTOs;

/// <summary>DTO для відображення завдання в списку</summary>
public class TaskListDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public string DueDateText { get; init; } = string.Empty;
    public string StatusIcon { get; init; } = string.Empty;
    public string PriorityColor { get; init; } = "#95A5A6";
}

/// <summary>DTO для детального відображення завдання</summary>
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

/// <summary>DTO для форми створення/редагування завдання</summary>
public class TaskFormDto
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium";
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public bool IsCompleted { get; set; }
}
