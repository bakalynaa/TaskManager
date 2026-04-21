using System.ComponentModel.DataAnnotations;

namespace TaskManager.Repositories.Models;

/// <summary>DB Model проєкту для зберігання в SQLite через EF Core.</summary>
public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public ProjectType Type { get; set; }
}
