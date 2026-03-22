using TaskManager.ViewModels;

namespace TaskManager.UI;

/// <summary>
/// Сторінка деталей завдання — відображає всі поля завдання включно з обчислюваними.
/// </summary>
public partial class TaskDetailPage : ContentPage
{
    private readonly TaskViewModel _task;

    public TaskDetailPage(TaskViewModel task)
    {
        InitializeComponent();
        _task = task;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindTask();
    }

    private void BindTask()
    {
        Title = _task.Title;
        TitleLabel.Text = _task.Title;
        PriorityLabel.Text = _task.Priority.ToString();
        DueDateLabel.Text = _task.DueDate.ToString("dd.MM.yyyy");
        DescriptionLabel.Text = _task.Description;

        // Обчислюване поле IsCompleted
        CompletedLabel.Text = _task.IsCompleted ? "Так ✓" : "Ні";
        CompletedLabel.TextColor = _task.IsCompleted
            ? Color.FromArgb("#27AE60")
            : Color.FromArgb("#E74C3C");

        // Обчислюване поле IsOverdue
        OverdueLabel.Text = _task.IsOverdue ? "Так ⚠" : "Ні";
        OverdueLabel.TextColor = _task.IsOverdue
            ? Color.FromArgb("#E74C3C")
            : Color.FromArgb("#27AE60");

        // Статус бейдж
        if (_task.IsCompleted)
        {
            StatusFrame.BackgroundColor = Color.FromArgb("#27AE60");
            StatusLabel.Text = "✓ Виконано";
        }
        else if (_task.IsOverdue)
        {
            StatusFrame.BackgroundColor = Color.FromArgb("#E74C3C");
            StatusLabel.Text = "⚠ Прострочено";
        }
        else
        {
            StatusFrame.BackgroundColor = Color.FromArgb("#3498DB");
            StatusLabel.Text = "○ В роботі";
        }
    }
}
