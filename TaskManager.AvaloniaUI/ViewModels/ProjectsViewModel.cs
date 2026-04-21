using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>
/// ViewModel головної сторінки — список проєктів з пошуком, сортуванням і CRUD.
/// </summary>
public class ProjectsViewModel : ViewModelBase
{
    private readonly IProjectService _projectService;
    private readonly Action<int> _navigateToProject;
    private readonly Action _navigateToCreateProject;

    private bool _isBusy;
    private string _searchText = string.Empty;
    private string _selectedSort = "default";

    public bool IsBusy
    {
        get => _isBusy;
        set => SetField(ref _isBusy, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            SetField(ref _searchText, value);
            _ = LoadProjectsAsync();
        }
    }

    public string SelectedSort
    {
        get => _selectedSort;
        set
        {
            SetField(ref _selectedSort, value);
            _ = LoadProjectsAsync();
        }
    }

    public ObservableCollection<ProjectListDto> Projects { get; } = new();

    public string[] SortOptions { get; } = { "default", "name_asc", "name_desc", "type" };
    public string[] SortLabels { get; } = { "За замовчуванням", "Назва А-Я", "Назва Я-А", "Тип" };

    public ICommand SelectProjectCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand DeleteProjectCommand { get; }
    public ICommand RefreshCommand { get; }

    public ProjectsViewModel(IProjectService projectService, Action<int> navigateToProject, Action navigateToCreateProject)
    {
        _projectService = projectService;
        _navigateToProject = navigateToProject;
        _navigateToCreateProject = navigateToCreateProject;

        SelectProjectCommand = new RelayCommand<ProjectListDto>(p => { if (p is not null) _navigateToProject(p.Id); });
        AddProjectCommand = new RelayCommand(_navigateToCreateProject);
        DeleteProjectCommand = new RelayCommand<ProjectListDto>(async p => { if (p is not null) await DeleteProjectAsync(p.Id); });
        RefreshCommand = new RelayCommand(async () => await LoadProjectsAsync());
    }

    public async Task LoadProjectsAsync()
    {
        IsBusy = true;
        try
        {
            var projects = await _projectService.GetAllProjectsAsync(SearchText, SelectedSort);
            Projects.Clear();
            foreach (var p in projects) Projects.Add(p);
        }
        finally { IsBusy = false; }
    }

    private async Task DeleteProjectAsync(int id)
    {
        IsBusy = true;
        try
        {
            await _projectService.DeleteProjectAsync(id);
            await LoadProjectsAsync();
        }
        finally { IsBusy = false; }
    }
}
