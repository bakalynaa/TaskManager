namespace TaskManager.UI;

/// <summary>
/// Головний клас застосунку. Створює єдине вікно з NavigationPage.
/// Навігація відбувається через зміну сторінок, а не через нові вікна.
/// </summary>
public partial class App : Application
{
    public App(ProjectsPage projectsPage)
    {
        InitializeComponent();

        // Єдине вікно з навігацією через NavigationPage
        MainPage = new NavigationPage(projectsPage)
        {
            BarBackgroundColor = Color.FromArgb("#2C3E50"),
            BarTextColor = Colors.White
        };
    }
}
