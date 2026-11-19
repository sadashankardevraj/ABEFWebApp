using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABEFWebApp.Areas.Identity.Pages.Account.Manage;

[Authorize]
public class DownloadPersonalDataModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("./Index");

    public IActionResult OnPost() => RedirectToPage("./Index");
}
