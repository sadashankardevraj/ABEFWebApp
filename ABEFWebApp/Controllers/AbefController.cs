using ABEFWebApp.Data;
using ABEFWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEFWebApp.Controllers;

public class AbefController : Controller
{
    private readonly ApplicationDbContext _db;

    public AbefController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Why()
    {
        return View();
    }

    public async Task<IActionResult> What()
    {
        var viewModel = new AbefWhatViewModel
        {
            Categories = await _db.Categories
                .AsNoTracking()
                .OrderBy(c => c.Number)
                .ToListAsync(),
            Principles = await _db.Principles
                .AsNoTracking()
                .OrderBy(p => p.Number)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult HowItWorks()
    {
        return View();
    }

    public IActionResult Adri()
    {
        return View();
    }
}
