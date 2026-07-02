using CISConnect.Data;
using CISConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Route("admin/guides")]
public class GuidesController : Controller
{
    private readonly ApplicationDbContext _db;

    public GuidesController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Guides";
        var guides = await _db.GuideArticles
            .Include(g => g.MenuSection)
            .OrderBy(g => g.MenuSectionId)
            .ThenBy(g => g.Title)
            .ToListAsync();
        return View(guides);
    }

    private async Task PopulateSectionsAsync(int? selectedId = null)
    {
        var sections = await _db.MenuSections.OrderBy(s => s.Name).ToListAsync();
        ViewBag.Sections = new SelectList(sections, "Id", "Name", selectedId);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "New Guide";
        await PopulateSectionsAsync();
        return View(new GuideArticle { CreatedAt = DateTime.UtcNow });
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GuideArticle guide)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "New Guide";
            await PopulateSectionsAsync(guide.MenuSectionId);
            return View(guide);
        }
        _db.GuideArticles.Add(guide);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Guide \"{guide.Title}\" created.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var guide = await _db.GuideArticles.FindAsync(id);
        if (guide is null) return NotFound();
        ViewData["Title"] = "Edit Guide";
        await PopulateSectionsAsync(guide.MenuSectionId);
        return View(guide);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GuideArticle guide)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Edit Guide";
            await PopulateSectionsAsync(guide.MenuSectionId);
            return View(guide);
        }
        _db.GuideArticles.Update(guide);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Guide \"{guide.Title}\" updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var guide = await _db.GuideArticles.FindAsync(id);
        if (guide is not null)
        {
            _db.GuideArticles.Remove(guide);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Guide \"{guide.Title}\" deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
