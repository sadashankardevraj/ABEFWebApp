namespace ABEFWebApp.Models.ViewModels;

public class ItemDeepDiveViewModel
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? WhyItMatters { get; init; }
    public string? ReflectionQuestion { get; init; }
    public int CategoryNumber { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public IReadOnlyList<string> ApproachPoints { get; init; } = [];
    public IReadOnlyList<string> DeploymentPoints { get; init; } = [];
    public IReadOnlyList<string> ResultsPoints { get; init; } = [];
    public IReadOnlyList<string> ImprovementPoints { get; init; } = [];
    public IReadOnlyList<ItemSiblingViewModel> OtherItems { get; init; } = [];
    public string? PreviousCode { get; init; }
    public string? PreviousName { get; init; }
    public string? NextCode { get; init; }
    public string? NextName { get; init; }
}

public class ItemSiblingViewModel
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
