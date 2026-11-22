using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Category
{
    public int Id { get; set; }

    public int Number { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string ShortDescription { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Purpose { get; set; }

    [MaxLength(500)]
    public string? Tips { get; set; }

    [MaxLength(800)]
    public string? Recommendation { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public ICollection<CategoryPrinciple> CategoryPrinciples { get; set; } = new List<CategoryPrinciple>();
}
