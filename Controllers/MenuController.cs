using CISConnect.Data;
using CISConnect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Controllers;

public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> PreArrival()
    {
        return await RenderSectionPageAsync("pre-arrival", "Pre-Arrival");
    }

    public async Task<IActionResult> ArrivalSetup()
    {
        return await RenderSectionPageAsync("arrival-setup", "Arrival & Setup");
    }

    public async Task<IActionResult> LivingInMalaysia()
    {
        return await RenderSectionPageAsync("living-in-malaysia", "Living in Malaysia");
    }

    public async Task<IActionResult> CISCommunity()
    {
        return await RenderSectionPageAsync("cis-community", "CIS Community");
    }

    public async Task<IActionResult> Deals()
    {
        return await RenderSectionPageAsync("deals-recommendations", "Deals & Recommendations");
    }

    public async Task<IActionResult> Contacts()
    {
        var section = await _context.MenuSections
            .Include(menuSection => menuSection.GuideArticles)
            .FirstOrDefaultAsync(menuSection => menuSection.Slug == "important-contacts");

        if (section is null)
        {
            return NotFound();
        }

        var viewModel = new MenuPageViewModel
        {
            Section = section,
            Articles = section.GuideArticles.OrderByDescending(article => article.CreatedAt).ToList(),
            Contacts = await _context.ContactItems
                .OrderBy(contact => contact.Category)
                .ThenBy(contact => contact.Name)
                .ToListAsync(),
            CountrySupportItems = GetCountrySupportItems()
        };

        ViewData["Title"] = "Important Contacts";

        return View(viewModel);
    }

    private static IEnumerable<CountrySupportViewModel> GetCountrySupportItems()
    {
        return new List<CountrySupportViewModel>
        {
            new()
            {
                Country = "Kazakhstan",
                EmbassyName = "Embassy of Kazakhstan in Malaysia",
                EmbassyUrl = "https://www.gov.kz/memleket/entities/mfa-kuala-lumpur?lang=en",
                CommunityNote = "Kazakh diaspora and Kazakh Students Association chats are listed in CIS Community.",
                StudentTip = "Good for consular registration, passport issues, and document legalization questions."
            },
            new()
            {
                Country = "Uzbekistan",
                EmbassyName = "Embassy of Uzbekistan in Malaysia",
                EmbassyUrl = "https://gov.uz/en/mfa/sections/view/52419",
                CommunityNote = "Use official embassy contacts first for legal or passport matters.",
                StudentTip = "Ask your university international office and embassy before visa renewal deadlines."
            },
            new()
            {
                Country = "Kyrgyzstan",
                EmbassyName = "Embassy of the Kyrgyz Republic in Malaysia",
                EmbassyUrl = "https://mfa.gov.kg/en/dm/Embassy-of-the-Kyrgyz-Republic-in-the-Malaysia",
                CommunityNote = "Student communities may help with everyday tips, but official matters should be checked with the embassy.",
                StudentTip = "Save embassy details before travelling in case you need urgent document support."
            },
            new()
            {
                Country = "Tajikistan",
                EmbassyName = "Embassy of Tajikistan in Malaysia",
                EmbassyUrl = "https://www.mfa.tj/en/main/view/1259/embassy-of-the-republic-of-tajikistan-in-malaysia",
                CommunityNote = "Check official embassy pages for latest contact and appointment information.",
                StudentTip = "Useful for passport, consular, and graduation document questions."
            },
            new()
            {
                Country = "Turkmenistan",
                EmbassyName = "Embassy of Turkmenistan in Malaysia",
                EmbassyUrl = "https://malaysia.tmembassy.gov.tm/",
                CommunityNote = "Use official embassy channels for consular information.",
                StudentTip = "Keep contact details saved before long holidays or travel outside Malaysia."
            },
            new()
            {
                Country = "Azerbaijan",
                EmbassyName = "Embassy of Azerbaijan in Malaysia",
                EmbassyUrl = "https://kualalumpur.mfa.gov.az/en",
                CommunityNote = "Official embassy pages are the safest starting point for consular support.",
                StudentTip = "Check document requirements early if you plan to legalize your diploma."
            },
            new()
            {
                Country = "Russia",
                EmbassyName = "Embassy of Russia in Malaysia",
                EmbassyUrl = "https://malaysia.mid.ru/",
                CommunityNote = "Use official consular information for passport and legal document matters.",
                StudentTip = "Keep embassy and university international office contacts together."
            },
            new()
            {
                Country = "Armenia",
                EmbassyName = "Embassy of Armenia accredited to Malaysia",
                EmbassyUrl = "https://www.mfa.am/en/embassies/my",
                CommunityNote = "Some support may be handled through an accredited embassy rather than a resident office.",
                StudentTip = "Confirm jurisdiction and document procedure before making plans."
            },
            new()
            {
                Country = "Moldova",
                EmbassyName = "Embassy of Moldova accredited to Malaysia",
                EmbassyUrl = "https://thailand.mfa.gov.md/en",
                CommunityNote = "Moldova's diplomatic coverage for Malaysia is typically through a regional embassy.",
                StudentTip = "Confirm which embassy covers your consular district before travelling."
            },
            new()
            {
                Country = "Belarus",
                EmbassyName = "Embassy of Belarus in Malaysia",
                EmbassyUrl = "https://malaysia.mfa.gov.by/en/",
                CommunityNote = "Use official embassy channels for passport and consular matters.",
                StudentTip = "Register with the embassy on arrival for emergency consular support."
            }
        };
    }

    private async Task<IActionResult> RenderSectionPageAsync(string slug, string pageTitle)
    {
        var section = await _context.MenuSections
            .Include(menuSection => menuSection.GuideArticles)
            .FirstOrDefaultAsync(menuSection => menuSection.Slug == slug);

        if (section is null)
        {
            return NotFound();
        }

        var viewModel = new MenuPageViewModel
        {
            Section = section,
            Articles = section.GuideArticles.OrderByDescending(article => article.CreatedAt).ToList()
        };

        ViewData["Title"] = pageTitle;

        return View("Section", viewModel);
    }
}
