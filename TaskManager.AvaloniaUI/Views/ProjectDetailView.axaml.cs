using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.AvaloniaUI;

public partial class ProjectDetailView : UserControl
{
    private readonly ProjectViewModel _project;
    private readonly MainWindow _mainWindow;

    public ProjectDetailView(ProjectViewModel project, MainWindow mainWindow)
    {
        InitializeComponent();
        _project = project;
        _mainWindow = mainWindow;
        BindData();
    }

    private void BindData()
    {
        ProjectTypeLabel.Text = $"Тип: {_project.Type}";
        ProjectDescLabel.Text = _project.Description;
        ProjectProgress.Value = _project.Progress;
        ProjectProgressLabel.Text = $"{_project.Progress}% виконано ({_project.Tasks.Count(t => t.IsCompleted)}/{_project.Tasks.Count} завдань)";
        TasksList.ItemsSource = _project.Tasks.Select(t => new TaskListItem(t, _mainWindow)).ToList();
    }
}

public class TaskListItem
{
    private readonly TaskViewModel _viewModel;
    private readonly MainWindow _mainWindow;

    public string Title => _viewModel.Title;
    public string Priority => _viewModel.Priority.ToString();
    public string DueDateText => $"Дедлайн: {_viewModel.DueDate:dd.MM.yyyy}";
    public string StatusIcon => _viewModel.IsCompleted ? "✓" : (_viewModel.IsOverdue ? "⚠" : "○");

    public IBrush PriorityColor => _viewModel.Priority switch
    {
        TaskPriority.Critical => new SolidColorBrush(Color.Parse("#E74C3C")),
        TaskPriority.High     => new SolidColorBrush(Color.Parse("#E67E22")),
        TaskPriority.Medium   => new SolidColorBrush(Color.Parse("#F39C12")),
        TaskPriority.Low      => new SolidColorBrush(Color.Parse("#27AE60")),
        _                     => new SolidColorBrush(Color.Parse("#95A5A6"))
    };

    public ICommand SelectCommand { get; }

    public TaskListItem(TaskViewModel vm, MainWindow mainWindow)
    {
        _viewModel = vm;
        _mainWindow = mainWindow;
        SelectCommand = new RelayCommand(() => _mainWindow.NavigateTo(new TaskDetailView(vm), vm.Title));
    }
}
