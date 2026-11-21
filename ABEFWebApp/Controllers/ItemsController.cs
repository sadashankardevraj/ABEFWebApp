using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

[Route("[controller]")]
public class ItemsController : Controller
{
    private readonly ApplicationDbContext _db;

    public ItemsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> Details(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound");
        }

        var item = await _db.Items
            .AsNoTracking()
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.Code == code);

        if (item is null || item.Category is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound");
        }

        var siblings = await _db.Items
            .AsNoTracking()
            .Where(i => i.CategoryId == item.CategoryId)
            .OrderBy(i => i.Code)
            .Select(i => new ItemSiblingViewModel
            {
                Code = i.Code,
                Name = i.Name
            })
            .ToListAsync();

        var index = siblings.FindIndex(i => i.Code == item.Code);
        var previous = index > 0 ? siblings[index - 1] : null;
        var next = index >= 0 && index < siblings.Count - 1 ? siblings[index + 1] : null;

        var viewModel = new ItemDeepDiveViewModel
        {
            Code = item.Code,
            Name = item.Name,
            WhyItMatters = item.WhyItMatters,
            ReflectionQuestion = item.ReflectionQuestion,
            CategoryNumber = item.Category.Number,
            CategoryName = item.Category.Name,
            ApproachPoints = SplitPipeValues(item.ApproachPoints),
            DeploymentPoints = SplitPipeValues(item.DeploymentPoints),
            ResultsPoints = SplitPipeValues(item.ResultsPoints),
            ImprovementPoints = SplitPipeValues(item.ImprovementPoints),
            OtherItems = siblings.Where(i => i.Code != item.Code).ToList(),
            PreviousCode = previous?.Code,
            PreviousName = previous?.Name,
            NextCode = next?.Code,
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
