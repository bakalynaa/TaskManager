using TaskManager.Repositories.Models;

namespace TaskManager.Repositories.Storage;

/// <summary>
/// Штучне сховище даних. Доступне лише всередині бібліотеки Repositories.
/// </summary>
internal static class FakeStorage
{
    internal static readonly List<ProjectEntity> Projects;
    internal static readonly List<TaskEntity> Tasks;

    static FakeStorage()
    {
        Projects = new List<ProjectEntity>
        {
            new(1, "Дипломна робота", "Розробка веб-застосунку для управління задачами на ASP.NET Core", ProjectType.Educational),
            new(2, "Корпоративний портал", "Внутрішній портал компанії з управлінням документами та HR-модулем", ProjectType.Work),
            new(3, "Особистий блог", "Блог на статичному генераторі Hugo із темою про технології", ProjectType.Personal),
        };

        Tasks = new List<TaskEntity>
        {
            new(1,  1, "Визначити тему та мету роботи",  "Обговорити з науковим керівником і затвердити тему",         TaskPriority.Critical, new DateTime(2025, 1, 15),  true),
            new(2,  1, "Написати вступ",                 "Актуальність, мета, завдання, об'єкт і предмет дослідження", TaskPriority.High,     new DateTime(2025, 2, 1),   true),
            new(3,  1, "Огляд літератури",               "Аналіз існуючих рішень і наукових статей за темою",          TaskPriority.High,     new DateTime(2025, 2, 20),  true),
            new(4,  1, "Проєктування архітектури",       "UML-діаграми, ER-модель бази даних, опис шарів застосунку",  TaskPriority.Critical, new DateTime(2025, 3, 5),   true),
            new(5,  1, "Реалізація бекенду",             "REST API на ASP.NET Core з автентифікацією через JWT",       TaskPriority.Critical, new DateTime(2025, 4, 1),   true),
            new(6,  1, "Реалізація фронтенду",           "SPA на React із підключенням до API",                        TaskPriority.High,     new DateTime(2025, 4, 25),  true),
            new(7,  1, "Тестування",                     "Unit та інтеграційні тести, ручне тестування інтерфейсу",    TaskPriority.Medium,   new DateTime(2025, 5, 10),  false),
            new(8,  1, "Написати висновки",              "Підсумки роботи, результати, подальші дослідження",          TaskPriority.High,     new DateTime(2025, 5, 20),  false),
            new(9,  1, "Оформити документацію",          "Привести документ у відповідність до ДСТУ та вимог кафедри", TaskPriority.Medium,   new DateTime(2025, 5, 28),  false),
            new(10, 1, "Підготувати презентацію",        "Слайди для захисту: короткий виклад роботи, демонстрація",   TaskPriority.High,     new DateTime(2025, 6, 1),   false),
            new(11, 2, "Зібрати вимоги від замовника",  "Провести інтерв'ю з HR, IT та бухгалтерією",                TaskPriority.Critical, new DateTime(2025, 3, 10),  true),
            new(12, 2, "Розробити макети інтерфейсу",   "Wireframes у Figma для головних сторінок порталу",           TaskPriority.High,     new DateTime(2025, 4, 15),  false),
        };
    }
}
