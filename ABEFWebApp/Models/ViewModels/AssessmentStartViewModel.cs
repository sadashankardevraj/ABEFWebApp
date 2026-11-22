using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models.ViewModels;

public class AssessmentStartViewModel
{
    [Required(ErrorMessage = "Organisation name is required.")]
    [MaxLength(200)]
    [Display(Name = "Organisation name")]
    public string OrganisationName { get; set; } = string.Empty;

    public int? DraftAssessmentId { get; set; }

    public int DraftCurrentCategory { get; set; } = 1;

    public bool HasDraft => DraftAssessmentId.HasValue;
}
