using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Controllers;

public class FAQController : Controller
{
    private readonly ApplicationDbContext _context;

    public FAQController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm)
    {
        var query = _context.FAQItems
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var normalizedTerm = searchTerm.Trim();
            query = query.Where(item =>
                item.Question.Contains(normalizedTerm) ||
                item.Answer.Contains(normalizedTerm) ||
                item.Category.Contains(normalizedTerm));
        }

        var viewModel = new FAQViewModel
        {
            SearchTerm = searchTerm ?? string.Empty,
            Items = await query
                .OrderBy(item => item.Category)
                .ThenBy(item => item.DisplayOrder)
                .ToListAsync()
        };

        return View(viewModel);
    }
}
