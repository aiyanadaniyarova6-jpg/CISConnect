using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CISConnect.Controllers;

[Route("post")]
public class PostController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly string _baseUrl;

    public PostController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _baseUrl = config["Site:BaseUrl"] ?? "https://cisconnect.app";
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        if (id.StartsWith("update-", StringComparison.OrdinalIgnoreCase)
            && int.TryParse(id["update-".Length..], out var updateId))
        {
            var post = await _context.UpdatePosts
                .FirstOrDefaultAsync(item => item.Id == updateId);

            if (post is null)
            {
                return NotFound();
            }

            var relatedPosts = await _context.UpdatePosts
                .Where(item => item.Id != post.Id && item.Category == post.Category)
                .OrderByDescending(item => item.PublishedAt)
                .Take(3)
                .Select(item => new PostSummaryViewModel
                {
                    PublicId = $"update-{item.Id}",
                    Title = item.Title,
                    Summary = item.Summary,
                    Category = item.Category,
                    PublishedAt = item.PublishedAt,
                    SourceLabel = "Home"
                })
                .ToListAsync();

            var orderedPosts = await _context.UpdatePosts
                .OrderByDescending(item => item.PublishedAt)
                .ThenByDescending(item => item.Id)
                .Select(item => new PostSummaryViewModel
                {
                    PublicId = $"update-{item.Id}",
                    Title = item.Title,
                    Summary = item.Summary,
                    Category = item.Category,
                    PublishedAt = item.PublishedAt,
                    SourceLabel = "Home"
                })
                .ToListAsync();

            var currentIndex = orderedPosts.FindIndex(item => item.PublicId == id);

            var canonicalUrl = $"{_baseUrl}/post/{id}";
            ViewData["Description"] = post.Summary;
            ViewData["CanonicalUrl"] = canonicalUrl;
            ViewData["OgType"] = "article";
            if (!string.IsNullOrWhiteSpace(post.MediaUrl) &&
                (post.MediaType == "image" || string.IsNullOrWhiteSpace(post.MediaType)))
                ViewData["OgImage"] = post.MediaUrl;

            ViewData["JsonLd"] = System.Text.Json.JsonSerializer.Serialize(new
            {
                @context = "https://schema.org",
                @type = "NewsArticle",
                headline = post.Title,
                description = post.Summary,
                datePublished = post.PublishedAt.ToString("o"),
                dateModified = (post.LastVerifiedAt ?? post.PublishedAt).ToString("o"),
                url = canonicalUrl,
                publisher = new { @type = "Organization", name = "CIS Connect" }
            });

            var viewModel = new PostDetailsViewModel
            {
                PublicId = id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                Category = post.Category,
                PublishedAt = post.PublishedAt,
                MediaType = post.MediaType,
                MediaUrl = post.MediaUrl,
                MediaAltText = post.MediaAltText,
                SourceName = post.SourceName,
                SourceUrl = post.SourceUrl,
                LastVerifiedAt = post.LastVerifiedAt,
                IsHighlighted = post.IsHighlighted,
                SourceLabel = "Home Feed",
                BackController = "Updates",
                BackAction = "Index",
                BackButtonText = "Back to Home",
                Breadcrumbs = new List<BreadcrumbItemViewModel>
                {
                    new() { Label = "Home", Controller = "Updates", Action = "Index" },
                    new() { Label = post.Category, Controller = "Updates", Action = "Index" },
                    new() { Label = post.Title, IsCurrent = true }
                },
                PreviousPost = currentIndex >= 0 && currentIndex < orderedPosts.Count - 1 ? orderedPosts[currentIndex + 1] : null,
                NextPost = currentIndex > 0 ? orderedPosts[currentIndex - 1] : null,
                RelatedPosts = relatedPosts
            };

            return View(viewModel);
        }

        if (id.StartsWith("guide-", StringComparison.OrdinalIgnoreCase)
            && int.TryParse(id["guide-".Length..], out var guideId))
        {
            var article = await _context.GuideArticles
                .Include(item => item.MenuSection)
                .FirstOrDefaultAsync(item => item.Id == guideId);

            if (article is null)
            {
                return NotFound();
            }

            var sectionName = article.MenuSection?.Name ?? "Guide";

            var relatedGuides = await _context.GuideArticles
                .Where(item => item.Id != article.Id && item.MenuSectionId == article.MenuSectionId)
                .OrderByDescending(item => item.CreatedAt)
                .Take(3)
                .Include(item => item.MenuSection)
                .Select(item => new PostSummaryViewModel
                {
                    PublicId = $"guide-{item.Id}",
                    Title = item.Title,
                    Summary = item.Summary,
                    Category = item.MenuSection != null ? item.MenuSection.Name : "Guide",
                    PublishedAt = item.CreatedAt,
                    SourceLabel = item.MenuSection != null ? item.MenuSection.Name : "Guide"
                })
                .ToListAsync();

            var orderedGuides = await _context.GuideArticles
                .Where(item => item.MenuSectionId == article.MenuSectionId)
                .OrderByDescending(item => item.CreatedAt)
                .ThenByDescending(item => item.Id)
                .Select(item => new PostSummaryViewModel
                {
                    PublicId = $"guide-{item.Id}",
                    Title = item.Title,
                    Summary = item.Summary,
                    Category = sectionName,
                    PublishedAt = item.CreatedAt,
                    SourceLabel = sectionName
                })
                .ToListAsync();

            var currentGuideIndex = orderedGuides.FindIndex(item => item.PublicId == id);

            var guideCanonicalUrl = $"{_baseUrl}/post/{id}";
            ViewData["Description"] = article.Summary;
            ViewData["CanonicalUrl"] = guideCanonicalUrl;
            ViewData["OgType"] = "article";
            ViewData["JsonLd"] = System.Text.Json.JsonSerializer.Serialize(new
            {
                @context = "https://schema.org",
                @type = "Article",
                headline = article.Title,
                description = article.Summary,
                datePublished = article.CreatedAt.ToString("o"),
                dateModified = (article.LastVerifiedAt ?? article.CreatedAt).ToString("o"),
                url = guideCanonicalUrl,
                publisher = new { @type = "Organization", name = "CIS Connect" }
            });

            var viewModel = new PostDetailsViewModel
            {
                PublicId = id,
                Title = article.Title,
                Summary = article.Summary,
                Content = article.Content,
                Category = sectionName,
                PublishedAt = article.CreatedAt,
                SourceName = article.SourceName,
                SourceUrl = article.SourceUrl,
                LastVerifiedAt = article.LastVerifiedAt,
                SourceLabel = sectionName,
                BackController = "Menu",
                BackAction = ResolveMenuBackAction(article.MenuSection?.Slug),
                BackButtonText = "Back to Menu",
                Breadcrumbs = new List<BreadcrumbItemViewModel>
                {
                    new() { Label = "Menu", Controller = "Menu", Action = ResolveMenuBackAction(article.MenuSection?.Slug) },
                    new() { Label = sectionName, Controller = "Menu", Action = ResolveMenuBackAction(article.MenuSection?.Slug) },
                    new() { Label = article.Title, IsCurrent = true }
                },
                PreviousPost = currentGuideIndex >= 0 && currentGuideIndex < orderedGuides.Count - 1 ? orderedGuides[currentGuideIndex + 1] : null,
                NextPost = currentGuideIndex > 0 ? orderedGuides[currentGuideIndex - 1] : null,
                RelatedPosts = relatedGuides
            };

            return View(viewModel);
        }

        return NotFound();
    }

    private static string ResolveMenuBackAction(string? slug)
    {
        return slug switch
        {
            "pre-arrival" => "PreArrival",
            "arrival-setup" => "ArrivalSetup",
            "living-in-malaysia" => "LivingInMalaysia",
            "cis-community" => "CISCommunity",
            "deals-recommendations" => "Deals",
            "important-contacts" => "Contacts",
            _ => "PreArrival"
        };
    }
}
