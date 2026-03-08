using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Services;

// Сервіс для роботи зі сховищем.
// Єдиний клас, який має доступ до FakeStorage.
// Відповідає за отримання та перетворення Entity у ViewModel.

public class TaskRepository : ITaskRepository
{
    // Повертає всі проєкти у вигляді ViewModel (без завдань).
    // Завдання завантажуються окремо через LoadTasksForProject (lazy loading).
    public List<ProjectViewModel> GetAllProjects()
    {
        return FakeStorage.Projects
            .Select(p => new ProjectViewModel(p))
            .ToList();
    }
    
    // Завантажує та прив'язує завдання до вказаного ProjectViewModel.
    // Якщо завдання вже завантажені — повторного завантаження не відбувається.
    public void LoadTasksForProject(ProjectViewModel project)
    {
        if (project.TasksLoaded) return;

        var tasks = FakeStorage.Tasks
            .Where(t => t.ProjectId == project.Id)
            .Select(t => new TaskViewModel(t))
            .ToList();

        project.LoadTasks(tasks);
    }
    
    // Повертає один проєкт за його ідентифікатором або null, якщо не знайдено.
    public ProjectViewModel? GetProjectById(int id)
    {
        var entity = FakeStorage.Projects.FirstOrDefault(p => p.Id == id);
        return entity is null ? null : new ProjectViewModel(entity);
    }
}
