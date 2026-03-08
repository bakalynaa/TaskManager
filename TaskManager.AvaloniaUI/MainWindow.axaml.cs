using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaskManager.Services;

namespace TaskManager.AvaloniaUI;

/// <summary>
/// Головне вікно застосунку. Відповідає за навігацію між сторінками.
/// </summary>
public partial class MainWindow : Window
{
    private readonly ITaskRepository _repository;
    private readonly Stack<Control> _navigationStack = new();

    public MainWindow(ITaskRepository repository)
    {
        InitializeComponent();
        _repository = repository;
        NavigateTo(new ProjectsView(_repository, this), "Task Manager");
    }

    public void NavigateTo(Control view, string title)
    {
        if (ContentArea.Content is Control current)
            _navigationStack.Push(current);

        ContentArea.Content = view;
        TitleLabel.Text = title;
        BackButton.IsVisible = _navigationStack.Count > 0;
    }

    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        if (_navigationStack.TryPop(out var previous))
        {
            ContentArea.Content = previous;
            BackButton.IsVisible = _navigationStack.Count > 0;
            TitleLabel.Text = _navigationStack.Count == 0 ? "Task Manager" : "Деталі проєкту";
        }
    }
}
