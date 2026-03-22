using TaskManager.Repositories.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Сервіс завдань. Конвертує DB Model завдання у детальний DTO.
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskDetailDto? GetTaskDetail(int taskId)
    {
        var task = _taskRepository.GetById(taskId);
        if (task is null) return null;

        bool isOverdue = !task.IsCompleted && task.DueDate < DateTime.Today;

        return new TaskDetailDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority.ToString(),
            DueDate = task.DueDate.ToString("dd.MM.yyyy"),
            IsCompleted = task.IsCompleted,
            IsOverdue = isOverdue,
            StatusText = task.IsCompleted ? "✓ Виконано" : (isOverdue ? "⚠ Прострочено" : "○ В роботі"),
            StatusColor = task.IsCompleted ? "#27AE60" : (isOverdue ? "#E74C3C" : "#3498DB")
        };
    }
}
