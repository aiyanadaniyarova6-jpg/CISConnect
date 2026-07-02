using CISConnect.Data;
using CISConnect.Models;
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
    private readonly IWebHostEnvironment _env;

    public PostsController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
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
        var uploaded = await TrySaveImageAsync(imageFile);
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
        var uploaded = await TrySaveImageAsync(imageFile);
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

    // Saves uploaded image to /uploads/posts/, returns its URL or null if no file / bad extension.
    private async Task<string?> TrySaveImageAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0) return null;

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".avif" };
        if (!allowed.Contains(ext)) return null;

        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "posts");
        Directory.CreateDirectory(uploadsDir);

        var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(uploadsDir, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/posts/{fileName}";
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
