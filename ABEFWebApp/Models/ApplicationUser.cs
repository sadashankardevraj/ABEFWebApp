using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ABEFWebApp.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Organisation { get; set; }
}
