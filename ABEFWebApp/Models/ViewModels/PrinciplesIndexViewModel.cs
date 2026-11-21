namespace ABEFWebApp.Models.ViewModels;

public class PrinciplesIndexViewModel
{
    public IReadOnlyList<PrincipleSummaryViewModel> Principles { get; init; } = [];
}

public class PrincipleSummaryViewModel
{
    public int Number { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
}
