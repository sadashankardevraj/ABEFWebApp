using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Resource
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Summary { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Section { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string ResourceType { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    public Category? Category { get; set; }

    [MaxLength(260)]
    public string? FileName { get; set; }

    [MaxLength(300)]
    public string? ExternalUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; } = true;
}
