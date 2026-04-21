using System;
using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

public partial class TaskFormView : UserControl
{
    private readonly TaskFormViewModel _viewModel;

    public TaskFormView(TaskFormViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        PriorityComboBox.SelectedIndex = Array.IndexOf(_viewModel.PriorityOptions, _viewModel.SelectedPriority);
        _ = _viewModel.LoadAsync();
    }

    private void OnPriorityChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox cb && DataContext is TaskFormViewModel vm && cb.SelectedIndex >= 0)
            vm.SelectedPriority = vm.PriorityOptions[cb.SelectedIndex];
    }
}
