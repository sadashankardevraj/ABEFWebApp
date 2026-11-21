using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

[Authorize]
public class AssessmentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
