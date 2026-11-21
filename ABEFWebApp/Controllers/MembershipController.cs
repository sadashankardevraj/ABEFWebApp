using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

public class MembershipController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
