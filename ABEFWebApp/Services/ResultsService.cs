using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ABEFWebApp.Services;

public class ResultsService
{
    private static readonly string[] LevelNames =
    [
        "",
        "Not evident",
        "Limited",
        "Developing",
        "Established",
        "Leading"
    ];

    private static readonly Color BeaNavy = Color.FromHex("#0A1A3A");
    private static readonly Color BeaRed = Color.FromHex("#8B1010");

    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public ResultsService(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public async Task<AssessmentResultsViewModel?> BuildAsync(int assessmentId, string userId)
    {
        var assessment = await _db.Assessments
            .AsNoTracking()
            .FirstOrDefaultAsync(a =>
                a.Id == assessmentId
                && a.UserId == userId
                && a.Status == "Submitted"
                && a.SubmittedAt != null);

        if (assessment is null || assessment.SubmittedAt is null)
        {
            return null;
        }

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
            .Where(a => a.AssessmentId == assessmentId)
            .ToDictionaryAsync(a => a.QuestionId);

        var categoryById = categories.ToDictionary(c => c.Id);

        var items = questions.Select(q =>
        {
            answers.TryGetValue(q.Id, out var answer);
            var score = answer?.Score is >= 1 and <= 5 ? answer.Score : (int?)null;
            categoryById.TryGetValue(q.Item!.CategoryId, out var category);

            return new AssessmentResultItemViewModel
            {
                CategoryNumber = category?.Number ?? 0,
                CategoryName = category?.Name ?? string.Empty,
                ItemCode = q.Item.Code,
                ItemName = q.Item.Name,
                Score = score,
                Level = score.HasValue ? LevelNames[score.Value] : "Not answered",
                Comment = string.IsNullOrWhiteSpace(answer?.Comment) ? null : answer.Comment.Trim()
            };
        }).ToList();

        var categoryExactAverages = new Dictionary<int, decimal>();
        var categoryResults = categories.Select(category =>
        {
            var scores = items
                .Where(i => i.CategoryNumber == category.Number && i.Score.HasValue)
                .Select(i => i.Score!.Value)
                .ToList();

            var exactAverage = scores.Count == 0 ? 0m : (decimal)scores.Average();
            categoryExactAverages[category.Number] = exactAverage;
            var (displayAverage, percentage, level) = FromExactAverage(exactAverage);

            return new AssessmentCategoryResultViewModel
            {
                Number = category.Number,
                Name = category.Name,
                Type = category.Type,
                Average = displayAverage,
                Percentage = percentage,
                Level = level
            };
        }).ToList();

        var exactOverallAverage = categoryExactAverages.Count == 0
            ? 0m
            : categoryExactAverages.Values.Average();
        var (overallAverage, overallPercentage, overallLevel) = FromExactAverage(exactOverallAverage);

        var orderedByStrength = categoryResults
            .OrderByDescending(c => categoryExactAverages[c.Number])
            .ThenBy(c => c.Number)
            .ToList();

        var strengths = orderedByStrength.Take(2).ToList();
        var areasToImprove = orderedByStrength.TakeLast(2).Reverse().ToList();

        var areaCategoryIds = categories
            .Where(c => areasToImprove.Any(a => a.Number == c.Number))
            .Select(c => c.Id)
            .ToList();

        var resourcesByCategory = await _db.Resources
            .AsNoTracking()
            .Where(r => r.IsPublished && r.CategoryId != null && areaCategoryIds.Contains(r.CategoryId.Value))
            .OrderBy(r => r.Title)
            .ToListAsync();

        var recommendations = areasToImprove.Select(area =>
        {
            var category = categories.First(c => c.Number == area.Number);
            var lowest = items
                .Where(i => i.CategoryNumber == area.Number && i.Score.HasValue)
                .OrderBy(i => i.Score)
                .ThenBy(i => i.ItemCode)
                .FirstOrDefault();

            var relatedResources = resourcesByCategory
                .Where(r => r.CategoryId == category.Id)
                .Take(2)
                .Select(r => new AssessmentRecommendationResourceViewModel
                {
                    Id = r.Id,
                    Title = r.Title
                })
                .ToList();

            return new AssessmentRecommendationViewModel
            {
                CategoryNumber = area.Number,
                CategoryName = area.Name,
                Average = area.Average,
                Level = area.Level,
                Recommendation = string.IsNullOrWhiteSpace(category.Recommendation)
                    ? "Explore this category and use ADRI to plan one practical improvement."
                    : category.Recommendation,
                LowestItemCode = lowest?.ItemCode,
                LowestItemScore = lowest?.Score,
                LowestItemLevel = lowest?.Level,
                Resources = relatedResources
            };
        }).ToList();

        return new AssessmentResultsViewModel
        {
            AssessmentId = assessment.Id,
            OrganisationName = assessment.OrganisationName,
            SubmittedAt = assessment.SubmittedAt.Value,
            OverallAverage = overallAverage,
            OverallPercentage = overallPercentage,
            OverallLevel = overallLevel,
            SummarySentence = $"Your organisation is at the {overallLevel} level overall.",
            Categories = categoryResults,
            Strengths = strengths,
            AreasToImprove = areasToImprove,
            Recommendations = recommendations,
            Items = items
        };
    }

    public async Task<AssessmentLatestResultViewModel?> GetLatestSubmittedResultAsync(string userId)
    {
        var latest = await _db.Assessments
            .AsNoTracking()
            .Where(a => a.UserId == userId && a.Status == "Submitted" && a.SubmittedAt != null)
            .OrderByDescending(a => a.SubmittedAt)
            .Select(a => new { a.Id, a.SubmittedAt })
            .FirstOrDefaultAsync();

        if (latest is null || latest.SubmittedAt is null)
        {
            return null;
        }

        var scores = await GetOverallScoresAsync([latest.Id]);
        if (!scores.TryGetValue(latest.Id, out var score))
        {
            return null;
        }

        return new AssessmentLatestResultViewModel
        {
            AssessmentId = latest.Id,
            OverallPercentage = score.OverallPercentage,
            OverallLevel = score.OverallLevel,
            SubmittedAt = latest.SubmittedAt.Value
        };
    }

    public async Task<IReadOnlyDictionary<int, AssessmentOverallScore>> GetOverallScoresAsync(
        IReadOnlyCollection<int> assessmentIds)
    {
        if (assessmentIds.Count == 0)
        {
            return new Dictionary<int, AssessmentOverallScore>();
        }

        var categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Number)
            .Select(c => new { c.Id, c.Number })
            .ToListAsync();

        var questions = await _db.AssessmentQuestions
            .AsNoTracking()
            .Include(q => q.Item)
            .Where(q => q.Item != null)
            .Select(q => new { q.Id, CategoryId = q.Item!.CategoryId })
            .ToListAsync();

        var answers = await _db.AssessmentAnswers
            .AsNoTracking()
            .Where(a => assessmentIds.Contains(a.AssessmentId) && a.Score >= 1 && a.Score <= 5)
            .Select(a => new { a.AssessmentId, a.QuestionId, a.Score })
            .ToListAsync();

        var answersByAssessment = answers
            .GroupBy(a => a.AssessmentId)
            .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.QuestionId, x => x.Score));

        var result = new Dictionary<int, AssessmentOverallScore>();

        foreach (var assessmentId in assessmentIds.Distinct())
        {
            answersByAssessment.TryGetValue(assessmentId, out var answerMap);
            answerMap ??= [];

            var exactCategoryAverages = categories.Select(category =>
            {
                var scores = questions
                    .Where(q => q.CategoryId == category.Id)
                    .Select(q => answerMap.TryGetValue(q.Id, out var score) ? score : (int?)null)
                    .Where(score => score.HasValue)
                    .Select(score => score!.Value)
                    .ToList();

                return scores.Count == 0 ? 0m : (decimal)scores.Average();
            }).ToList();

            var exactOverallAverage = exactCategoryAverages.Count == 0
                ? 0m
                : exactCategoryAverages.Average();
            var (overallAverage, overallPercentage, overallLevel) = FromExactAverage(exactOverallAverage);

            result[assessmentId] = new AssessmentOverallScore
            {
                AssessmentId = assessmentId,
                OverallAverage = overallAverage,
                OverallPercentage = overallPercentage,
                OverallLevel = overallLevel
            };
        }

        return result;
    }

    public static string BuildExportFileName(AssessmentResultsViewModel model, string extension)
    {
        var org = SanitizeFilePart(model.OrganisationName);
        var date = model.SubmittedAt.ToLocalTime().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        return $"ABEF-Assessment-{org}-{date}.{extension.TrimStart('.')}";
    }

    public static string BuildCsv(AssessmentResultsViewModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Category,Item Code,Item Name,Score,Level,Comment");

        foreach (var item in model.Items)
        {
            sb.Append(EscapeCsv(item.CategoryName)).Append(',');
            sb.Append(EscapeCsv(item.ItemCode)).Append(',');
            sb.Append(EscapeCsv(item.ItemName)).Append(',');
            sb.Append(item.Score?.ToString(CultureInfo.InvariantCulture) ?? string.Empty).Append(',');
            sb.Append(EscapeCsv(item.Level)).Append(',');
            sb.AppendLine(EscapeCsv(item.Comment ?? string.Empty));
        }

        sb.AppendLine();
        sb.AppendLine("Category,Average,Percentage,Level");

        foreach (var category in model.Categories)
        {
            sb.Append(EscapeCsv($"{category.Number}. {category.Name}")).Append(',');
            sb.Append(category.Average.ToString("0.0", CultureInfo.InvariantCulture)).Append(',');
            sb.Append(category.Percentage.ToString("0.0", CultureInfo.InvariantCulture)).Append(',');
            sb.AppendLine(EscapeCsv(category.Level));
        }

        sb.Append(EscapeCsv("Overall")).Append(',');
        sb.Append(model.OverallAverage.ToString("0.0", CultureInfo.InvariantCulture)).Append(',');
        sb.Append(model.OverallPercentage.ToString("0.0", CultureInfo.InvariantCulture)).Append(',');
        sb.AppendLine(EscapeCsv(model.OverallLevel));

        return sb.ToString();
    }

    public byte[] BuildPdf(AssessmentResultsViewModel model)
    {
        var logoPath = Path.Combine(_env.WebRootPath, "images", "bea-logo.png");
        var hasLogo = File.Exists(logoPath);
        var submitted = model.SubmittedAt.ToLocalTime().ToString("d MMMM yyyy", CultureInfo.GetCultureInfo("en-AU"));

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginTop(36);
                page.MarginBottom(40);
                page.MarginHorizontal(40);
                page.DefaultTextStyle(x => x.FontSize(9).FontColor(BeaNavy));

                page.Header().Element(header =>
                {
                    header.Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            if (hasLogo)
                            {
                                row.ConstantItem(110).Height(42).Image(logoPath).FitArea();
                            }

                            row.RelativeItem().PaddingLeft(hasLogo ? 12 : 0).AlignMiddle().Column(titleCol =>
                            {
                                titleCol.Item().Text("ABEF Self-Assessment Results")
                                    .FontSize(16).Bold().FontColor(BeaNavy);
                                titleCol.Item().PaddingTop(2).Text(model.OrganisationName)
                                    .FontSize(11).SemiBold().FontColor(BeaRed);
                                titleCol.Item().Text($"Submitted {submitted}")
                                    .FontSize(9).FontColor(Colors.Grey.Darken2);
                            });
                        });

                        col.Item().PaddingTop(10).LineHorizontal(2).LineColor(BeaRed);
                    });
                });

                page.Content().PaddingTop(16).Column(col =>
                {
                    col.Spacing(14);

                    col.Item().Background(Color.FromHex("#F5F6F8")).Border(1).BorderColor(Color.FromHex("#E5E7EB"))
                        .Padding(12).Row(row =>
                        {
                            row.RelativeItem().Column(box =>
                            {
                                box.Item().Text("Overall score").FontSize(9).FontColor(Colors.Grey.Darken2);
                                box.Item().Text($"{model.OverallPercentage:0}%")
                                    .FontSize(28).Bold().FontColor(BeaNavy);
                            });
                            row.ConstantItem(160).AlignMiddle().AlignRight().Column(box =>
                            {
                                box.Item().AlignRight().Text(model.OverallLevel)
                                    .FontSize(14).Bold().FontColor(BeaRed);
                                box.Item().AlignRight().Text($"Average {model.OverallAverage:0.0} / 5")
                                    .FontSize(9).FontColor(Colors.Grey.Darken2);
                            });
                        });

                    col.Item().Text(model.SummarySentence).FontSize(10).SemiBold();

                    col.Item().Element(e => SectionTitle(e, "Score by category"));
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(24);
                            columns.RelativeColumn(3);
                            columns.ConstantColumn(48);
                            columns.ConstantColumn(58);
                            columns.RelativeColumn(1.4f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TableHeaderCell).Text("#");
                            header.Cell().Element(TableHeaderCell).Text("Category");
                            header.Cell().Element(TableHeaderCell).AlignRight().Text("Avg");
                            header.Cell().Element(TableHeaderCell).AlignRight().Text("%");
                            header.Cell().Element(TableHeaderCell).Text("Level");
                        });

                        foreach (var category in model.Categories)
                        {
                            table.Cell().Element(TableBodyCell).Text(category.Number.ToString(CultureInfo.InvariantCulture));
                            table.Cell().Element(TableBodyCell).Text(category.Name);
                            table.Cell().Element(TableBodyCell).AlignRight()
                                .Text(category.Average.ToString("0.0", CultureInfo.InvariantCulture));
                            table.Cell().Element(TableBodyCell).AlignRight()
                                .Text(category.Percentage.ToString("0.0", CultureInfo.InvariantCulture));
                            table.Cell().Element(TableBodyCell).Text(category.Level);
                        }
                    });

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().PaddingRight(6).Element(box =>
                            InsightBox(box, "Strengths", model.Strengths, Color.FromHex("#E8F5EE")));
                        row.RelativeItem().PaddingLeft(6).Element(box =>
                            InsightBox(box, "Areas to improve", model.AreasToImprove, Color.FromHex("#F8F1E4")));
                    });

                    if (model.Recommendations.Count > 0)
                    {
                        col.Item().Element(e => SectionTitle(e, "Recommendations"));
                        foreach (var rec in model.Recommendations)
                        {
                            col.Item().Border(1).BorderColor(Color.FromHex("#E5E7EB")).Padding(10).Column(recCol =>
                            {
                                recCol.Item().Text($"{rec.CategoryNumber}. {rec.CategoryName}")
                                    .SemiBold().FontColor(BeaNavy);
                                recCol.Item().Text($"{rec.Average:0.0} — {rec.Level}")
                                    .FontSize(8).FontColor(Colors.Grey.Darken2);
                                recCol.Item().PaddingTop(4).Text(rec.Recommendation).FontSize(9);
                                if (!string.IsNullOrWhiteSpace(rec.LowestItemCode) && rec.LowestItemScore.HasValue)
                                {
                                    recCol.Item().PaddingTop(4)
                                        .Text($"Lowest item: {rec.LowestItemCode} scored {rec.LowestItemScore} {rec.LowestItemLevel}")
                                        .FontSize(8).FontColor(BeaRed);
                                }
                            });
                        }
                    }

                    col.Item().Element(e => SectionTitle(e, "All item scores"));
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(36);
                            columns.RelativeColumn(2.2f);
                            columns.ConstantColumn(36);
                            columns.RelativeColumn(1.2f);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(TableHeaderCell).Text("Code");
                            header.Cell().Element(TableHeaderCell).Text("Item");
                            header.Cell().Element(TableHeaderCell).AlignRight().Text("Score");
                            header.Cell().Element(TableHeaderCell).Text("Level");
                            header.Cell().Element(TableHeaderCell).Text("Comment");
                        });

                        foreach (var item in model.Items)
                        {
                            table.Cell().Element(TableBodyCell).Text(item.ItemCode).SemiBold();
                            table.Cell().Element(TableBodyCell).Text(item.ItemName);
                            table.Cell().Element(TableBodyCell).AlignRight()
                                .Text(item.Score?.ToString(CultureInfo.InvariantCulture) ?? "—");
                            table.Cell().Element(TableBodyCell).Text(item.Level);
                            table.Cell().Element(TableBodyCell).Text(item.Comment ?? string.Empty);
                        }
                    });
                });

                page.Footer().Element(footer =>
                {
                    footer.Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor(Color.FromHex("#E5E7EB"));
                        col.Item().PaddingTop(6).Row(row =>
                        {
                            row.RelativeItem().Text("Business Excellence Australia - Confidential")
                                .FontSize(8).FontColor(Colors.Grey.Darken1);
                            row.ConstantItem(80).AlignRight().Text(text =>
                            {
                                text.Span("Page ").FontSize(8).FontColor(Colors.Grey.Darken1);
                                text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Darken1);
                                text.Span(" of ").FontSize(8).FontColor(Colors.Grey.Darken1);
                                text.TotalPages().FontSize(8).FontColor(Colors.Grey.Darken1);
                            });
                        });
                    });
                });
            });
        }).GeneratePdf();
    }

    private static void SectionTitle(IContainer container, string title)
    {
        container.Text(title).FontSize(12).Bold().FontColor(BeaNavy);
    }

    private static void InsightBox(
        IContainer container,
        string title,
        IReadOnlyList<AssessmentCategoryResultViewModel> items,
        Color background)
    {
        container.Background(background).Padding(10).Column(col =>
        {
            col.Item().Text(title).SemiBold().FontColor(BeaNavy).FontSize(10);
            foreach (var item in items)
            {
                col.Item().PaddingTop(4)
                    .Text($"{item.Number}. {item.Name} — {item.Average:0.0} ({item.Level})")
                    .FontSize(8);
            }
        });
    }

    private static IContainer TableHeaderCell(IContainer container) =>
        container.DefaultTextStyle(x => x.SemiBold().FontSize(8).FontColor(Colors.White))
            .Background(BeaNavy)
            .PaddingVertical(5)
            .PaddingHorizontal(4);

    private static IContainer TableBodyCell(IContainer container) =>
        container.BorderBottom(0.5f).BorderColor(Color.FromHex("#E5E7EB"))
            .PaddingVertical(4)
            .PaddingHorizontal(4)
            .DefaultTextStyle(x => x.FontSize(8));

    /// <summary>
    /// Percentage is always from the exact average (exact/5*100, 1 dp).
    /// Average is rounded to 1 dp for display/level only.
    /// </summary>
    private static (decimal DisplayAverage, decimal Percentage, string Level) FromExactAverage(decimal exactAverage)
    {
        var displayAverage = Math.Round(exactAverage, 1, MidpointRounding.AwayFromZero);
        var percentage = Math.Round(exactAverage / 5m * 100m, 1, MidpointRounding.AwayFromZero);
        return (displayAverage, percentage, LevelFromAverage(displayAverage));
    }

    private static string LevelFromAverage(decimal average)
    {
        if (average <= 0)
        {
            return LevelNames[1];
        }

        var rounded = (int)Math.Round(average, MidpointRounding.AwayFromZero);
        rounded = Math.Clamp(rounded, 1, 5);
        return LevelNames[rounded];
    }

    private static string EscapeCsv(string value)
    {
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }

    private static string SanitizeFilePart(string value)
    {
        var cleaned = Regex.Replace(value.Trim(), @"[^\w\-]+", "-");
        cleaned = Regex.Replace(cleaned, "-{2,}", "-").Trim('-');
        return string.IsNullOrWhiteSpace(cleaned) ? "Organisation" : cleaned;
    }
}
