namespace ABEFWebApp.Models.ViewModels;

public class CategoryHubViewModel
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string? Purpose { get; init; }
    public IReadOnlyList<string> Tips { get; init; } = [];
    public IReadOnlyList<CategoryItemSummaryViewModel> Items { get; init; } = [];
    public IReadOnlyList<int> FrameworkNumbers { get; init; } = [];
    public int? PreviousNumber { get; init; }
    public string? PreviousName { get; init; }
    public int? NextNumber { get; init; }
    public string? NextName { get; init; }
}

public class CategoryItemSummaryViewModel
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
}
