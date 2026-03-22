using TaskManager.Services.DTOs;

namespace TaskManager.Services.Services;

/// <summary>
/// Інтерфейс сервісу проєктів.
/// UI взаємодіє лише з цим інтерфейсом — не знає про Repository та DB Models.
/// </summary>
public interface IProjectService
{
    List<ProjectListDto> GetAllProjects();
    ProjectDetailDto? GetProjectDetail(int projectId);
}
