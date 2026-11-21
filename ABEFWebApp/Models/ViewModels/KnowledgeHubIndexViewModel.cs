namespace ABEFWebApp.Models.ViewModels;

public class KnowledgeHubIndexViewModel
{
    public IReadOnlyList<KnowledgeHubSectionCardViewModel> Sections { get; init; } = [];
    public IReadOnlyList<KnowledgeHubRecentResourceViewModel> RecentlyAdded { get; init; } = [];
}

public class KnowledgeHubSectionCardViewModel
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string IconClass { get; init; } = string.Empty;
    public int ResourceCount { get; init; }
}

public class KnowledgeHubRecentResourceViewModel
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Summary { get; init; } = string.Empty;
    public string SectionKey { get; init; } = string.Empty;
    public string SectionTitle { get; init; } = string.Empty;
    public string ResourceType { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
