using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>ViewModel сторінки деталей завдання.</summary>
public class TaskDetailViewModel : ViewModelBase
{
    private bool _isBusy;
    private TaskDetailDto? _task;

    public bool IsBusy { get => _isBusy; set => SetField(ref _isBusy, value); }
    public TaskDetailDto? Task { get => _task; private set => SetField(ref _task, value); }

    public ICommand EditCommand { get; }

    private readonly ITaskService _taskService;
    private readonly int _taskId;

    public TaskDetailViewModel(ITaskService taskService, int taskId, Action<int> navigateToEdit)
    {
        _taskService = taskService;
        _taskId = taskId;
        EditCommand = new RelayCommand(() => navigateToEdit(_taskId));
    }

    public async Task LoadAsync()
    {
        IsBusy = true;
        try { Task = await _taskService.GetTaskDetailAsync(_taskId); }
        finally { IsBusy = false; }
    }
}
