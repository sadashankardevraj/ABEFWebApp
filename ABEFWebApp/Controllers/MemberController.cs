using ABEFWebApp.Data;
using ABEFWebApp.Models;
using ABEFWebApp.Models.ViewModels;
using ABEFWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

[Authorize]
public class MemberController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly ResultsService _resultsService;

    public MemberController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        ResultsService resultsService)
    {
        _userManager = userManager;
        _db = db;
        _resultsService = resultsService;
    }

    public async Task<IActionResult> Dashboard()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = ResolveDisplayRole(roles);
        var isAdmin = roles.Contains("Admin");
        var canReview = isAdmin || roles.Contains("Evaluator");

        var upcomingEvents = await _db.Events
            .AsNoTracking()
            .Where(e => e.IsPublished && e.Date >= DateTime.Today)
            .OrderBy(e => e.Date)
            .Take(2)
            .Select(e => new DashboardEventViewModel
            {
                Title = e.Title,
                Date = e.Date
            })
            .ToListAsync();

        var latestResult = await _resultsService.GetLatestSubmittedResultAsync(user.Id);

        var viewModel = new MemberDashboardViewModel
        {
            FirstName = string.IsNullOrWhiteSpace(user.FirstName) ? "Member" : user.FirstName,
            Role = role,
            Organisation = user.Organisation,
            ShowProfileNote = string.IsNullOrWhiteSpace(user.PhoneNumber)
                || string.IsNullOrWhiteSpace(user.Organisation),
            CanReviewAssessments = canReview,
            IsAdmin = isAdmin,
            LatestResult = latestResult,
            UpcomingEvents = upcomingEvents
        };

        return View(viewModel);
    }

    private static string ResolveDisplayRole(IList<string> roles)
    {
        if (roles.Contains("Admin"))
        {
            return "Admin";
        }

        if (roles.Contains("Evaluator"))
        {
            return "Evaluator";
        }

        return "Member";
    }
}
