using System;
using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

public partial class ProjectFormView : UserControl
{
    private readonly ProjectFormViewModel _viewModel;

    public ProjectFormView(ProjectFormViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        // Встановлюємо початковий вибір типу
        TypeComboBox.SelectedIndex = Array.IndexOf(_viewModel.TypeOptions, _viewModel.SelectedType);
        _ = _viewModel.LoadAsync();
    }

    private void OnTypeChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox cb && DataContext is ProjectFormViewModel vm && cb.SelectedIndex >= 0)
            vm.SelectedType = vm.TypeOptions[cb.SelectedIndex];
    }
}
