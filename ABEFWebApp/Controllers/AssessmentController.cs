using System.Text;
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
[Route("[controller]")]
public class AssessmentController : Controller
{
    private static readonly string[] ScoreLabels =
    [
        "",
        "Not evident",
        "Limited",
        "Developing",
        "Established",
        "Leading"
    ];

    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ResultsService _resultsService;

    public AssessmentController(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        ResultsService resultsService)
    {
        _db = db;
        _userManager = userManager;
        _resultsService = resultsService;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var draft = await _db.Assessments
            .AsNoTracking()
            .Where(a => a.UserId == user.Id && a.Status == "Draft")
            .OrderByDescending(a => a.StartedAt)
            .FirstOrDefaultAsync();

        var viewModel = new AssessmentStartViewModel
        {
            OrganisationName = string.IsNullOrWhiteSpace(user.Organisation)
                ? string.Empty
                : user.Organisation,
            DraftAssessmentId = draft?.Id,
            DraftCurrentCategory = draft?.CurrentCategory is >= 1 and <= 7
                ? draft.CurrentCategory
                : 1
        };

        return View(viewModel);
    }

    [HttpGet("My")]
    public async Task<IActionResult> My()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var rows = await _db.Assessments
            .AsNoTracking()
            .Where(a => a.UserId == user.Id)
            .OrderByDescending(a => a.StartedAt)
            .Select(a => new
            {
                a.Id,
                a.OrganisationName,
                a.StartedAt,
                a.Status,
                a.CurrentCategory
            })
            .ToListAsync();

        var submittedIds = rows
            .Where(a => string.Equals(a.Status, "Submitted", StringComparison.OrdinalIgnoreCase))
            .Select(a => a.Id)
            .ToList();

        var scores = await _resultsService.GetOverallScoresAsync(submittedIds);

        var assessments = rows
            .Select(a => new AssessmentListItemViewModel
            {
                Id = a.Id,
                OrganisationName = a.OrganisationName,
                StartedAt = a.StartedAt,
                Status = a.Status,
                CurrentCategory = a.CurrentCategory is >= 1 and <= 7 ? a.CurrentCategory : 1,
                OverallPercentage = scores.TryGetValue(a.Id, out var score)
                    ? score.OverallPercentage
                    : null
            })
            .ToList();

