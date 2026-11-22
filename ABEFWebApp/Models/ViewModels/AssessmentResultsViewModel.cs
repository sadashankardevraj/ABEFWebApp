namespace ABEFWebApp.Models.ViewModels;

public class AssessmentResultsViewModel
{
    public int AssessmentId { get; init; }

    public string OrganisationName { get; init; } = string.Empty;

    public DateTime SubmittedAt { get; init; }

    public decimal OverallAverage { get; init; }

    public decimal OverallPercentage { get; init; }

    public string OverallLevel { get; init; } = string.Empty;

    public string SummarySentence { get; init; } = string.Empty;

    public IReadOnlyList<AssessmentCategoryResultViewModel> Categories { get; init; } = [];

    public IReadOnlyList<AssessmentCategoryResultViewModel> Strengths { get; init; } = [];

    public IReadOnlyList<AssessmentCategoryResultViewModel> AreasToImprove { get; init; } = [];

    public IReadOnlyList<AssessmentRecommendationViewModel> Recommendations { get; init; } = [];

    public IReadOnlyList<AssessmentResultItemViewModel> Items { get; init; } = [];
}

public class AssessmentResultItemViewModel
{
    public int CategoryNumber { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string ItemCode { get; init; } = string.Empty;

    public string ItemName { get; init; } = string.Empty;

    public int? Score { get; init; }

    public string Level { get; init; } = string.Empty;

    public string? Comment { get; init; }
}

public class AssessmentCategoryResultViewModel
{
    public int Number { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public decimal Average { get; init; }

    public decimal Percentage { get; init; }

    public string Level { get; init; } = string.Empty;
}

public class AssessmentRecommendationViewModel
{
    public int CategoryNumber { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public decimal Average { get; init; }

    public string Level { get; init; } = string.Empty;

    public string Recommendation { get; init; } = string.Empty;

    public string? LowestItemCode { get; init; }

    public int? LowestItemScore { get; init; }

    public string? LowestItemLevel { get; init; }

    public IReadOnlyList<AssessmentRecommendationResourceViewModel> Resources { get; init; } = [];
}

public class AssessmentRecommendationResourceViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;
}

public class AssessmentOverallScore
{
    public int AssessmentId { get; init; }

    public decimal OverallAverage { get; init; }

    public decimal OverallPercentage { get; init; }

    public string OverallLevel { get; init; } = string.Empty;
}

public class AssessmentLatestResultViewModel
{
    public int AssessmentId { get; init; }

    public decimal OverallPercentage { get; init; }

    public string OverallLevel { get; init; } = string.Empty;

    public DateTime SubmittedAt { get; init; }
}
