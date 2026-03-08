using TaskManager.Services;
using TaskManager.ViewModels;

// Task Manager by Anastasiia Bakalyna

var repository = new TaskRepository();
List<ProjectViewModel> projects = new();

bool running = true;

while (running)
{
    ShowMainMenu();
    string? input = Console.ReadLine()?.Trim();

    switch (input)
    {
        case "1":
            LoadAndShowProjects();
            break;
        case "2":
            SelectProject();
            break;
        case "0":
            running = false;
            Console.WriteLine("\nДо побачення!");
            break;
        default:
            Console.WriteLine("Невідома команда. Спробуйте ще раз.");
            break;
    }
}

// Відображення головного меню

void ShowMainMenu()
{
    Console.WriteLine();
    Console.WriteLine("----- TASK MANAGER ------");
    Console.WriteLine("1 — Завантажити / оновити список проєктів");
    Console.WriteLine("2 — Вибрати проєкт");
    Console.WriteLine("0 — Вийти");
    Console.Write("Ваш вибір: ");
}

// Завантаження проєктів зі сховища та вивід списку

void LoadAndShowProjects()
{
    projects = repository.GetAllProjects();
    PrintProjectList();
}

void PrintProjectList()
{
    if (projects.Count == 0)
    {
        Console.WriteLine("Список проєктів порожній. Натисніть 1, щоб завантажити.");
        return;
    }

    Console.WriteLine("\n── Список проєктів ──");
    foreach (var p in projects)
        Console.WriteLine(p.ToListString());
}

// Вибір проєкту та робота з його завданнями

void SelectProject()
{
    if (projects.Count == 0)
    {
        Console.WriteLine("Спочатку завантажте список проєктів (команда 1).");
        return;
    }

    PrintProjectList();
    Console.Write("\nВведіть ID проєкту: ");

    if (!int.TryParse(Console.ReadLine(), out int projectId))
    {
        Console.WriteLine("Невірний ID.");
        return;
    }

    var project = projects.FirstOrDefault(p => p.Id == projectId);
    if (project is null)
    {
        Console.WriteLine("Проєкт з таким ID не знайдено.");
        return;
    }

    // Завантаження завдань (якщо ще не завантажено)
    repository.LoadTasksForProject(project);

    // Відображення детальної інформації по проєкту
    Console.WriteLine(project.ToDetailString());

    // Відображення списку завдань
    if (project.Tasks.Count == 0)
    {
        Console.WriteLine("У цьому проєкті поки немає завдань.");
    }
    else
    {
        Console.WriteLine("── Завдання проєкту ──");
        foreach (var t in project.Tasks)
            Console.WriteLine(t.ToListString());

        // Пропозиція переглянути повну інформацію по конкретному завданню
        ProjectTaskMenu(project);
    }
}

// Меню перегляду окремого завдання всередині проєкту

void ProjectTaskMenu(ProjectViewModel project)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("d [ID] — переглянути деталі завдання  |  b — назад до головного меню");
        Console.Write("Команда: ");
        string? cmd = Console.ReadLine()?.Trim();

        if (cmd == "b" || cmd == "B") break;

        if (cmd is not null && cmd.StartsWith("d ") &&
            int.TryParse(cmd[2..], out int taskId))
        {
            var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task is null)
                Console.WriteLine("Завдання з таким ID не знайдено.");
            else
                Console.WriteLine(task.ToDetailString());
        }
        else
        {
            Console.WriteLine("Невідома команда.");
        }
    }
}
