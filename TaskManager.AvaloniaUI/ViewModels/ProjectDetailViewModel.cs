using System.Linq;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>
/// ViewModel сторінки деталей проєкту зі списком завдань і CRUD.
/// </summary>
public class ProjectDetailViewModel : ViewModelBase
{
    private readonly IProjectService _projectService;
    private readonly Action<int> _navigateToTask;
    private readonly Action<int> _navigateToEditProject;
    private readonly Action<int> _navigateToCreateTask;

    private bool _isBusy;
    private string _searchText = string.Empty;
    private string _selectedSort = "default";
    private ProjectDetailDto? _project;

    public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }

    public string SearchText
    {
        get => _searchText;
        set { SetField(ref _searchText, value); FilterTasks(); }
    }

    public string SelectedSort
    {
        get => _selectedSort;
        set { SetField(ref _selectedSort, value); FilterTasks(); }
    }

    public ProjectDetailDto? Project { get => _project; private set => SetField(ref _project, value); }

    public System.Collections.ObjectModel.ObservableCollection<TaskListDto> FilteredTasks { get; } = new();

    public string[] SortOptions { get; } = { "default", "title_asc", "priority", "due_date" };
    public string[] SortLabels { get; } = { "За замовчуванням", "Назва А-Я", "Пріоритет", "Дедлайн" };

    public ICommand SelectTaskCommand { get; }
    public ICommand EditProjectCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand RefreshCommand { get; }

    private readonly int _projectId;
    private readonly ITaskService _taskService;

    public ProjectDetailViewModel(IProjectService projectService, ITaskService taskService,
        int projectId, Action<int> navigateToTask, Action<int> navigateToEditProject, Action<int> navigateToCreateTask)
    {
        _projectService = projectService;
        _taskService = taskService;
        _projectId = projectId;
        _navigateToTask = navigateToTask;
        _navigateToEditProject = navigateToEditProject;
        _navigateToCreateTask = navigateToCreateTask;

        SelectTaskCommand = new RelayCommand<TaskListDto>(t => { if (t is not null) _navigateToTask(t.Id); });
        EditProjectCommand = new RelayCommand(() => _navigateToEditProject(_projectId));
        AddTaskCommand = new RelayCommand(() => _navigateToCreateTask(_projectId));
        DeleteTaskCommand = new RelayCommand<TaskListDto>(async t => { if (t is not null) await DeleteTaskAsync(t.Id); });
        RefreshCommand = new RelayCommand(async () => await LoadAsync());
    }

    public async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            Project = await _projectService.GetProjectDetailAsync(_projectId);
            FilterTasks();
        }
        finally { IsBusy = false; }
    }

    private void FilterTasks()
    {
        if (Project is null) return;

        var tasks = Project.Tasks.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            tasks = tasks.Where(t => t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        tasks = SelectedSort switch
        {
            "title_asc" => tasks.OrderBy(t => t.Title),
            "priority"  => tasks.OrderBy(t => t.Priority),
            "due_date"  => tasks.OrderBy(t => t.DueDateText),
            _           => tasks
        };

        FilteredTasks.Clear();
        foreach (var t in tasks) FilteredTasks.Add(t);
    }

    private async Task DeleteTaskAsync(int taskId)
    {
        IsBusy = true;
        try
        {
            await _taskService.DeleteTaskAsync(taskId);
            await LoadAsync();
        }
        finally { IsBusy = false; }
    }
}
