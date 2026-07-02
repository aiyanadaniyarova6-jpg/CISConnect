using CISConnect.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Xml;

namespace CISConnect.Controllers;

public class SitemapController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly string _baseUrl;

    public SitemapController(ApplicationDbContext db, IConfiguration config)
    {
        _db = db;
        _baseUrl = (config["Site:BaseUrl"] ?? "https://cisconnect.app").TrimEnd('/');
    }

    [HttpGet("/sitemap.xml")]
    public async Task<IActionResult> Index()
    {
        var posts = await _db.UpdatePosts
            .OrderByDescending(p => p.PublishedAt)
            .Select(p => new { p.Id, p.PublishedAt })
            .ToListAsync();

        var guides = await _db.GuideArticles
            .OrderByDescending(g => g.CreatedAt)
            .Select(g => new { g.Id, g.CreatedAt })
            .ToListAsync();

        var universities = await _db.Universities
            .Select(u => new { u.Id })
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        void AddUrl(string loc, string? lastmod = null, string priority = "0.5")
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{loc}</loc>");
            if (lastmod != null) sb.AppendLine($"    <lastmod>{lastmod}</lastmod>");
            sb.AppendLine($"    <priority>{priority}</priority>");
            sb.AppendLine("  </url>");
        }

        // Static pages
        AddUrl(_baseUrl + "/", lastmod: DateTime.UtcNow.ToString("yyyy-MM-dd"), priority: "1.0");
        AddUrl(_baseUrl + "/Universities", priority: "0.8");
        AddUrl(_baseUrl + "/Universities/Compare", priority: "0.7");
        AddUrl(_baseUrl + "/Menu/PreArrival", priority: "0.8");
        AddUrl(_baseUrl + "/Menu/ArrivalSetup", priority: "0.8");
        AddUrl(_baseUrl + "/Menu/LivingInMalaysia", priority: "0.7");
        AddUrl(_baseUrl + "/Menu/CISCommunity", priority: "0.7");
        AddUrl(_baseUrl + "/Menu/Deals", priority: "0.6");
        AddUrl(_baseUrl + "/Menu/Contacts", priority: "0.7");
        AddUrl(_baseUrl + "/FAQ", priority: "0.7");
        AddUrl(_baseUrl + "/Checklist", priority: "0.6");
        AddUrl(_baseUrl + "/Search", priority: "0.5");

        // Universities
        foreach (var u in universities)
            AddUrl($"{_baseUrl}/Universities/Details/{u.Id}", priority: "0.8");

        // Posts
        foreach (var p in posts)
            AddUrl($"{_baseUrl}/post/update-{p.Id}",
                lastmod: p.PublishedAt.ToString("yyyy-MM-dd"),
                priority: "0.6");

        // Guides
        foreach (var g in guides)
            AddUrl($"{_baseUrl}/post/guide-{g.Id}",
                lastmod: g.CreatedAt.ToString("yyyy-MM-dd"),
                priority: "0.6");

        sb.AppendLine("</urlset>");

        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }
}
