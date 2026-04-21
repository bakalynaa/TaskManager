using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>ViewModel форми створення/редагування завдання.</summary>
public class TaskFormViewModel : ViewModelBase
{
    private readonly ITaskService _taskService;
    private readonly Action _onSaved;
    private bool _isBusy;
    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _selectedPriority = "Medium";
    private DateTime _dueDate = DateTime.Today.AddDays(7);
    private bool _isCompleted;

    public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }
    public bool IsEditMode { get; }
    public string Title => IsEditMode ? "Редагувати завдання" : "Нове завдання";

    public string TaskTitle { get => _title; set => SetField(ref _title, value); }
    public string Description { get => _description; set => SetField(ref _description, value); }
    public string SelectedPriority { get => _selectedPriority; set => SetField(ref _selectedPriority, value); }
    public DateTime DueDate { get => _dueDate; set => SetField(ref _dueDate, value); }
    public bool IsCompleted { get => _isCompleted; set => SetField(ref _isCompleted, value); }

    public string[] PriorityOptions { get; } = { "Critical", "High", "Medium", "Low", "Optional" };
    public string[] PriorityLabels { get; } = { "Критичний", "Високий", "Середній", "Низький", "Необов'язковий" };

    public ICommand SaveCommand { get; }

    private int _taskId;
    private int _projectId;

    public TaskFormViewModel(ITaskService taskService, Action onSaved, int projectId, int taskId = 0)
    {
        _taskService = taskService;
        _onSaved = onSaved;
        _projectId = projectId;
        _taskId = taskId;
        IsEditMode = taskId > 0;
        SaveCommand = new RelayCommand(async () => await SaveAsync());
    }

    public async Task LoadAsync()
    {
        if (!IsEditMode) return;
        IsBusy = true;
        try
        {
            var dto = await _taskService.GetTaskFormAsync(_taskId);
            if (dto is null) return;
            TaskTitle = dto.Title;
            Description = dto.Description;
            SelectedPriority = dto.Priority;
            DueDate = dto.DueDate;
            IsCompleted = dto.IsCompleted;
        }
        finally { IsBusy = false; }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(TaskTitle)) return;
        IsBusy = true;
        try
        {
            var dto = new TaskFormDto
            {
                Id = _taskId, ProjectId = _projectId, Title = TaskTitle,
                Description = Description, Priority = SelectedPriority,
                DueDate = DueDate, IsCompleted = IsCompleted
            };
            if (IsEditMode) await _taskService.UpdateTaskAsync(dto);
            else await _taskService.CreateTaskAsync(dto);
            _onSaved();
        }
        finally { IsBusy = false; }
    }
}
