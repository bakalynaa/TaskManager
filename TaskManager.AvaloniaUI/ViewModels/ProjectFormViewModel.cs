using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>ViewModel форми створення/редагування проєкту.</summary>
public class ProjectFormViewModel : ViewModelBase
{
    private readonly IProjectService _projectService;
    private readonly Action _onSaved;
    private bool _isBusy;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _selectedType = "Educational";

    public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }
    public bool IsEditMode { get; }
    public string Title => IsEditMode ? "Редагувати проєкт" : "Новий проєкт";

    public string Name { get => _name; set => SetField(ref _name, value); }
    public string Description { get => _description; set => SetField(ref _description, value); }
    public string SelectedType { get => _selectedType; set => SetField(ref _selectedType, value); }

    public string[] TypeOptions { get; } = { "Educational", "Work", "Personal", "Research", "OpenSource" };
    public string[] TypeLabels { get; } = { "Навчальний", "Робочий", "Особистий", "Дослідницький", "Відкрите джерело" };

    public ICommand SaveCommand { get; }

    private int _projectId;

    public ProjectFormViewModel(IProjectService projectService, Action onSaved, int projectId = 0)
    {
        _projectService = projectService;
        _onSaved = onSaved;
        _projectId = projectId;
        IsEditMode = projectId > 0;
        SaveCommand = new RelayCommand(async () => await SaveAsync());
    }

    public async Task LoadAsync()
    {
        if (!IsEditMode) return;
        IsBusy = true;
        try
        {
            var dto = await _projectService.GetProjectFormAsync(_projectId);
            if (dto is null) return;
            Name = dto.Name;
            Description = dto.Description;
            SelectedType = dto.Type;
        }
        finally { IsBusy = false; }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name)) return;
        IsBusy = true;
        try
        {
            var dto = new ProjectFormDto { Id = _projectId, Name = Name, Description = Description, Type = SelectedType };
            if (IsEditMode) await _projectService.UpdateProjectAsync(dto);
            else await _projectService.CreateProjectAsync(dto);
            _onSaved();
        }
        finally { IsBusy = false; }
    }
}
