using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string ShortDescription { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? WhyItMatters { get; set; }

    [MaxLength(500)]
    public string? ApproachPoints { get; set; }

    [MaxLength(500)]
    public string? DeploymentPoints { get; set; }

    [MaxLength(500)]
    public string? ResultsPoints { get; set; }

    [MaxLength(500)]
    public string? ImprovementPoints { get; set; }

    [MaxLength(300)]
    public string? ReflectionQuestion { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
