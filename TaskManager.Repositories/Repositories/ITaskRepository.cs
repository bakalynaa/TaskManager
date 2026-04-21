using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Інтерфейс репозиторію завдань з асинхронними CRUD операціями.
/// </summary>
public interface ITaskRepository
{
    Task<List<TaskEntity>> GetByProjectIdAsync(int projectId);
    Task<TaskEntity?> GetByIdAsync(int id);
    Task<TaskEntity> AddAsync(TaskEntity entity);
    Task UpdateAsync(TaskEntity entity);
    Task DeleteAsync(int id);
}
