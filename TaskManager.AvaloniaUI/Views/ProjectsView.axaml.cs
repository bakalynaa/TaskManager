using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using TaskManager.Services;
using TaskManager.ViewModels;

namespace TaskManager.AvaloniaUI;

public partial class ProjectsView : UserControl
{
    private readonly ITaskRepository _repository;
    private readonly MainWindow _mainWindow;

    public ProjectsView(ITaskRepository repository, MainWindow mainWindow)
    {
        InitializeComponent();
        _repository = repository;
        _mainWindow = mainWindow;
        LoadProjects();
    }

    private void LoadProjects()
    {
        var projects = _repository.GetAllProjects();
        var items = projects.Select(p => new ProjectListItem(p, _repository, _mainWindow)).ToList();
        ProjectsList.ItemsSource = items;
    }
}

public class ProjectListItem
{
    private readonly ProjectViewModel _viewModel;
    private readonly ITaskRepository _repository;
    private readonly MainWindow _mainWindow;

    public string Name => _viewModel.Name;
    public string Description => _viewModel.Description;
    public string Type => _viewModel.Type.ToString();
    public double Progress => _viewModel.Progress;
    public string ProgressText => $"{_viewModel.Progress}% виконано";
    public ICommand SelectCommand { get; }

    public ProjectListItem(ProjectViewModel vm, ITaskRepository repository, MainWindow mainWindow)
    {
        _viewModel = vm;
        _repository = repository;
        _mainWindow = mainWindow;
        SelectCommand = new RelayCommand(() =>
        {
            _repository.LoadTasksForProject(_viewModel);
            _mainWindow.NavigateTo(new ProjectDetailView(_viewModel, _mainWindow), _viewModel.Name);
        });
    }
}
