using TaskManager.Services;
using TaskManager.ViewModels;

namespace TaskManager.UI;

/// <summary>
/// Головна сторінка — список всіх проєктів.
/// Отримує дані через ITaskRepository (Dependency Injection).
/// </summary>
public partial class ProjectsPage : ContentPage
{
    private readonly ITaskRepository _repository;
    private List<ProjectDisplayModel> _projects = new();

    public ProjectsPage(ITaskRepository repository)
    {
        InitializeComponent();
        _repository = repository;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProjects();
    }

    private void LoadProjects()
    {
        var viewModels = _repository.GetAllProjects();
        _projects = viewModels.Select(p => new ProjectDisplayModel(p)).ToList();
        ProjectsCollection.ItemsSource = _projects;
    }

    private async void OnProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ProjectDisplayModel selected) return;

        // Скидаємо вибір щоб можна було вибрати знову
        ProjectsCollection.SelectedItem = null;

        // Завантажуємо завдання для вибраного проєкту
        _repository.LoadTasksForProject(selected.ViewModel);

        await Navigation.PushAsync(new ProjectDetailPage(selected.ViewModel));
    }
}

/// <summary>
/// Допоміжна модель для відображення проєкту в списку з додатковими властивостями для binding
/// </summary>
public class ProjectDisplayModel
{
    public ProjectViewModel ViewModel { get; }
    public string Name => ViewModel.Name;
    public string Description => ViewModel.Description;
    public string Type => ViewModel.Type.ToString();
    public double ProgressFraction => ViewModel.Progress / 100.0;
    public string ProgressText => $"{ViewModel.Progress}%";

    public ProjectDisplayModel(ProjectViewModel vm) => ViewModel = vm;
}
