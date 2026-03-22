namespace TaskManager.Repositories.Models;

/// <summary>
/// DB Model проєкту — зберігає лише дані, без обчислюваних полів і без колекцій.
/// </summary>
public class ProjectEntity
{
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectType Type { get; set; }

    public ProjectEntity(int id, string name, string description, ProjectType type)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
    }
}
