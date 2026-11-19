using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ABEFWebApp.Services;

public class FileEmailSender : IEmailSender
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FileEmailSender> _logger;

    public FileEmailSender(IWebHostEnvironment environment, ILogger<FileEmailSender> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogInformation("Sending email to {Email} with subject '{Subject}'", email, subject);
        _logger.LogInformation("Email body: {Body}", htmlMessage);

        var outboxPath = Path.Combine(_environment.ContentRootPath, "EmailOutbox");
        Directory.CreateDirectory(outboxPath);

        var safeEmail = string.Join("_", email.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
        var fileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmss_fff}_{safeEmail}.html";
        var filePath = Path.Combine(outboxPath, fileName);

        var document = new StringBuilder();
        document.AppendLine("<!DOCTYPE html>");
        document.AppendLine("<html><head><meta charset=\"utf-8\" /><title>");
        document.Append(System.Net.WebUtility.HtmlEncode(subject));
        document.AppendLine("</title></head><body>");
        document.AppendLine($"<p><strong>To:</strong> {System.Net.WebUtility.HtmlEncode(email)}</p>");
        document.AppendLine($"<p><strong>Subject:</strong> {System.Net.WebUtility.HtmlEncode(subject)}</p>");
        document.AppendLine("<hr />");
        document.AppendLine(htmlMessage);
        document.AppendLine("</body></html>");

        await File.WriteAllTextAsync(filePath, document.ToString());
        _logger.LogInformation("Email saved to {FilePath}", filePath);
    }
}
