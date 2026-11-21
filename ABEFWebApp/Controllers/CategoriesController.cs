using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

[Route("[controller]")]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoriesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Number)
            .Select(c => new CategorySummaryViewModel
            {
                Number = c.Number,
                Name = c.Name,
                Type = c.Type,
                ShortDescription = c.ShortDescription
            })
            .ToListAsync();

        var viewModel = new CategoriesIndexViewModel
        {
            Drivers = categories
                .Where(c => c.Type.Equals("Driver", StringComparison.OrdinalIgnoreCase))
                .ToList(),
            Enablers = categories
                .Where(c => c.Type.Equals("Enabler", StringComparison.OrdinalIgnoreCase))
                .ToList(),
            Results = categories
                .Where(c => c.Type.Equals("Result", StringComparison.OrdinalIgnoreCase))
                .ToList()
        };

        return View(viewModel);
    }

    [HttpGet("{number:int}")]
    public async Task<IActionResult> Details(int number)
    {
        var category = await _db.Categories
            .AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Number == number);

        if (category is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound");
        }

        var allCategories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Number)
            .Select(c => new { c.Number, c.Name })
            .ToListAsync();

        var index = allCategories.FindIndex(c => c.Number == category.Number);
        var previous = index > 0 ? allCategories[index - 1] : null;
        var next = index >= 0 && index < allCategories.Count - 1 ? allCategories[index + 1] : null;

        var viewModel = new CategoryHubViewModel
        {
            Number = category.Number,
            Name = category.Name,
            Type = category.Type,
            Purpose = category.Purpose,
            Tips = SplitPipeValues(category.Tips),
            Items = category.Items
                .OrderBy(i => i.Code)
                .Select(i => new CategoryItemSummaryViewModel
                {
                    Code = i.Code,
                    Name = i.Name,
                    ShortDescription = i.ShortDescription
                })
                .ToList(),
            FrameworkNumbers = allCategories.Select(c => c.Number).ToList(),
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
