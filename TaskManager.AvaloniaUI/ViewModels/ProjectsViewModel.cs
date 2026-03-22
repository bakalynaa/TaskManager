using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>
/// ViewModel головної сторінки — список проєктів.
/// Отримує дані через IProjectService (DI). Не знає про Repository та DB Models.
/// </summary>
public class ProjectsViewModel : ViewModelBase
{
    private readonly IProjectService _projectService;
    private readonly Action<int> _navigateToProject;

    public ObservableCollection<ProjectListDto> Projects { get; } = new();

    public ICommand SelectProjectCommand { get; }

    public ProjectsViewModel(IProjectService projectService, Action<int> navigateToProject)
    {
        _projectService = projectService;
        _navigateToProject = navigateToProject;

        SelectProjectCommand = new RelayCommand<ProjectListDto>(p =>
        {
            if (p is not null) _navigateToProject(p.Id);
        });

        LoadProjects();
    }

    private void LoadProjects()
    {
        Projects.Clear();
        foreach (var p in _projectService.GetAllProjects())
            Projects.Add(p);
    }
}
