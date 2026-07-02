using CISConnect.Data;
using CISConnect.Models;
using CISConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Route("admin/posts")]
public class PostsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IImageStorageService _storage;

    public PostsController(ApplicationDbContext db, IImageStorageService storage)
    {
        _db = db;
        _storage = storage;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Posts";
        var posts = await _db.UpdatePosts.OrderByDescending(p => p.PublishedAt).ToListAsync();
        return View(posts);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New Post";
        return View(new UpdatePost { PublishedAt = DateTime.UtcNow });
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UpdatePost post, IFormFile? imageFile)
    {
        var uploaded = await _storage.UploadAsync(imageFile!);
        if (uploaded is not null)
        {
            post.MediaUrl = uploaded;
            post.MediaType = "image";
        }

        NormalizePost(post);

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "New Post";
            ViewData["Error"] = "Post was not created. Please fix the highlighted fields.";
            return View(post);
        }

        _db.UpdatePosts.Add(post);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Post \"{post.Title}\" created.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _db.UpdatePosts.FindAsync(id);
        if (post is null) return NotFound();
        ViewData["Title"] = "Edit Post";
        return View(post);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdatePost post, IFormFile? imageFile)
    {
        var uploaded = await _storage.UploadAsync(imageFile!);
        if (uploaded is not null)
        {
            post.MediaUrl = uploaded;
            post.MediaType = "image";
        }

        NormalizePost(post);

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Edit Post";
            ViewData["Error"] = "Post was not updated. Please fix the highlighted fields.";
            return View(post);
        }

        _db.UpdatePosts.Update(post);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Post \"{post.Title}\" updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _db.UpdatePosts.FindAsync(id);
        if (post is not null)
        {
            _db.UpdatePosts.Remove(post);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Post \"{post.Title}\" deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Strips leading/trailing spaces from all text fields before saving.
    private static void NormalizePost(UpdatePost post)
    {
        post.Title = (post.Title ?? string.Empty).Trim();
        post.Summary = (post.Summary ?? string.Empty).Trim();
        post.Content = (post.Content ?? string.Empty).Trim();
        post.Category = (post.Category ?? string.Empty).Trim();
        post.MediaType = (post.MediaType ?? string.Empty).Trim();
        post.MediaUrl = (post.MediaUrl ?? string.Empty).Trim();
        post.MediaAltText = (post.MediaAltText ?? string.Empty).Trim();
        post.SourceName = (post.SourceName ?? string.Empty).Trim();
        post.SourceUrl = (post.SourceUrl ?? string.Empty).Trim();

        if (post.PublishedAt == default)
        {
            post.PublishedAt = DateTime.UtcNow;
        }
    }
}
