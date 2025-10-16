using System.ComponentModel.DataAnnotations;

namespace TodoApi.Entities;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public DateTimeOffset DueAt { get; set; }

    [Range(0, 100)]
    public int PercentComplete { get; set; } = 0;

    public bool IsDone { get; set; } = false;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? CompletedAt { get; set; }
}