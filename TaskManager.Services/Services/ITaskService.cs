using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Інтерфейс сервісу завдань.
/// </summary>
public interface ITaskService
{
    Task<TaskDetailDto?> GetTaskDetailAsync(int taskId);
    Task<TaskFormDto?> GetTaskFormAsync(int taskId);
    Task<TaskListDto> CreateTaskAsync(TaskFormDto dto);
    Task UpdateTaskAsync(TaskFormDto dto);
    Task DeleteTaskAsync(int taskId);
}
