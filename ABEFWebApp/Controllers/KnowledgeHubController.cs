using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

[Authorize]
[Route("[controller]")]
public class KnowledgeHubController : Controller
{
    private static readonly string[] ResourceTypes = ["PDF", "Video", "Link", "Template"];

    private static readonly KnowledgeHubSectionCardViewModel[] SectionDefinitions =
    [
        new()
        {
            Key = "YourCopyOfAbef",
            Title = "Your Copy of ABEF",
            Description = "Member copy of the framework and related materials.",
            IconClass = "bi-journal-bookmark-fill"
        },
        new()
        {
            Key = "HowToApply",
            Title = "How to Apply ABEF",
            Description = "Practical guides for using the framework day to day.",
            IconClass = "bi-lightbulb-fill"
        },
        new()
        {
            Key = "Bpir",
            Title = "BPIR Resource",
            Description = "Benchmarking and improvement resources for members.",
            IconClass = "bi-bar-chart-fill"
        },
        new()
        {
            Key = "Webinars",
            Title = "Webinars",
            Description = "Recorded sessions and learning from BEA events.",
            IconClass = "bi-camera-video-fill"
        },
        new()
        {
            Key = "Documents",
            Title = "Documents",
            Description = "Checklists, notes and supporting documents.",
            IconClass = "bi-file-earmark-text-fill"
        },
        new()
        {
            Key = "Education",
            Title = "Education",
            Description = "Teaching and learning packs for educators.",
            IconClass = "bi-mortarboard-fill"
        },
        new()
        {
            Key = "CaseStudies",
            Title = "Case Studies",
            Description = "Examples of how organisations apply excellence.",
            IconClass = "bi-building"
        },
        new()
        {
            Key = "Tools",
            Title = "Tools & Resources",
            Description = "Templates and worksheets you can use straight away.",
            IconClass = "bi-tools"
        }
    ];

    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _environment;

    public KnowledgeHubController(ApplicationDbContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var counts = await _db.Resources
            .AsNoTracking()
            .Where(r => r.IsPublished)
            .GroupBy(r => r.Section)
            .Select(g => new { Section = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Section, x => x.Count);

        var sections = SectionDefinitions
            .Select(hubSection => new KnowledgeHubSectionCardViewModel
            {
                Key = hubSection.Key,
                Title = hubSection.Title,
                Description = hubSection.Description,
                IconClass = hubSection.IconClass,
                ResourceCount = counts.GetValueOrDefault(hubSection.Key)
            })
            .ToList();

        var sectionTitles = GetSectionTitleMap();

        var recentRows = await _db.Resources
            .AsNoTracking()
            .Where(r => r.IsPublished)
            .OrderByDescending(r => r.CreatedAt)
            .ThenByDescending(r => r.Id)
            .Take(3)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Summary,
                r.Section,
                r.ResourceType,
                r.CreatedAt
            })
            .ToListAsync();

