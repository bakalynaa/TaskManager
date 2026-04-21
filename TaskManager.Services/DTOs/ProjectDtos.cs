namespace TaskManager.Services.DTOs;

/// <summary>DTO для відображення проєкту в списку</summary>
public class ProjectListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double Progress { get; init; }
    public string ProgressText => $"{Progress}% виконано";
}

/// <summary>DTO для детального відображення проєкту</summary>
public class ProjectDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double Progress { get; init; }
    public int TotalTasks { get; init; }
    public int CompletedTasks { get; init; }
    public string ProgressText => $"{Progress}% ({CompletedTasks}/{TotalTasks} завдань)";
    public List<TaskListDto> Tasks { get; init; } = new();
}

/// <summary>DTO для форми створення/редагування проєкту</summary>
public class ProjectFormDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