        return View(new AssessmentMyViewModel { Assessments = assessments });
    }

    [HttpPost("Start")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Start(AssessmentStartViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            var draft = await _db.Assessments
                .AsNoTracking()
                .Where(a => a.UserId == user.Id && a.Status == "Draft")
                .OrderByDescending(a => a.StartedAt)
                .FirstOrDefaultAsync();

            model.DraftAssessmentId = draft?.Id;
            model.DraftCurrentCategory = draft?.CurrentCategory is >= 1 and <= 7
                ? draft.CurrentCategory
                : 1;

            return View("Index", model);
        }

        var assessment = new Assessment
        {
            UserId = user.Id,
            OrganisationName = model.OrganisationName.Trim(),
            Status = "Draft",
            StartedAt = DateTime.UtcNow,
            CurrentCategory = 1
        };

        _db.Assessments.Add(assessment);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Questions), new { id = assessment.Id, category = 1 });
    }

    [HttpGet("Questions/{id:int}")]
    public async Task<IActionResult> Questions(int id, int category = 1)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id);
        if (assessment is null)
        {
            return NotFound();
        }

        if (IsSubmitted(assessment))
        {
            return RedirectToAction(nameof(Thanks), new { id });
        }

        var viewModel = await BuildQuestionsViewModelAsync(assessment, category);
        if (viewModel is null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [HttpPost("Questions/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Questions(int id, int category, string navAction, AssessmentQuestionsViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id, track: true);
        if (assessment is null)
        {
            return NotFound();
        }

        if (IsSubmitted(assessment))
        {
            return RedirectToAction(nameof(Thanks), new { id });
        }

        category = NormalizeCategory(category);
        var postedAnswers = model.Answers ?? [];
        await SaveAnswersAsync(assessment.Id, postedAnswers);

        var action = (navAction ?? string.Empty).Trim().ToLowerInvariant();

        if (action == "save")
        {
            assessment.CurrentCategory = category;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        if (action == "previous")
        {
            var previousCategory = Math.Max(1, category - 1);
            assessment.CurrentCategory = previousCategory;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Questions), new { id, category = previousCategory });
        }

        // Validate only when moving forward (Next / Review answers).
        var expectedQuestionIds = await GetCategoryQuestionIdsAsync(category);
        var unanswered = expectedQuestionIds.Any(questionId =>
        {
            var posted = postedAnswers.FirstOrDefault(a => a.QuestionId == questionId);
            return posted?.Score is not (>= 1 and <= 5);
        });

        if (unanswered)
        {
            var viewModel = await BuildQuestionsViewModelAsync(assessment, category, postedAnswers);
            if (viewModel is null)
            {
                return NotFound();
            }

            viewModel.ValidationMessage = "Please answer all questions in this category.";
            return View(viewModel);
        }

        if (category >= 7)
        {
            assessment.CurrentCategory = 7;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Review), new { id });
        }

        var nextCategory = category + 1;
        assessment.CurrentCategory = nextCategory;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Questions), new { id, category = nextCategory });
    }

    [HttpGet("Review/{id:int}")]
    public async Task<IActionResult> Review(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id);
        if (assessment is null)
        {
            return NotFound();
        }

        if (IsSubmitted(assessment))
        {
            return RedirectToAction(nameof(Thanks), new { id });
        }

        var viewModel = await BuildReviewViewModelAsync(assessment);
        return View(viewModel);
    }

    [HttpGet("Submit/{id:int}")]
    public async Task<IActionResult> Submit(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id);
        if (assessment is null)
        {
            return NotFound();
        }

        if (IsSubmitted(assessment))
        {
            return RedirectToAction(nameof(Thanks), new { id });
        }

        var review = await BuildReviewViewModelAsync(assessment);
        if (!review.IsComplete)
        {
            return RedirectToAction(nameof(Review), new { id });
        }

        return View(new AssessmentSubmitViewModel
        {
            AssessmentId = assessment.Id,
            OrganisationName = assessment.OrganisationName
        });
    }

    [HttpPost("Submit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(int id, AssessmentSubmitViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id, track: true);
        if (assessment is null)
        {
            return NotFound();
        }

        if (IsSubmitted(assessment))
        {
            return RedirectToAction(nameof(Thanks), new { id });
        }

        var review = await BuildReviewViewModelAsync(assessment);
        if (!review.IsComplete)
        {
            return RedirectToAction(nameof(Review), new { id });
        }

        model.AssessmentId = assessment.Id;
        model.OrganisationName = assessment.OrganisationName;

        // Checkbox posts "true" when checked; do not use [Range] on bool (client validation breaks).
        if (!model.Confirmed)
        {
            ModelState.AddModelError(nameof(model.Confirmed), "Please confirm before submitting.");
            return View(model);
        }

        assessment.Status = "Submitted";
        assessment.SubmittedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Thanks), new { id });
    }

    [HttpGet("Thanks/{id:int}")]
    public async Task<IActionResult> Thanks(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var assessment = await GetOwnedAssessmentAsync(id, user.Id);
        if (assessment is null)
        {
            return NotFound();
        }

        if (!IsSubmitted(assessment) || assessment.SubmittedAt is null)
        {
            return RedirectToAction(nameof(Review), new { id });
        }

        return View(new AssessmentThanksViewModel
        {
            AssessmentId = assessment.Id,
            OrganisationName = assessment.OrganisationName,
            SubmittedAt = assessment.SubmittedAt.Value
        });
    }

    [HttpGet("Results/{id:int}")]
    public async Task<IActionResult> Results(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var viewModel = await _resultsService.BuildAsync(id, user.Id);
        if (viewModel is null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [HttpGet("ExportCsv/{id:int}")]
    public async Task<IActionResult> ExportCsv(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var viewModel = await _resultsService.BuildAsync(id, user.Id);
        if (viewModel is null)
        {
            return NotFound();
        }

        var csv = ResultsService.BuildCsv(viewModel);
        var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray();
        var fileName = ResultsService.BuildExportFileName(viewModel, "csv");
        return File(bytes, "text/csv; charset=utf-8", fileName);
    }

    [HttpGet("ExportPdf/{id:int}")]
    public async Task<IActionResult> ExportPdf(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var viewModel = await _resultsService.BuildAsync(id, user.Id);
        if (viewModel is null)
        {
            return NotFound();
        }

        var pdf = _resultsService.BuildPdf(viewModel);
        var fileName = ResultsService.BuildExportFileName(viewModel, "pdf");
        return File(pdf, "application/pdf", fileName);
    }

    private async Task<Assessment?> GetOwnedAssessmentAsync(int id, string userId, bool track = false)
    {
        var query = track ? _db.Assessments.AsQueryable() : _db.Assessments.AsNoTracking();
        return await query.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    private static bool IsSubmitted(Assessment assessment) =>
        string.Equals(assessment.Status, "Submitted", StringComparison.OrdinalIgnoreCase);

    private async Task<AssessmentReviewViewModel> BuildReviewViewModelAsync(Assessment assessment)
    {
        var categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Number)
            .ToListAsync();

        var questions = await _db.AssessmentQuestions
            .AsNoTracking()
            .Include(q => q.Item)
            .Where(q => q.Item != null)
            .OrderBy(q => q.Item!.Code)
            .ToListAsync();

        var answers = await _db.AssessmentAnswers
            .AsNoTracking()
            .Where(a => a.AssessmentId == assessment.Id)
            .ToDictionaryAsync(a => a.QuestionId);

        var categoryCards = categories.Select(category =>
        {
            var categoryQuestions = questions
                .Where(q => q.Item!.CategoryId == category.Id)
                .ToList();

            var items = categoryQuestions.Select(q =>
            {
                answers.TryGetValue(q.Id, out var answer);
                var score = answer?.Score is >= 1 and <= 5 ? answer.Score : (int?)null;
                return new AssessmentReviewItemViewModel
                {
                    ItemCode = q.Item!.Code,
                    Score = score,
                    ScoreLabel = score.HasValue ? ScoreLabels[score.Value] : "Not answered"
                };
            }).ToList();

            var answeredCount = items.Count(i => i.Score.HasValue);

            return new AssessmentReviewCategoryViewModel
            {
                Number = category.Number,
                Name = category.Name,
                AnsweredCount = answeredCount,
                QuestionCount = categoryQuestions.Count,
                Items = items
            };
        }).ToList();

        return new AssessmentReviewViewModel
        {
            AssessmentId = assessment.Id,
            OrganisationName = assessment.OrganisationName,
            IsComplete = categoryCards.Sum(c => c.AnsweredCount) >= 21
                && categoryCards.All(c => c.IsComplete),
            Categories = categoryCards
        };
    }

    private async Task<AssessmentQuestionsViewModel?> BuildQuestionsViewModelAsync(
        Assessment assessment,
        int category,
        IReadOnlyList<AssessmentQuestionInputViewModel>? postedAnswers = null)
    {
        category = NormalizeCategory(category);

        var categoryEntity = await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Number == category);

        if (categoryEntity is null)
        {
            return null;
        }

        var questions = await _db.AssessmentQuestions
            .AsNoTracking()
            .Include(q => q.Item)
            .Where(q => q.Item != null && q.Item.CategoryId == categoryEntity.Id)
            .OrderBy(q => q.Item!.Code)
            .ToListAsync();

        var questionIds = questions.Select(q => q.Id).ToList();
        var savedAnswers = await _db.AssessmentAnswers
            .AsNoTracking()
            .Where(a => a.AssessmentId == assessment.Id && questionIds.Contains(a.QuestionId))
            .ToDictionaryAsync(a => a.QuestionId);

        var postedByQuestionId = postedAnswers?
            .Where(q => q.QuestionId > 0)
            .GroupBy(q => q.QuestionId)
            .ToDictionary(g => g.Key, g => g.First());

        var answerModels = questions.Select(q =>
        {
            AssessmentQuestionInputViewModel? posted = null;
            postedByQuestionId?.TryGetValue(q.Id, out posted);
            savedAnswers.TryGetValue(q.Id, out var saved);

            return new AssessmentQuestionInputViewModel
            {
                QuestionId = q.Id,
                ItemCode = q.Item?.Code ?? string.Empty,
                ItemName = q.Item?.Name ?? string.Empty,
                QuestionText = q.QuestionText,
                Score = posted?.Score ?? saved?.Score,
                Comment = posted?.Comment ?? saved?.Comment
            };
        }).ToList();

        return new AssessmentQuestionsViewModel
        {
            AssessmentId = assessment.Id,
            CategoryNumber = category,
            CategoryName = categoryEntity.Name,
            CategoryType = categoryEntity.Type,
            OrganisationName = assessment.OrganisationName,
            ProgressPercent = (int)Math.Round(category / 7.0 * 100),
            Answers = answerModels
        };
    }

    private async Task<List<int>> GetCategoryQuestionIdsAsync(int category)
    {
        category = NormalizeCategory(category);

        var categoryId = await _db.Categories
            .AsNoTracking()
            .Where(c => c.Number == category)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        if (categoryId == 0)
        {
            return [];
        }

        return await _db.AssessmentQuestions
            .AsNoTracking()
            .Where(q => q.Item != null && q.Item.CategoryId == categoryId)
            .OrderBy(q => q.Item!.Code)
            .Select(q => q.Id)
            .ToListAsync();
    }

    private async Task SaveAnswersAsync(int assessmentId, List<AssessmentQuestionInputViewModel>? answers)
    {
        if (answers is null || answers.Count == 0)
        {
            return;
        }

        var questionIds = answers.Select(a => a.QuestionId).Distinct().ToList();
        var existing = await _db.AssessmentAnswers
            .Where(a => a.AssessmentId == assessmentId && questionIds.Contains(a.QuestionId))
            .ToDictionaryAsync(a => a.QuestionId);

        foreach (var answer in answers)
        {
            if (answer.QuestionId <= 0)
            {
                continue;
            }

            var hasScore = answer.Score is >= 1 and <= 5;
            var comment = string.IsNullOrWhiteSpace(answer.Comment) ? null : answer.Comment.Trim();

            if (!hasScore && string.IsNullOrWhiteSpace(comment))
            {
                continue;
            }

            if (existing.TryGetValue(answer.QuestionId, out var current))
            {
                if (hasScore)
                {
                    current.Score = answer.Score!.Value;
                }

                current.Comment = comment;
            }
            else if (hasScore)
            {
                _db.AssessmentAnswers.Add(new AssessmentAnswer
                {
                    AssessmentId = assessmentId,
                    QuestionId = answer.QuestionId,
                    Score = answer.Score!.Value,
                    Comment = comment
                });
            }
        }

        await _db.SaveChangesAsync();
    }

    private static int NormalizeCategory(int category) =>
        category is >= 1 and <= 7 ? category : 1;
}
