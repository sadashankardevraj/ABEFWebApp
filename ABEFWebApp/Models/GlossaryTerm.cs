using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class GlossaryTerm
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Term { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Definition { get; set; } = string.Empty;
}
