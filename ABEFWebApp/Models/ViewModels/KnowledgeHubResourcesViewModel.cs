namespace ABEFWebApp.Models.ViewModels;

public class KnowledgeHubResourcesViewModel
{
    public string? Query { get; init; }
    public string? Section { get; init; }
    public int? CategoryId { get; init; }
    public string? Type { get; init; }
    public int ResultCount { get; init; }
    public bool HasActiveFilters { get; init; }
    public IReadOnlyList<KnowledgeHubFilterOptionViewModel> Sections { get; init; } = [];
    public IReadOnlyList<KnowledgeHubFilterOptionViewModel> Categories { get; init; } = [];
    public IReadOnlyList<string> Types { get; init; } = [];
    public IReadOnlyList<KnowledgeHubResourceCardViewModel> Resources { get; init; } = [];
}

public class KnowledgeHubFilterOptionViewModel
{
    public string Value { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
}

public class KnowledgeHubResourceCardViewModel
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Summary { get; init; } = string.Empty;
    public string SectionKey { get; init; } = string.Empty;
    public string SectionTitle { get; init; } = string.Empty;
    public string ResourceType { get; init; } = string.Empty;
    public string? CategoryName { get; init; }
    public int? CategoryNumber { get; init; }
}

public class KnowledgeHubResourceDetailViewModel
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Summary { get; init; } = string.Empty;
    public string SectionKey { get; init; } = string.Empty;
    public string SectionTitle { get; init; } = string.Empty;
    public string ResourceType { get; init; } = string.Empty;
    public int? CategoryId { get; init; }
    public int? CategoryNumber { get; init; }
    public string? CategoryName { get; init; }
    public bool HasFile { get; init; }
    public bool HasExternalUrl { get; init; }
    public string? ExternalUrl { get; init; }
    public IReadOnlyList<KnowledgeHubResourceCardViewModel> MoreInSection { get; init; } = [];
}
