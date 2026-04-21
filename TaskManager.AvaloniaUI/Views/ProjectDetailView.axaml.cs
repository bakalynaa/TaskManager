using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

public partial class ProjectDetailView : UserControl
{
    private readonly ProjectDetailViewModel _viewModel;

    public ProjectDetailView(ProjectDetailViewModel viewModel)
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

    private void OnSortChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox cb && DataContext is ProjectDetailViewModel vm)
            vm.SelectedSort = vm.SortOptions[cb.SelectedIndex];
    }
}
