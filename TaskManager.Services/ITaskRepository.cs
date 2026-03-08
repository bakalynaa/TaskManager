using TaskManager.ViewModels;

namespace TaskManager.Services;

// Інтерфейс сервісу для роботи зі сховищем проєктів і завдань.
// Використовується для Dependency Inversion.

public interface ITaskRepository
{
    // Повертає всі проєкти без завантажених завдань
    List<ProjectViewModel> GetAllProjects();

    // Завантажує завдання для вказаного проєкту (lazy loading)
    void LoadTasksForProject(ProjectViewModel project);
}
