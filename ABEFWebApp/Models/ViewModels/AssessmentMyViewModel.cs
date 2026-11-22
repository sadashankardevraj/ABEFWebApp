namespace ABEFWebApp.Models.ViewModels;

public class AssessmentMyViewModel
{
    public IReadOnlyList<AssessmentListItemViewModel> Assessments { get; init; } = [];
}

public class AssessmentListItemViewModel
{
    public int Id { get; init; }
    public string OrganisationName { get; init; } = string.Empty;
    public DateTime StartedAt { get; init; }
    public string Status { get; init; } = string.Empty;
    public int CurrentCategory { get; init; }
    public bool IsDraft => string.Equals(Status, "Draft", StringComparison.OrdinalIgnoreCase);
}
