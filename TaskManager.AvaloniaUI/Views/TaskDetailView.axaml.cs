using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

public partial class TaskDetailView : UserControl
{
    private readonly TaskDetailViewModel _viewModel;

    public TaskDetailView(TaskDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _ = _viewModel.LoadAsync();
    }
}
