namespace ABEFWebApp.Models.ViewModels;

public class EventsIndexViewModel
{
    public string ActiveFilter { get; init; } = "All";
    public IReadOnlyList<Event> Events { get; init; } = [];
}
