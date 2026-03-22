using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaskManager.AvaloniaUI.ViewModels;
using TaskManager.AvaloniaUI.Views;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI;

/// <summary>
/// Головне вікно. Відповідає за навігацію через стек сторінок.
/// Отримує сервіси через DI.
/// </summary>
public partial class MainWindow : Window
{
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;
    private readonly Stack<Control> _navigationStack = new();

    public MainWindow(IProjectService projectService, ITaskService taskService)
    {
        InitializeComponent();
        _projectService = projectService;
        _taskService = taskService;

        NavigateTo(new ProjectsView(new ProjectsViewModel(_projectService, NavigateToProject)), "Task Manager");
    }

    private void NavigateToProject(int projectId)
    {
        var vm = new ProjectDetailViewModel(_projectService, projectId, NavigateToTask);
        NavigateTo(new ProjectDetailView(vm), vm.Project?.Name ?? "Проєкт");
    }

    private void NavigateToTask(int taskId)
    {
        var vm = new TaskDetailViewModel(_taskService, taskId);
        NavigateTo(new TaskDetailView(vm), vm.Task?.Title ?? "Завдання");
    }

    private void NavigateTo(Control view, string title)
    {
        if (ContentArea.Content is Control current)
            _navigationStack.Push(current);

        ContentArea.Content = view;
        TitleLabel.Text = title;
        BackButton.IsVisible = _navigationStack.Count > 0;
    }

    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        if (_navigationStack.TryPop(out var previous))
        {
            ContentArea.Content = previous;
            BackButton.IsVisible = _navigationStack.Count > 0;
            TitleLabel.Text = _navigationStack.Count == 0 ? "Task Manager" : "Деталі проєкту";
        }
    }
}
