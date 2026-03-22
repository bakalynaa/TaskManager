using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.UI;

/// <summary>
/// Сторінка деталей проєкту — показує інформацію про проєкт і список його завдань.
/// </summary>
public partial class ProjectDetailPage : ContentPage
{
    private readonly ProjectViewModel _project;

    public ProjectDetailPage(ProjectViewModel project)
    {
        InitializeComponent();
        _project = project;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindProjectInfo();
        BindTasks();
    }

    private void BindProjectInfo()
    {
        ProjectNameLabel.Text = _project.Name;
        ProjectTypeLabel.Text = _project.Type.ToString();
        ProjectDescLabel.Text = _project.Description;
        ProjectProgress.Progress = _project.Progress / 100.0;
        ProjectProgressLabel.Text = $"{_project.Progress}% виконано ({_project.Tasks.Count(t => t.IsCompleted)}/{_project.Tasks.Count} завдань)";
    }

    private void BindTasks()
    {
        var displayTasks = _project.Tasks
            .Select(t => new TaskDisplayModel(t))
            .ToList();

        TasksCollection.ItemsSource = displayTasks;
    }

    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TaskDisplayModel selected) return;
        TasksCollection.SelectedItem = null;
        await Navigation.PushAsync(new TaskDetailPage(selected.ViewModel));
    }
}

/// <summary>
/// Допоміжна модель для відображення завдання в списку
/// </summary>
public class TaskDisplayModel
{
    public TaskViewModel ViewModel { get; }
    public string Title => ViewModel.Title;
    public string Priority => ViewModel.Priority.ToString();
    public string DueDateText => $"Дедлайн: {ViewModel.DueDate:dd.MM.yyyy}";

    public string StatusIcon => ViewModel.IsCompleted ? "✓" : (ViewModel.IsOverdue ? "⚠" : "○");

    public string PriorityColor => ViewModel.Priority switch
    {
        TaskPriority.Critical => "#E74C3C",
        TaskPriority.High     => "#E67E22",
        TaskPriority.Medium   => "#F1C40F",
        TaskPriority.Low      => "#27AE60",
        _                     => "#95A5A6"
    };

    public TaskDisplayModel(TaskViewModel vm) => ViewModel = vm;
}
