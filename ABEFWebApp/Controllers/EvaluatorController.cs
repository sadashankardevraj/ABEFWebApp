using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABEFWebApp.Controllers;

[Authorize(Roles = "Evaluator,Admin")]
public class EvaluatorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
