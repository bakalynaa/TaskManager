using Microsoft.EntityFrameworkCore;
using TaskManager.Repositories.Models;
using TaskManager.Repositories.Storage;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Реалізація репозиторію проєктів через EF Core + SQLite.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context) => _context = context;

    public async Task<List<ProjectEntity>> GetAllAsync() =>
        await _context.Projects.ToListAsync();

    public async Task<ProjectEntity?> GetByIdAsync(int id) =>
        await _context.Projects.FindAsync(id);

    public async Task<ProjectEntity> AddAsync(ProjectEntity entity)
    {
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(ProjectEntity entity)
    {
        _context.Projects.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Projects.FindAsync(id);
        if (entity is not null)
        {
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
