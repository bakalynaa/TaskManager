using TaskManager.Repositories.Models;
using TaskManager.Repositories.Storage;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Реалізація репозиторію проєктів. Звертається до FakeStorage за даними.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    public List<ProjectEntity> GetAll() => FakeStorage.Projects;

    public ProjectEntity? GetById(int id) =>
        FakeStorage.Projects.FirstOrDefault(p => p.Id == id);
}
