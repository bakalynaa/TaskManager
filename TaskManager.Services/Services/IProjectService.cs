using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Інтерфейс сервісу проєктів. UI взаємодіє лише через цей інтерфейс.
/// </summary>
public interface IProjectService
{
    Task<List<ProjectListDto>> GetAllProjectsAsync(string? search = null, string? sortBy = null);
    Task<ProjectDetailDto?> GetProjectDetailAsync(int projectId);
    Task<ProjectFormDto?> GetProjectFormAsync(int projectId);
    Task<ProjectListDto> CreateProjectAsync(ProjectFormDto dto);
    Task UpdateProjectAsync(ProjectFormDto dto);
    Task DeleteProjectAsync(int projectId);
}
