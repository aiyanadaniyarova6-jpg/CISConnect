using CISConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Route("admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

    public DashboardController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    [HttpGet("dashboard")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Dashboard";
        ViewData["PostsCount"]      = await _db.UpdatePosts.CountAsync();
        ViewData["GuidesCount"]     = await _db.GuideArticles.CountAsync();
        ViewData["UnisCount"]       = await _db.Universities.CountAsync();
        ViewData["FaqCount"]        = await _db.FAQItems.CountAsync();
        ViewData["LatestPost"]      = await _db.UpdatePosts.OrderByDescending(p => p.PublishedAt).Select(p => p.Title).FirstOrDefaultAsync();
        ViewData["HighlightedCount"] = await _db.UpdatePosts.CountAsync(p => p.IsHighlighted);
        return View();
    }
}
