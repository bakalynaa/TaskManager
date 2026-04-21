using System.ComponentModel.DataAnnotations;

namespace TaskManager.Repositories.Models;

/// <summary>DB Model завдання для зберігання в SQLite через EF Core.</summary>
public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    public int ProjectId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}
