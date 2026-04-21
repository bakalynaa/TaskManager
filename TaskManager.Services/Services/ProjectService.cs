using TaskManager.Repositories.Models;
using TaskManager.Repositories.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Сервіс проєктів — отримує дані з репозиторію, конвертує у DTO, підтримує CRUD.
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

    public async Task<List<ProjectListDto>> GetAllProjectsAsync(string? search = null, string? sortBy = null)
    {
        var projects = await _projectRepository.GetAllAsync();

        // Фільтрація за пошуком
        if (!string.IsNullOrWhiteSpace(search))
            projects = projects.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        // Сортування
        projects = sortBy switch
        {
            "name_asc"  => projects.OrderBy(p => p.Name).ToList(),
            "name_desc" => projects.OrderByDescending(p => p.Name).ToList(),
            "type"      => projects.OrderBy(p => p.Type).ToList(),
            _           => projects
        };

        var result = new List<ProjectListDto>();
        foreach (var p in projects)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(p.Id);
            int completed = tasks.Count(t => t.IsCompleted);
            double progress = tasks.Count == 0 ? 0 : Math.Round((double)completed / tasks.Count * 100, 1);
            result.Add(new ProjectListDto
            {
                Id = p.Id, Name = p.Name, Type = p.Type.ToString(),
                Description = p.Description, Progress = progress
            });
        }
        return result;
    }

    public async Task<ProjectDetailDto?> GetProjectDetailAsync(int projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project is null) return null;

        var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
        int completed = tasks.Count(t => t.IsCompleted);
        double progress = tasks.Count == 0 ? 0 : Math.Round((double)completed / tasks.Count * 100, 1);

        return new ProjectDetailDto
        {
            Id = project.Id, Name = project.Name, Type = project.Type.ToString(),
            Description = project.Description, Progress = progress,
            TotalTasks = tasks.Count, CompletedTasks = completed,
            Tasks = tasks.Select(MapTaskToListDto).ToList()
        };
    }

    public async Task<ProjectFormDto?> GetProjectFormAsync(int projectId)
    {
        var p = await _projectRepository.GetByIdAsync(projectId);
        if (p is null) return null;
        return new ProjectFormDto { Id = p.Id, Name = p.Name, Description = p.Description, Type = p.Type.ToString() };
    }

    public async Task<ProjectListDto> CreateProjectAsync(ProjectFormDto dto)
    {
        var entity = new ProjectEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            Type = Enum.Parse<ProjectType>(dto.Type)
        };
        await _projectRepository.AddAsync(entity);
        return new ProjectListDto { Id = entity.Id, Name = entity.Name, Type = entity.Type.ToString(), Description = entity.Description, Progress = 0 };
    }

    public async Task UpdateProjectAsync(ProjectFormDto dto)
    {
        var entity = await _projectRepository.GetByIdAsync(dto.Id);
        if (entity is null) return;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Type = Enum.Parse<ProjectType>(dto.Type);
        await _projectRepository.UpdateAsync(entity);
    }

    public async Task DeleteProjectAsync(int projectId) =>
        await _projectRepository.DeleteAsync(projectId);

    private static TaskListDto MapTaskToListDto(TaskEntity t)
    {
        bool isOverdue = !t.IsCompleted && t.DueDate < DateTime.Today;
        return new TaskListDto
        {
            Id = t.Id, Title = t.Title, Priority = t.Priority.ToString(),
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
