using Avalonia.Controls;
using TaskManager.AvaloniaUI.ViewModels;

namespace TaskManager.AvaloniaUI.Views;

/// <summary>
/// Code-behind містить лише ініціалізацію та задання DataContext — відповідно до MVVM.
/// </summary>
public partial class ProjectDetailView : UserControl
{
    public ProjectDetailView(ProjectDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
