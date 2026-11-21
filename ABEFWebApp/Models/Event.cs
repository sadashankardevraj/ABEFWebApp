using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Event
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Required]
    [MaxLength(150)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string ShortDescription { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? RegisterUrl { get; set; }

    public bool IsPublished { get; set; } = true;
}
