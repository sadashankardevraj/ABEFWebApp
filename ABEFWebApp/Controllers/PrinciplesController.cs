using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

[Route("[controller]")]
public class PrinciplesController : Controller
{
    private readonly ApplicationDbContext _db;

    public PrinciplesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var principles = await _db.Principles
            .AsNoTracking()
            .OrderBy(p => p.Number)
            .Select(p => new PrincipleSummaryViewModel
            {
                Number = p.Number,
                Name = p.Name,
                ShortDescription = p.ShortDescription
            })
            .ToListAsync();

        return View(new PrinciplesIndexViewModel { Principles = principles });
    }

    [HttpGet("{number:int}")]
    public async Task<IActionResult> Details(int number)
    {
        var principle = await _db.Principles
            .AsNoTracking()
            .Include(p => p.CategoryPrinciples)
                .ThenInclude(cp => cp.Category)
            .FirstOrDefaultAsync(p => p.Number == number);

        if (principle is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound");
        }

        var allPrinciples = await _db.Principles
            .AsNoTracking()
            .OrderBy(p => p.Number)
            .Select(p => new { p.Number, p.Name })
            .ToListAsync();

        var index = allPrinciples.FindIndex(p => p.Number == principle.Number);
        var previous = index > 0 ? allPrinciples[index - 1] : null;
        var next = index >= 0 && index < allPrinciples.Count - 1 ? allPrinciples[index + 1] : null;

        var viewModel = new PrincipleDetailViewModel
        {
            Number = principle.Number,
            Name = principle.Name,
            Explanation = principle.Explanation,
            InPractice = SplitPipeValues(principle.InPractice),
            StrengthSigns = SplitPipeValues(principle.StrengthSigns),
            WeaknessSigns = SplitPipeValues(principle.WeaknessSigns),
            RelatedCategories = principle.CategoryPrinciples
                .Where(cp => cp.Category is not null)
                .OrderBy(cp => cp.Category!.Number)
                .Select(cp => new PrincipleRelatedCategoryViewModel
                {
                    Number = cp.Category!.Number,
                    Name = cp.Category.Name
                })
                .ToList(),
            PreviousNumber = previous?.Number,
            PreviousName = previous?.Name,
            NextNumber = next?.Number,
            NextName = next?.Name
        };

        return View(viewModel);
    }

    private static IReadOnlyList<string> SplitPipeValues(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return [];
        }

        return value
            .Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }
}
