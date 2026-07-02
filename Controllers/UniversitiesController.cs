using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CISConnect.Controllers;

public class UniversitiesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly string _baseUrl;
    private static readonly Dictionary<string, int> SectionOrder = UniversityDetailsViewModel.SectionOrder
        .Select((sectionType, index) => new { sectionType, index })
        .ToDictionary(item => item.sectionType, item => item.index);

    public UniversitiesController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _baseUrl = config["Site:BaseUrl"] ?? "https://cisconnect.app";
    }

    [OutputCache(PolicyName = "public-lists")]
    public async Task<IActionResult> Index()
    {
        var universities = await _context.Universities
            .OrderBy(university => university.Name)
            .ToListAsync();

        return View(universities);
    }

    public async Task<IActionResult> Details(int id)
    {
        var university = await _context.Universities
            .Include(item => item.Sections)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (university is null)
        {
            return NotFound();
        }

        ViewData["Description"] = university.Description.Length > 160
            ? university.Description[..157] + "…"
            : university.Description;
        ViewData["CanonicalUrl"] = $"{_baseUrl}/Universities/Details/{id}";
        ViewData["JsonLd"] = System.Text.Json.JsonSerializer.Serialize(new
        {
            @context = "https://schema.org",
            @type = "CollegeOrUniversity",
            name = university.Name,
            description = university.Description,
            address = new { @type = "PostalAddress", addressLocality = university.Location, addressCountry = "MY" },
            url = $"{_baseUrl}/Universities/Details/{id}"
        });

        var viewModel = new UniversityDetailsViewModel
        {
            University = university,
            Sections = university.Sections
                .OrderBy(section => SectionOrder.GetValueOrDefault(section.SectionType, 999))
                .ThenBy(section => section.SectionType)
                .ToList()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Compare()
    {
        var universities = await _context.Universities
            .Include(university => university.Sections)
            .OrderBy(university => university.Id)
            .ToListAsync();

        return View(universities);
    }
}
