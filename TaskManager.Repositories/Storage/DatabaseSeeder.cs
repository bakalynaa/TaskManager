using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Storage;

/// <summary>
/// Заповнює базу даних початковими даними при першому запуску.
/// </summary>
public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Якщо дані вже є — не заповнюємо повторно
        if (context.Projects.Any()) return;

        var projects = new List<ProjectEntity>
        {
            new() { Name = "Дипломна робота", Description = "Розробка веб-застосунку для управління задачами на ASP.NET Core", Type = ProjectType.Educational },
            new() { Name = "Корпоративний портал", Description = "Внутрішній портал компанії з управлінням документами та HR-модулем", Type = ProjectType.Work },
            new() { Name = "Особистий блог", Description = "Блог на статичному генераторі Hugo із темою про технології", Type = ProjectType.Personal },
        };

        await context.Projects.AddRangeAsync(projects);
        await context.SaveChangesAsync();

        var p1 = projects[0].Id;
        var p2 = projects[1].Id;

        var tasks = new List<TaskEntity>
        {
            new() { ProjectId = p1, Title = "Визначити тему та мету роботи",  Description = "Обговорити з науковим керівником і затвердити тему",         Priority = TaskPriority.Critical, DueDate = new DateTime(2025, 1, 15),  IsCompleted = true  },
            new() { ProjectId = p1, Title = "Написати вступ",                 Description = "Актуальність, мета, завдання, об'єкт і предмет дослідження", Priority = TaskPriority.High,     DueDate = new DateTime(2025, 2, 1),   IsCompleted = true  },
            new() { ProjectId = p1, Title = "Огляд літератури",               Description = "Аналіз існуючих рішень і наукових статей за темою",          Priority = TaskPriority.High,     DueDate = new DateTime(2025, 2, 20),  IsCompleted = true  },
            new() { ProjectId = p1, Title = "Проєктування архітектури",       Description = "UML-діаграми, ER-модель бази даних, опис шарів застосунку",  Priority = TaskPriority.Critical, DueDate = new DateTime(2025, 3, 5),   IsCompleted = true  },
            new() { ProjectId = p1, Title = "Реалізація бекенду",             Description = "REST API на ASP.NET Core з автентифікацією через JWT",       Priority = TaskPriority.Critical, DueDate = new DateTime(2025, 4, 1),   IsCompleted = true  },
            new() { ProjectId = p1, Title = "Реалізація фронтенду",           Description = "SPA на React із підключенням до API",                        Priority = TaskPriority.High,     DueDate = new DateTime(2025, 4, 25),  IsCompleted = true  },
            new() { ProjectId = p1, Title = "Тестування",                     Description = "Unit та інтеграційні тести, ручне тестування інтерфейсу",    Priority = TaskPriority.Medium,   DueDate = new DateTime(2025, 5, 10),  IsCompleted = false },
            new() { ProjectId = p1, Title = "Написати висновки",              Description = "Підсумки роботи, результати, подальші дослідження",          Priority = TaskPriority.High,     DueDate = new DateTime(2025, 5, 20),  IsCompleted = false },
            new() { ProjectId = p1, Title = "Оформити документацію",          Description = "Привести документ у відповідність до ДСТУ та вимог кафедри", Priority = TaskPriority.Medium,   DueDate = new DateTime(2025, 5, 28),  IsCompleted = false },
            new() { ProjectId = p1, Title = "Підготувати презентацію",        Description = "Слайди для захисту: короткий виклад роботи, демонстрація",   Priority = TaskPriority.High,     DueDate = new DateTime(2025, 6, 1),   IsCompleted = false },
            new() { ProjectId = p2, Title = "Зібрати вимоги від замовника",  Description = "Провести інтерв'ю з HR, IT та бухгалтерією",                Priority = TaskPriority.Critical, DueDate = new DateTime(2025, 3, 10),  IsCompleted = true  },
            new() { ProjectId = p2, Title = "Розробити макети інтерфейсу",   Description = "Wireframes у Figma для головних сторінок порталу",           Priority = TaskPriority.High,     DueDate = new DateTime(2025, 4, 15),  IsCompleted = false },
        };

        await context.Tasks.AddRangeAsync(tasks);
        await context.SaveChangesAsync();
    }
}
