using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

public partial class ProjectsView : UserControl
{
    private readonly ProjectsViewModel _viewModel;

    public ProjectsView(ProjectsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    // Завантаження при появі View — допустимо згідно з вимогами
    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _ = _viewModel.LoadProjectsAsync();
    }

    // Обробка зміни сортування через ComboBox індекс
    private void OnSortChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox cb && DataContext is ProjectsViewModel vm)
            vm.SelectedSort = vm.SortOptions[cb.SelectedIndex];
    }
}
