using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

public class EventsController : Controller
{
    private static readonly string[] AllowedFilters = ["All", "Webinar", "Workshop", "Conference"];
    private readonly ApplicationDbContext _db;

    public EventsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? type)
    {
        var activeFilter = AllowedFilters.Contains(type, StringComparer.OrdinalIgnoreCase)
            ? AllowedFilters.First(f => f.Equals(type, StringComparison.OrdinalIgnoreCase))
            : "All";

        var today = DateTime.Today;
        var query = _db.Events
            .AsNoTracking()
            .Where(e => e.IsPublished && e.Date >= today);

        if (!string.Equals(activeFilter, "All", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(e => e.Type == activeFilter);
        }

        var events = await query
            .OrderBy(e => e.Date)
            .ToListAsync();

        var viewModel = new EventsIndexViewModel
        {
            ActiveFilter = activeFilter,
            Events = events
        };

        return View(viewModel);
    }
}
