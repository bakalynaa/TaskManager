using System;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>
/// ViewModel сторінки деталей проєкту.
/// </summary>
public class ProjectDetailViewModel : ViewModelBase
{
    private ProjectDetailDto? _project;

    public ProjectDetailDto? Project
    {
        get => _project;
        private set => SetField(ref _project, value);
    }

    public ICommand SelectTaskCommand { get; }

    public ProjectDetailViewModel(IProjectService projectService, int projectId, Action<int> navigateToTask)
    {
        SelectTaskCommand = new RelayCommand<TaskListDto>(t =>
        {
            if (t is not null) navigateToTask(t.Id);
        });

        Project = projectService.GetProjectDetail(projectId);
    }
}
