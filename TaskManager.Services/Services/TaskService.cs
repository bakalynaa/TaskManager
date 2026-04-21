using TaskManager.Repositories.Models;
using TaskManager.Repositories.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Сервіс завдань — CRUD операції з конвертацією у DTO.
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository) => _taskRepository = taskRepository;

    public async Task<TaskDetailDto?> GetTaskDetailAsync(int taskId)
    {
        var t = await _taskRepository.GetByIdAsync(taskId);
        if (t is null) return null;
        bool isOverdue = !t.IsCompleted && t.DueDate < DateTime.Today;
        return new TaskDetailDto
        {
            Id = t.Id, Title = t.Title, Description = t.Description,
            Priority = t.Priority.ToString(), DueDate = t.DueDate.ToString("dd.MM.yyyy"),
            IsCompleted = t.IsCompleted, IsOverdue = isOverdue,
            StatusText = t.IsCompleted ? "✓ Виконано" : (isOverdue ? "⚠ Прострочено" : "○ В роботі"),
            StatusColor = t.IsCompleted ? "#27AE60" : (isOverdue ? "#E74C3C" : "#3498DB")
        };
    }

    public async Task<TaskFormDto?> GetTaskFormAsync(int taskId)
    {
        var t = await _taskRepository.GetByIdAsync(taskId);
        if (t is null) return null;
        return new TaskFormDto
        {
            Id = t.Id, ProjectId = t.ProjectId, Title = t.Title,
            Description = t.Description, Priority = t.Priority.ToString(),
            DueDate = t.DueDate, IsCompleted = t.IsCompleted
        };
    }

    public async Task<TaskListDto> CreateTaskAsync(TaskFormDto dto)
    {
        var entity = new TaskEntity
        {
            ProjectId = dto.ProjectId, Title = dto.Title,
            Description = dto.Description, Priority = Enum.Parse<TaskPriority>(dto.Priority),
            DueDate = dto.DueDate, IsCompleted = dto.IsCompleted
        };
        await _taskRepository.AddAsync(entity);
        bool isOverdue = !entity.IsCompleted && entity.DueDate < DateTime.Today;
        return new TaskListDto
        {
            Id = entity.Id, Title = entity.Title, Priority = entity.Priority.ToString(),
            DueDateText = $"Дедлайн: {entity.DueDate:dd.MM.yyyy}",
            StatusIcon = entity.IsCompleted ? "✓" : (isOverdue ? "⚠" : "○"),
            PriorityColor = entity.Priority switch
            {
                TaskPriority.Critical => "#E74C3C",
                TaskPriority.High     => "#E67E22",
                TaskPriority.Medium   => "#F39C12",
                TaskPriority.Low      => "#27AE60",
                _                     => "#95A5A6"
            }
        };
    }

    public async Task UpdateTaskAsync(TaskFormDto dto)
    {
        var entity = await _taskRepository.GetByIdAsync(dto.Id);
        if (entity is null) return;
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Priority = Enum.Parse<TaskPriority>(dto.Priority);
        entity.DueDate = dto.DueDate;
        entity.IsCompleted = dto.IsCompleted;
        await _taskRepository.UpdateAsync(entity);
    }

    public async Task DeleteTaskAsync(int taskId) =>
        await _taskRepository.DeleteAsync(taskId);
}