        var recentlyAdded = recentRows
            .Select(r => new KnowledgeHubRecentResourceViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Summary = r.Summary,
                SectionKey = r.Section,
                SectionTitle = sectionTitles.GetValueOrDefault(r.Section, r.Section),
                ResourceType = r.ResourceType,
                CreatedAt = r.CreatedAt
            })
            .ToList();

        return View(new KnowledgeHubIndexViewModel
        {
            Sections = sections,
            RecentlyAdded = recentlyAdded
        });
    }

    [HttpGet("Resources")]
    public async Task<IActionResult> Resources(string? section, int? category, string? type, string? q)
    {
        var selectedSection = NormalizeSection(section);
        var selectedType = NormalizeType(type);
        var queryText = string.IsNullOrWhiteSpace(q) ? null : q.Trim();

        var categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Number)
            .Select(c => new KnowledgeHubFilterOptionViewModel
            {
                Value = c.Id.ToString(),
                Label = $"{c.Number}. {c.Name}"
            })
            .ToListAsync();

        if (category.HasValue && categories.All(c => c.Value != category.Value.ToString()))
        {
            category = null;
        }

        var resourcesQuery = _db.Resources
            .AsNoTracking()
            .Include(r => r.Category)
            .Where(r => r.IsPublished);

        if (!string.IsNullOrWhiteSpace(selectedSection))
        {
            resourcesQuery = resourcesQuery.Where(r => r.Section == selectedSection);
        }

        if (category.HasValue)
        {
            resourcesQuery = resourcesQuery.Where(r => r.CategoryId == category.Value);
        }

        if (!string.IsNullOrWhiteSpace(selectedType))
        {
            resourcesQuery = resourcesQuery.Where(r => r.ResourceType == selectedType);
        }

        if (!string.IsNullOrWhiteSpace(queryText))
        {
            resourcesQuery = resourcesQuery.Where(r =>
                r.Title.Contains(queryText) || r.Summary.Contains(queryText));
        }

        var sectionTitles = GetSectionTitleMap();
        var rows = await resourcesQuery
            .OrderBy(r => r.Title)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Summary,
                r.Section,
                r.ResourceType,
                CategoryName = r.Category != null ? r.Category.Name : null,
                CategoryNumber = r.Category != null ? (int?)r.Category.Number : null
            })
            .ToListAsync();

        var resources = rows
            .Select(r => new KnowledgeHubResourceCardViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Summary = r.Summary,
                SectionKey = r.Section,
                SectionTitle = sectionTitles.GetValueOrDefault(r.Section, r.Section),
                ResourceType = r.ResourceType,
                CategoryName = r.CategoryName,
                CategoryNumber = r.CategoryNumber
            })
            .ToList();

        var viewModel = new KnowledgeHubResourcesViewModel
        {
            Query = queryText,
            Section = selectedSection,
            CategoryId = category,
            Type = selectedType,
            ResultCount = resources.Count,
            HasActiveFilters = !string.IsNullOrWhiteSpace(selectedSection)
                || category.HasValue
                || !string.IsNullOrWhiteSpace(selectedType)
                || !string.IsNullOrWhiteSpace(queryText),
            Sections = SectionDefinitions
                .Select(s => new KnowledgeHubFilterOptionViewModel
                {
                    Value = s.Key,
                    Label = s.Title
                })
                .ToList(),
            Categories = categories,
            Types = ResourceTypes,
            Resources = resources
        };

        return View(viewModel);
    }

    [HttpGet("Resource/{id:int}")]
    public async Task<IActionResult> Resource(int id)
    {
        var resource = await _db.Resources
            .AsNoTracking()
            .Include(r => r.Category)
            .FirstOrDefaultAsync(r => r.Id == id && r.IsPublished);

        if (resource is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("ResourceNotFound");
        }

        var sectionTitles = GetSectionTitleMap();
        var moreRows = await _db.Resources
            .AsNoTracking()
            .Include(r => r.Category)
            .Where(r => r.IsPublished && r.Section == resource.Section && r.Id != resource.Id)
            .OrderBy(r => r.Title)
            .Take(3)
            .Select(r => new
            {
                r.Id,
                r.Title,
                r.Summary,
                r.Section,
                r.ResourceType,
                CategoryName = r.Category != null ? r.Category.Name : null,
                CategoryNumber = r.Category != null ? (int?)r.Category.Number : null
            })
            .ToListAsync();

        var viewModel = new KnowledgeHubResourceDetailViewModel
        {
            Id = resource.Id,
            Title = resource.Title,
            Summary = resource.Summary,
            SectionKey = resource.Section,
            SectionTitle = sectionTitles.GetValueOrDefault(resource.Section, resource.Section),
            ResourceType = resource.ResourceType,
            CategoryId = resource.CategoryId,
            CategoryNumber = resource.Category?.Number,
            CategoryName = resource.Category?.Name,
            HasFile = !string.IsNullOrWhiteSpace(resource.FileName),
            HasExternalUrl = !string.IsNullOrWhiteSpace(resource.ExternalUrl),
            ExternalUrl = resource.ExternalUrl,
            MoreInSection = moreRows
                .Select(r => new KnowledgeHubResourceCardViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Summary = r.Summary,
                    SectionKey = r.Section,
                    SectionTitle = sectionTitles.GetValueOrDefault(r.Section, r.Section),
                    ResourceType = r.ResourceType,
                    CategoryName = r.CategoryName,
                    CategoryNumber = r.CategoryNumber
                })
                .ToList()
        };

        return View(viewModel);
    }

    [HttpGet("Download/{id:int}")]
    public async Task<IActionResult> Download(int id)
    {
        var resource = await _db.Resources
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id && r.IsPublished);

        if (resource is null || string.IsNullOrWhiteSpace(resource.FileName))
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("FileNotFound");
        }

        var safeFileName = Path.GetFileName(resource.FileName);
        if (string.IsNullOrWhiteSpace(safeFileName))
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("FileNotFound");
        }

        var privateFilesRoot = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, "PrivateFiles"));
        var fullPath = Path.GetFullPath(Path.Combine(privateFilesRoot, safeFileName));

        if (!fullPath.StartsWith(privateFilesRoot, StringComparison.OrdinalIgnoreCase)
            || !System.IO.File.Exists(fullPath))
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("FileNotFound");
        }

        var contentType = GetContentType(safeFileName);
        return PhysicalFile(fullPath, contentType, safeFileName);
    }

    [HttpGet("Glossary")]
    public async Task<IActionResult> Glossary()
    {
        var terms = await _db.GlossaryTerms
            .AsNoTracking()
            .OrderBy(t => t.Term)
            .ToListAsync();

        var cards = terms
            .Select(t =>
            {
                var (url, label) = GetGlossaryRelatedLink(t.Term);
                return new GlossaryTermCardViewModel
                {
                    Term = t.Term,
                    Definition = t.Definition,
                    Letter = char.ToUpperInvariant(t.Term[0]).ToString(),
                    RelatedUrl = url,
                    RelatedLabel = label
                };
            })
            .ToList();

        var letters = cards
            .Select(c => c.Letter[0])
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        return View(new KnowledgeHubGlossaryViewModel
        {
            Terms = cards,
            Letters = letters
        });
    }

    [HttpGet("Faq")]
    public async Task<IActionResult> Faq()
    {
        var items = await _db.Faqs
            .AsNoTracking()
            .OrderBy(f => f.SortOrder)
            .ThenBy(f => f.Id)
            .Select(f => new FaqItemViewModel
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer
            })
            .ToListAsync();

        return View(new KnowledgeHubFaqViewModel { Items = items });
    }

    private static Dictionary<string, string> GetSectionTitleMap() =>
        SectionDefinitions.ToDictionary(s => s.Key, s => s.Title);

    private static (string? Url, string? Label) GetGlossaryRelatedLink(string term) =>
        term.ToLowerInvariant() switch
        {
            "adri" or "approach" or "deployment" or "results" or "improvement"
                => ("/Abef/Adri", "Learn ADRI"),
            "category" or "item" or "driver" or "enabler"
                => ("/Categories", "Explore the 7 Categories"),
            "principle"
                => ("/Principles", "View the 9 Principles"),
            "business excellence"
                => ("/Abef", "About ABEF"),
            "self-assessment"
                => ("/Assessment", "Start Assessment"),
            "benchmarking"
                => ("/KnowledgeHub/Resources?section=Bpir", "BPIR resources"),
            "continuous improvement"
                => ("/Abef/HowItWorks", "How ABEF works"),
            "stakeholder"
                => ("/Categories/2", "Customers and stakeholders"),
            _ => (null, null)
        };

    private static string? NormalizeSection(string? section)
    {
        if (string.IsNullOrWhiteSpace(section))
        {
            return null;
        }

        return SectionDefinitions
            .FirstOrDefault(s => s.Key.Equals(section.Trim(), StringComparison.OrdinalIgnoreCase))
            ?.Key;
    }

    private static string? NormalizeType(string? type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return null;
        }

        return ResourceTypes
            .FirstOrDefault(t => t.Equals(type.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private static string GetContentType(string fileName)
    {
        return Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".mp4" => "video/mp4",
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            _ => "application/octet-stream"
        };
    }
}
