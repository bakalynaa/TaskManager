using Avalonia.Controls;
using Avalonia.Media;
using TaskManager.ViewModels;

namespace TaskManager.AvaloniaUI;

public partial class TaskDetailView : UserControl
{
    private readonly TaskViewModel _task;

    public TaskDetailView(TaskViewModel task)
    {
        InitializeComponent();
        _task = task;
        BindData();
    }

    private void BindData()
    {
        TitleLabel.Text = _task.Title;
        PriorityLabel.Text = _task.Priority.ToString();
        DueDateLabel.Text = _task.DueDate.ToString("dd.MM.yyyy");
        DescriptionLabel.Text = _task.Description;

        CompletedLabel.Text = _task.IsCompleted ? "Так ✓" : "Ні";
        CompletedLabel.Foreground = _task.IsCompleted
            ? new SolidColorBrush(Color.Parse("#27AE60"))
            : new SolidColorBrush(Color.Parse("#E74C3C"));

        OverdueLabel.Text = _task.IsOverdue ? "Так ⚠" : "Ні";
        OverdueLabel.Foreground = _task.IsOverdue
            ? new SolidColorBrush(Color.Parse("#E74C3C"))
            : new SolidColorBrush(Color.Parse("#27AE60"));

        if (_task.IsCompleted)
        {
            StatusBadge.Background = new SolidColorBrush(Color.Parse("#27AE60"));
            StatusLabel.Text = "✓ Виконано";
        }
        else if (_task.IsOverdue)
        {
            StatusBadge.Background = new SolidColorBrush(Color.Parse("#E74C3C"));
            StatusLabel.Text = "⚠ Прострочено";
        }
        else
        {
            StatusBadge.Background = new SolidColorBrush(Color.Parse("#3498DB"));
            StatusLabel.Text = "○ В роботі";
        }
    }
}
