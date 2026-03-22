using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Інтерфейс сервісу завдань.
/// </summary>
public interface ITaskService
{
    TaskDetailDto? GetTaskDetail(int taskId);
}
