using CISConnect.Data;
using CISConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Route("admin/universities")]
public class UniversitiesController : Controller
{
    private readonly ApplicationDbContext _db;

    public UniversitiesController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Universities";
        var unis = await _db.Universities
            .Include(u => u.Sections)
            .OrderBy(u => u.Name)
            .ToListAsync();
        return View(unis);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New University";
        return View(new University());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(University uni)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "New University";
            return View(uni);
        }
        _db.Universities.Add(uni);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"University \"{uni.Name}\" created.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var uni = await _db.Universities.Include(u => u.Sections).FirstOrDefaultAsync(u => u.Id == id);
        if (uni is null) return NotFound();
        ViewData["Title"] = "Edit University";
        return View(uni);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(University uni)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Edit University";
            return View(uni);
        }
        _db.Universities.Update(uni);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"University \"{uni.Name}\" updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var uni = await _db.Universities.FindAsync(id);
        if (uni is not null)
        {
            _db.Universities.Remove(uni);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"University \"{uni.Name}\" deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    // ── University Sections ─────────────────────────────────────────────────

    [HttpGet("add-section/{universityId:int}")]
    public async Task<IActionResult> AddSection(int universityId)
    {
        var uni = await _db.Universities.FindAsync(universityId);
        if (uni is null) return NotFound();
        ViewData["Title"] = $"Add Section - {uni.Name}";
        ViewBag.UniversityName = uni.Name;
        return View(new UniversitySection { UniversityId = universityId });
    }

    [HttpPost("add-section/{universityId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSection(UniversitySection section)
    {
        if (!ModelState.IsValid)
        {
            var uni = await _db.Universities.FindAsync(section.UniversityId);
            ViewData["Title"] = $"Add Section - {uni?.Name}";
            ViewBag.UniversityName = uni?.Name;
            return View(section);
        }
        _db.UniversitySections.Add(section);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Section added.";
        return RedirectToAction(nameof(Edit), new { id = section.UniversityId });
    }

    [HttpPost("delete-section")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSection(int sectionId, int universityId)
    {
        var section = await _db.UniversitySections.FindAsync(sectionId);
        if (section is not null)
        {
            _db.UniversitySections.Remove(section);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Section deleted.";
        }
        return RedirectToAction(nameof(Edit), new { id = universityId });
    }
}
