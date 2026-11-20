using ABEFWebApp.Data;
using ABEFWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ContactController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new ContactMessage();

        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                model.Name = $"{user.FirstName} {user.LastName}".Trim();
                model.Email = user.Email ?? string.Empty;
                model.Organisation = user.Organisation;
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([Bind(nameof(ContactMessage.Name), nameof(ContactMessage.Email), nameof(ContactMessage.Organisation), nameof(ContactMessage.Message))] ContactMessage model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var entity = new ContactMessage
        {
            Name = model.Name.Trim(),
            Email = model.Email.Trim(),
            Organisation = string.IsNullOrWhiteSpace(model.Organisation) ? null : model.Organisation.Trim(),
            Message = model.Message.Trim(),
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        _db.ContactMessages.Add(entity);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Thank you! Your message has been sent.";
        return RedirectToAction(nameof(Index));
    }
}
