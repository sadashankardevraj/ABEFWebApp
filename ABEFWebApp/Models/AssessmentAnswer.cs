using System.ComponentModel.DataAnnotations;

namespace ABEFWebApp.Models;

public class AssessmentAnswer
{
    public int Id { get; set; }

    public int AssessmentId { get; set; }

    public Assessment? Assessment { get; set; }

    public int QuestionId { get; set; }

    public AssessmentQuestion? Question { get; set; }

    [Range(1, 5)]
    public int Score { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }
}
