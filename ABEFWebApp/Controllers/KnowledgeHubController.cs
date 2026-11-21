using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

[Authorize]
public class KnowledgeHubController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
