using TaskManager.Services.DTOs;
using TaskManager.Services.Services;

namespace TaskManager.AvaloniaUI.ViewModels;

/// <summary>
/// ViewModel сторінки деталей завдання.
/// </summary>
public class TaskDetailViewModel : ViewModelBase
{
    private TaskDetailDto? _task;

    public TaskDetailDto? Task
    {
        get => _task;
        private set => SetField(ref _task, value);
    }

    public TaskDetailViewModel(ITaskService taskService, int taskId)
    {
        Task = taskService.GetTaskDetail(taskId);
    }
}
