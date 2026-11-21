namespace ABEFWebApp.Models.ViewModels;

public class CategoriesIndexViewModel
{
    public IReadOnlyList<CategorySummaryViewModel> Drivers { get; init; } = [];
    public IReadOnlyList<CategorySummaryViewModel> Enablers { get; init; } = [];
    public IReadOnlyList<CategorySummaryViewModel> Results { get; init; } = [];
}

public class CategorySummaryViewModel
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
}
