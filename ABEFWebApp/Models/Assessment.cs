using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class Assessment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(450)]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser? User { get; set; }

    [Required]
    [MaxLength(200)]
    public string OrganisationName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Draft";

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? SubmittedAt { get; set; }

    public int CurrentCategory { get; set; } = 1;

    public ICollection<AssessmentAnswer> Answers { get; set; } = new List<AssessmentAnswer>();
}
