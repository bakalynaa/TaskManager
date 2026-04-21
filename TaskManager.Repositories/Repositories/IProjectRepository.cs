using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Інтерфейс репозиторію проєктів з асинхронними CRUD операціями.
/// </summary>
public interface IProjectRepository
{
    Task<List<ProjectEntity>> GetAllAsync();
    Task<ProjectEntity?> GetByIdAsync(int id);
    Task<ProjectEntity> AddAsync(ProjectEntity entity);
    Task UpdateAsync(ProjectEntity entity);
    Task DeleteAsync(int id);
}
