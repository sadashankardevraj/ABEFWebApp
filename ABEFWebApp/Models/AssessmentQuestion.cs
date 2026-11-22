using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class AssessmentQuestion
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    [Required]
    [MaxLength(400)]
    public string QuestionText { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public ICollection<AssessmentAnswer> Answers { get; set; } = new List<AssessmentAnswer>();
}
