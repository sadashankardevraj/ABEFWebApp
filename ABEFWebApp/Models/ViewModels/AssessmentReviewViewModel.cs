using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models.ViewModels;

public class AssessmentReviewViewModel
{
    public int AssessmentId { get; set; }

    public string OrganisationName { get; set; } = string.Empty;

    public bool IsComplete { get; set; }

    public IReadOnlyList<AssessmentReviewCategoryViewModel> Categories { get; init; } = [];
}

public class AssessmentReviewCategoryViewModel
{
    public int Number { get; init; }

    public string Name { get; init; } = string.Empty;

    public int AnsweredCount { get; init; }

    public int QuestionCount { get; init; }

    public bool IsComplete => AnsweredCount >= QuestionCount && QuestionCount > 0;

    public IReadOnlyList<AssessmentReviewItemViewModel> Items { get; init; } = [];
}

public class AssessmentReviewItemViewModel
{
    public string ItemCode { get; init; } = string.Empty;

    public int? Score { get; init; }

    public string ScoreLabel { get; init; } = string.Empty;
}

public class AssessmentSubmitViewModel
{
    public int AssessmentId { get; set; }

    public string OrganisationName { get; set; } = string.Empty;

    [Display(Name = "I confirm these answers reflect our organisation.")]
    public bool Confirmed { get; set; }
}

public class AssessmentThanksViewModel
{
    public int AssessmentId { get; set; }

    public string OrganisationName { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; }
}
