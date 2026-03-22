using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Інтерфейс репозиторію завдань.
/// </summary>
public interface ITaskRepository
{
    List<TaskEntity> GetByProjectId(int projectId);
    TaskEntity? GetById(int id);
}
