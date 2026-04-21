using Microsoft.EntityFrameworkCore;
using TaskManager.Repositories.Models;
using TaskManager.Repositories.Storage;

namespace TaskManager.Repositories.Repositories;

/// <summary>
/// Реалізація репозиторію завдань через EF Core + SQLite.
/// </summary>
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<List<TaskEntity>> GetByProjectIdAsync(int projectId) =>
        await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();

    public async Task<TaskEntity?> GetByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task<TaskEntity> AddAsync(TaskEntity entity)
    {
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TaskEntity entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Tasks.FindAsync(id);
        if (entity is not null)
        {
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
