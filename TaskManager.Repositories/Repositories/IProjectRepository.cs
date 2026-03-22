using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Інтерфейс репозиторію проєктів.
/// Забезпечує Dependency Inversion — сервіси залежать від абстракції.
/// </summary>
public interface IProjectRepository
{
    List<ProjectEntity> GetAll();
    ProjectEntity? GetById(int id);
}
