using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABEFWebApp.Areas.Identity.Pages.Account.Manage;

[Authorize]
public class EmailModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("./Index");
}
