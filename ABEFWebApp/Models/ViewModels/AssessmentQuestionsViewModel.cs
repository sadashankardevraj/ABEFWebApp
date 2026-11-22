using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models.ViewModels;

public class AssessmentQuestionsViewModel
{
    public int AssessmentId { get; set; }

    public int CategoryNumber { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string CategoryType { get; set; } = string.Empty;

    public string OrganisationName { get; set; } = string.Empty;

    public int ProgressPercent { get; set; }

    public bool IsLastCategory => CategoryNumber >= 7;

    public string? ValidationMessage { get; set; }

    public List<AssessmentQuestionInputViewModel> Answers { get; set; } = [];
}

public class AssessmentQuestionInputViewModel
{
    public int QuestionId { get; set; }

    public string ItemCode { get; set; } = string.Empty;

    public string ItemName { get; set; } = string.Empty;

    public string QuestionText { get; set; } = string.Empty;

    [Range(1, 5)]
    public int? Score { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }
}
