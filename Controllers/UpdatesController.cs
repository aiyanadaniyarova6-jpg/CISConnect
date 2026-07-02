using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Controllers;

public class UpdatesController : Controller
{
    private readonly ApplicationDbContext _context;

    public UpdatesController(ApplicationDbContext context)
    {
        _context = context;
    }

    private const int PageSize = 9;

    [OutputCache(PolicyName = "public-lists", VaryByQueryKeys = ["page"])]
    public async Task<IActionResult> Index(int page = 1)
    {
        page = Math.Max(1, page);

        var highlighted = await _context.UpdatePosts
            .Where(post => post.IsHighlighted)
            .OrderByDescending(post => post.PublishedAt)
            .ThenByDescending(post => post.Id)
            .Take(3)
            .ToListAsync();

        var totalPosts = await _context.UpdatePosts.CountAsync();
        var totalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);

        var latestUpdates = await _context.UpdatePosts
            .OrderByDescending(post => post.PublishedAt)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var viewModel = new HomeViewModel
        {
            HighlightedUpdates = highlighted,
            LatestUpdates = latestUpdates,
            CurrentPage = page,
            TotalPages = Math.Max(1, totalPages)
        };

        return View(viewModel);
    }
}
