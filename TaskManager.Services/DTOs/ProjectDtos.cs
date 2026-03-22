namespace TaskManager.Services.DTOs;

/// <summary>
/// DTO для відображення проєкту в списку — лише необхідні поля.
/// </summary>
public class ProjectListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double Progress { get; init; }
    public string ProgressText => $"{Progress}% виконано";
}

/// <summary>
/// DTO для детального відображення проєкту — всі поля включно з обчислюваними.
/// </summary>
public class ProjectDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double Progress { get; init; }
    public int TotalTasks { get; init; }
    public int CompletedTasks { get; init; }
    public string ProgressText => $"{Progress}% виконано ({CompletedTasks}/{TotalTasks} завдань)";
    public List<TaskListDto> Tasks { get; init; } = new();
}
