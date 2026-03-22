using TaskManager.Repositories.Models;
using TaskManager.Repositories.Storage;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Реалізація репозиторію завдань. Звертається до FakeStorage за даними.
/// </summary>
public class TaskRepository : ITaskRepository
{
    public List<TaskEntity> GetByProjectId(int projectId) =>
        FakeStorage.Tasks.Where(t => t.ProjectId == projectId).ToList();

    public TaskEntity? GetById(int id) =>
        FakeStorage.Tasks.FirstOrDefault(t => t.Id == id);
}
