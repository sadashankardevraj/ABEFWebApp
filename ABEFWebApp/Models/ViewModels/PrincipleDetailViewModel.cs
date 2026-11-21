namespace ABEFWebApp.Models.ViewModels;

public class PrincipleDetailViewModel
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Explanation { get; init; }
    public IReadOnlyList<string> InPractice { get; init; } = [];
    public IReadOnlyList<string> StrengthSigns { get; init; } = [];
    public IReadOnlyList<string> WeaknessSigns { get; init; } = [];
    public IReadOnlyList<PrincipleRelatedCategoryViewModel> RelatedCategories { get; init; } = [];
    public int? PreviousNumber { get; init; }
    public string? PreviousName { get; init; }
    public int? NextNumber { get; init; }
    public string? NextName { get; init; }
}

public class PrincipleRelatedCategoryViewModel
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
}
