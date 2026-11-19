using ABEFWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

[Authorize]
public class MemberController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MemberController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Dashboard()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge();
        }

        var roles = await _userManager.GetRolesAsync(user);
        ViewData["FirstName"] = string.IsNullOrWhiteSpace(user.FirstName) ? "Member" : user.FirstName;
        ViewData["Role"] = roles.FirstOrDefault() ?? "Member";
        return View();
    }
}
