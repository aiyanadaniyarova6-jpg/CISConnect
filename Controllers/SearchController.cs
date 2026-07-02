using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Controllers;

public class SearchController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        var results = new List<PostSummaryViewModel>();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var normalizedQuery = query.ToLower();

            var updateResults = await _context.UpdatePosts
                .Where(post =>
                    post.Title.ToLower().Contains(normalizedQuery) ||
                    post.Summary.ToLower().Contains(normalizedQuery) ||
                    post.Content.ToLower().Contains(normalizedQuery) ||
                    post.Category.ToLower().Contains(normalizedQuery))
                .OrderByDescending(post => post.PublishedAt)
                .Select(post => new PostSummaryViewModel
                {
                    PublicId = $"update-{post.Id}",
                    Title = post.Title,
                    Summary = post.Summary,
                    Category = post.Category,
                    PublishedAt = post.PublishedAt,
                    SourceLabel = "Home"
                })
                .ToListAsync();

            var guideResults = await _context.GuideArticles
                .Include(article => article.MenuSection)
                .Where(article =>
                    article.Title.ToLower().Contains(normalizedQuery) ||
                    article.Summary.ToLower().Contains(normalizedQuery) ||
                    article.Content.ToLower().Contains(normalizedQuery) ||
                    (article.MenuSection != null && article.MenuSection.Name.ToLower().Contains(normalizedQuery)))
                .OrderByDescending(article => article.CreatedAt)
                .Select(article => new PostSummaryViewModel
                {
                    PublicId = $"guide-{article.Id}",
                    Title = article.Title,
                    Summary = article.Summary,
                    Category = article.MenuSection != null ? article.MenuSection.Name : "Guide",
                    PublishedAt = article.CreatedAt,
                    SourceLabel = article.MenuSection != null ? article.MenuSection.Name : "Guide"
                })
                .ToListAsync();

            var universityResults = await _context.Universities
                .Where(u =>
                    u.Name.ToLower().Contains(normalizedQuery) ||
                    u.Location.ToLower().Contains(normalizedQuery) ||
                    u.Description.ToLower().Contains(normalizedQuery))
                .OrderBy(u => u.Name)
                .Select(u => new UniversitySearchResult(u.Id, u.Name, u.Location, u.Description))
                .ToListAsync();

            results = updateResults
                .Concat(guideResults)
                .OrderByDescending(item => item.PublishedAt)
                .ToList();

            return View(new SearchViewModel
            {
                Query = query,
                Results = results,
                UniversityResults = universityResults
            });
        }

        return View(new SearchViewModel
        {
            Query = query,
            Results = results
        });
    }

    [EnableRateLimiting("search-live")]
    public async Task<IActionResult> Live(string? q)
    {
        var query = (q ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            return Json(new object[0]);

        var normalizedQuery = query.ToLower();

        var updateResults = await _context.UpdatePosts
            .Where(post =>
                post.Title.ToLower().Contains(normalizedQuery) ||
                post.Summary.ToLower().Contains(normalizedQuery) ||
                post.Category.ToLower().Contains(normalizedQuery))
            .OrderByDescending(post => post.PublishedAt)
            .Take(12)
            .Select(post => new
            {
                publicId = $"update-{post.Id}",
                title = post.Title,
                summary = post.Summary,
                category = post.Category,
                sourceLabel = "Update",
                publishedAt = post.PublishedAt.ToString("dd MMM yyyy")
            })
            .ToListAsync();

        var guideResults = await _context.GuideArticles
            .Include(a => a.MenuSection)
            .Where(a =>
                a.Title.ToLower().Contains(normalizedQuery) ||
                a.Summary.ToLower().Contains(normalizedQuery) ||
                (a.MenuSection != null && a.MenuSection.Name.ToLower().Contains(normalizedQuery)))
            .OrderByDescending(a => a.CreatedAt)
            .Take(8)
            .Select(a => new
            {
                publicId = $"guide-{a.Id}",
                title = a.Title,
                summary = a.Summary,
                category = a.MenuSection != null ? a.MenuSection.Name : "Guide",
                sourceLabel = "Guide",
                publishedAt = a.CreatedAt.ToString("dd MMM yyyy")
            })
            .ToListAsync();

        var universityResults = await _context.Universities
            .Where(u =>
                u.Name.ToLower().Contains(normalizedQuery) ||
                u.Location.ToLower().Contains(normalizedQuery))
            .Take(4)
            .Select(u => new
            {
                publicId = $"univ-{u.Id}",
                url = $"/Universities/Details/{u.Id}",
                title = u.Name,
                summary = u.Location,
                category = "University",
                sourceLabel = "University",
                publishedAt = string.Empty
            })
            .ToListAsync();

        var combined = updateResults.Cast<object>()
            .Concat(guideResults.Cast<object>())
            .Concat(universityResults.Cast<object>())
            .Take(16)
            .ToList();

        return Json(combined);
    }
}
