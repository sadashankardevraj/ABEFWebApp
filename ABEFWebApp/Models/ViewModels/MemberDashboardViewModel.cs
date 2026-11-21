namespace ABEFWebApp.Models.ViewModels;

public class MemberDashboardViewModel
{
    public string FirstName { get; init; } = "Member";
    public string Role { get; init; } = "Member";
    public string? Organisation { get; init; }
    public bool ShowProfileNote { get; init; }
    public bool CanReviewAssessments { get; init; }
    public bool IsAdmin { get; init; }
    public IReadOnlyList<DashboardEventViewModel> UpcomingEvents { get; init; } = [];
}

public class DashboardEventViewModel
{
    public string Title { get; init; } = string.Empty;
    public DateTime Date { get; init; }
}
