namespace TaskManager.Models;

// Клас для зберігання даних проєкту.
// Не містить обчислюваних полів і не містить колекції завдань —
// зв'язок між проєктами та завданнями підтримується через ProjectId у TaskEntity.

public class ProjectEntity
{
    // Унікальний ідентифікатор проєкту 
    public int Id { get; }

    // Назва проєкту
    public string Name { get; set; }

    // Короткий опис проєкту
    public string Description { get; set; }

    // Тип проєкту
    public ProjectType Type { get; set; }
    
    // Основний конструктор
    public ProjectEntity(int id, string name, string description, ProjectType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }
}
