using TaskManager.Repositories.Models;
using TaskManager.Repositories.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Сервіс проєктів. Отримує DB Models з репозиторію, конвертує їх у DTO.
/// Репозиторій отримується через DI — сервіс не створює його сам.
/// </summary>
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    /// <summary>Повертає список проєктів для відображення в списку</summary>
    public List<ProjectListDto> GetAllProjects()
    {
        return _projectRepository.GetAll()
            .Select(p => MapToListDto(p))
            .ToList();
    }

    /// <summary>Повертає детальну інформацію про проєкт разом із завданнями</summary>
    public ProjectDetailDto? GetProjectDetail(int projectId)
    {
        var project = _projectRepository.GetById(projectId);
        if (project is null) return null;

        var tasks = _taskRepository.GetByProjectId(projectId);
        var taskDtos = tasks.Select(MapTaskToListDto).ToList();
        int completed = tasks.Count(t => t.IsCompleted);
        double progress = tasks.Count == 0 ? 0 : Math.Round((double)completed / tasks.Count * 100, 1);

        return new ProjectDetailDto
        {
            Id = project.Id,
            Name = project.Name,
            Type = project.Type.ToString(),
            Description = project.Description,
            Progress = progress,
            TotalTasks = tasks.Count,
            CompletedTasks = completed,
            Tasks = taskDtos
        };
    }

    private ProjectListDto MapToListDto(ProjectEntity p)
    {
        var tasks = _taskRepository.GetByProjectId(p.Id);
        int completed = tasks.Count(t => t.IsCompleted);
        double progress = tasks.Count == 0 ? 0 : Math.Round((double)completed / tasks.Count * 100, 1);

        return new ProjectListDto
        {
            Id = p.Id,
            Name = p.Name,
            Type = p.Type.ToString(),
            Description = p.Description,
            Progress = progress
        };
    }

    private static TaskListDto MapTaskToListDto(TaskEntity t)
    {
        bool isOverdue = !t.IsCompleted && t.DueDate < DateTime.Today;
        return new TaskListDto
        {
            Id = t.Id,
            Title = t.Title,
            Priority = t.Priority.ToString(),
            DueDateText = $"Дедлайн: {t.DueDate:dd.MM.yyyy}",
            StatusIcon = t.IsCompleted ? "✓" : (isOverdue ? "⚠" : "○"),
            PriorityColor = t.Priority switch
            {
                TaskPriority.Critical => "#E74C3C",
                TaskPriority.High     => "#E67E22",
                TaskPriority.Medium   => "#F39C12",
                TaskPriority.Low      => "#27AE60",
                _                     => "#95A5A6"
            }
        };
    }
}
