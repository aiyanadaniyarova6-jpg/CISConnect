using CISConnect.Data;
using CISConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Route("admin/faq")]
public class FaqController : Controller
{
    private readonly ApplicationDbContext _db;

    public FaqController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "FAQ";
        var items = await _db.FAQItems
            .OrderBy(f => f.Category)
            .ThenBy(f => f.DisplayOrder)
            .ToListAsync();
        return View(items);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New FAQ Item";
        return View(new FAQItem());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FAQItem item)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "New FAQ Item";
            return View(item);
        }
        _db.FAQItems.Add(item);
        await _db.SaveChangesAsync();
        TempData["Success"] = "FAQ item created.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.FAQItems.FindAsync(id);
        if (item is null) return NotFound();
        ViewData["Title"] = "Edit FAQ Item";
        return View(item);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(FAQItem item)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Edit FAQ Item";
            return View(item);
        }
        _db.FAQItems.Update(item);
        await _db.SaveChangesAsync();
        TempData["Success"] = "FAQ item updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.FAQItems.FindAsync(id);
        if (item is not null)
        {
            _db.FAQItems.Remove(item);
            await _db.SaveChangesAsync();
            TempData["Success"] = "FAQ item deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
