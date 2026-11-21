namespace ABEFWebApp.Models.ViewModels;

public class KnowledgeHubGlossaryViewModel
{
    public IReadOnlyList<GlossaryTermCardViewModel> Terms { get; init; } = [];
    public IReadOnlyList<char> Letters { get; init; } = [];
}

public class GlossaryTermCardViewModel
{
    public string Term { get; init; } = string.Empty;
    public string Definition { get; init; } = string.Empty;
    public string Letter { get; init; } = string.Empty;
    public string? RelatedUrl { get; init; }
    public string? RelatedLabel { get; init; }
}

public class KnowledgeHubFaqViewModel
{
    public IReadOnlyList<FaqItemViewModel> Items { get; init; } = [];
}

public class FaqItemViewModel
{
    public int Id { get; init; }
    public string Question { get; init; } = string.Empty;
    public string Answer { get; init; } = string.Empty;
}
