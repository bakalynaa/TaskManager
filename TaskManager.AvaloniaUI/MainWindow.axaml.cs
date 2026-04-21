using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaskManager.AvaloniaUI.ViewModels;
using TaskManager.AvaloniaUI.Views;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI;

/// <summary>
/// Головне вікно. Керує навігацією через стек сторінок.
/// </summary>
public partial class MainWindow : Window
{
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;
    private readonly Stack<(Control View, string Title)> _navigationStack = new();

    public MainWindow(IProjectService projectService, ITaskService taskService)
    {
        InitializeComponent();
        _projectService = projectService;
        _taskService = taskService;
        NavigateToProjects();
    }

    private void NavigateToProjects()
    {
        _navigationStack.Clear();
        var vm = new ProjectsViewModel(_projectService, NavigateToProjectDetail, NavigateToCreateProject);
        NavigateTo(new ProjectsView(vm), "Task Manager");
        BackButton.IsVisible = false;
    }

    private void NavigateToProjectDetail(int projectId)
    {
        var vm = new ProjectDetailViewModel(_projectService, _taskService, projectId,
            NavigateToTaskDetail, NavigateToEditProject, NavigateToCreateTask);
        NavigateTo(new ProjectDetailView(vm), "Деталі проєкту");
    }

    private void NavigateToTaskDetail(int taskId)
    {
        var vm = new TaskDetailViewModel(_taskService, taskId, NavigateToEditTask);
        NavigateTo(new TaskDetailView(vm), "Деталі завдання");
    }

    private void NavigateToCreateProject()
    {
        var vm = new ProjectFormViewModel(_projectService, () => { NavigateBack(); _ = RefreshProjectsIfNeeded(); });
        NavigateTo(new ProjectFormView(vm), "Новий проєкт");
    }

    private void NavigateToEditProject(int projectId)
    {
        var vm = new ProjectFormViewModel(_projectService, NavigateBack, projectId);
        NavigateTo(new ProjectFormView(vm), "Редагувати проєкт");
    }

    private void NavigateToCreateTask(int projectId)
    {
        var vm = new TaskFormViewModel(_taskService, NavigateBack, projectId);
        NavigateTo(new TaskFormView(vm), "Нове завдання");
    }

    private void NavigateToEditTask(int taskId)
    {
        // Отримуємо projectId з поточного контексту
        var vm = new TaskFormViewModel(_taskService, NavigateBack, 0, taskId);
        NavigateTo(new TaskFormView(vm), "Редагувати завдання");
    }

    private void NavigateTo(Control view, string title)
    {
        if (ContentArea.Content is Control current)
            _navigationStack.Push((current, TitleLabel.Text ?? string.Empty));

        ContentArea.Content = view;
        TitleLabel.Text = title;
        BackButton.IsVisible = _navigationStack.Count > 0;
    }

    private void NavigateBack()
    {
        if (_navigationStack.TryPop(out var previous))
        {
            ContentArea.Content = previous.View;
            TitleLabel.Text = previous.Title;
            BackButton.IsVisible = _navigationStack.Count > 0;

            // Оновлюємо дані при поверненні
            if (previous.View is ProjectsView pv && pv.DataContext is ProjectsViewModel pvm)
                _ = pvm.LoadProjectsAsync();
            else if (previous.View is ProjectDetailView pdv && pdv.DataContext is ProjectDetailViewModel pdvm)
                _ = pdvm.LoadAsync();
        }
    }

    private System.Threading.Tasks.Task RefreshProjectsIfNeeded() =>
        System.Threading.Tasks.Task.CompletedTask;

    private void OnBackClicked(object? sender, RoutedEventArgs e) => NavigateBack();
}
