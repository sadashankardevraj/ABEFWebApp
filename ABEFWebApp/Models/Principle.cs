using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Principle
{
    public int Id { get; set; }

    public int Number { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string ShortDescription { get; set; } = string.Empty;
}
