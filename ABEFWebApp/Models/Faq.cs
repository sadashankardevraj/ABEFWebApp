using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Faq
{
    public int Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string Question { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Answer { get; set; } = string.Empty;

    public int SortOrder { get; set; }
}
